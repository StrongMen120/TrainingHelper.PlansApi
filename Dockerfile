FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY ./src ./src
COPY ./nuget.config ./nuget.config

ARG VERSION
ARG FILE_VERSION

RUN dotnet restore "./src/Training.API.Plans/Training.API.Plans.csproj"
RUN dotnet build "./src/Training.API.Plans/Training.API.Plans.csproj" -c Release --no-restore -p:Version="$VERSION" -p:FileVersion="$FILE_VERSION"

FROM build AS publish
RUN dotnet publish "./src/Training.API.Plans/Training.API.Plans.csproj" -c Release -o /app/publish --no-build -p:Version="$VERSION" -p:FileVersion="$FILE_VERSION"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Training.API.Plans.dll"]