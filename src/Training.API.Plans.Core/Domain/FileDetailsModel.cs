namespace Training.API.Plans.Core.Domain;

public record FileDetailsModel(
    Guid Identifier,
    string Name,
    long Size,
    string Type,
    byte[] Bytes
);