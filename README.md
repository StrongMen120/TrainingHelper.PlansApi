# Magistry-Back

## Updating Database

```
dotnet ef migrations add AddTestTable --project .\src\Training.API.Plans.Persistance.TrainingsDb\Training.API.Plans.Persistance.TrainingsDb.csproj --context TrainingDbContext --startup-project .\src\Training.API.Plans\Training.API.Plans.csproj
```

```
dotnet ef migrations remove --project .\src\Training.API.Plans.Persistance.TrainingsDb\Training.API.Plans.Persistance.TrainingsDb.csproj --context TrainingDbContext --startup-project .\src\Training.API.Plans\Training.API.Plans.csproj
```

dotnet ef database update AddAuthorToExercise --project .\src\Training.API.Plans.Persistance.TrainingsDb\Training.API.Plans.Persistance.TrainingsDb.csproj --context TrainingDbContext --startup-project .\src\Training.API.Plans\Training.API.Plans.csproj

```

```
