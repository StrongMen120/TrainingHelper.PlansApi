using Mapster;
using NodaTime;

namespace Training.API.Plans.Integrations.Mappings;

public class NodaTimeMappingsRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<System.DateTime, OffsetDateTime>()
            .MapWith(src => NodaTime.OffsetDateTime.FromDateTimeOffset(new System.DateTimeOffset(src)));

        config.NewConfig<ZonedDateTime, OffsetDateTime>()
            .MapWith(src => src.ToOffsetDateTime());

        config.NewConfig<ZonedDateTime?, OffsetDateTime?>()
            .MapWith(src => src.HasValue ? src.Value.ToOffsetDateTime() : null);

        config.NewConfig<DateTimeOffset, ZonedDateTime>()
            .MapWith(src => ZonedDateTime.FromDateTimeOffset(src));

        config.NewConfig<ZonedDateTime, DateTimeOffset>()
            .MapWith(src => src.ToDateTimeOffset());


        config.NewConfig<DateTimeOffset?, ZonedDateTime?>()
            .MapWith(src => src.HasValue ? ZonedDateTime.FromDateTimeOffset(src.Value) : null);

        config.NewConfig<ZonedDateTime?, DateTimeOffset?>()
            .MapWith(src => src.HasValue ? src.Value.ToDateTimeOffset() : null);


        config.NewConfig<DateTimeOffset, OffsetDateTime>()
            .MapWith(src => OffsetDateTime.FromDateTimeOffset(src));

        config.NewConfig<OffsetDateTime, DateTimeOffset>()
            .MapWith(src => src.ToDateTimeOffset());


        config.NewConfig<DateTimeOffset?, OffsetDateTime?>()
            .MapWith(src => src.HasValue ? OffsetDateTime.FromDateTimeOffset(src.Value) : null);

        config.NewConfig<OffsetDateTime?, DateTimeOffset?>()
            .MapWith(src => src.HasValue ? src.Value.ToDateTimeOffset() : null);


        config.NewConfig<DateTimeOffset, LocalDate>()
            .MapWith(src => LocalDate.FromDateTime(src.Date));

        config.NewConfig<LocalDate, DateTimeOffset>()
            .MapWith(src => src.ToDateTimeUnspecified());


        config.NewConfig<DateTimeOffset?, LocalDate?>()
            .MapWith(src => src.HasValue ? LocalDate.FromDateTime(src.Value.Date) : null);

        config.NewConfig<LocalDate?, DateTimeOffset?>()
            .MapWith(src => src.HasValue ? src.Value.ToDateTimeUnspecified() : null);

        config.NewConfig<DateTime, LocalDate>()
            .MapWith(src => LocalDate.FromDateTime(src.Date));

        config.NewConfig<LocalDate, DateTime>()
            .MapWith(src => src.ToDateTimeUnspecified());


        config.NewConfig<DateTime?, LocalDate?>()
            .MapWith(src => src.HasValue ? LocalDate.FromDateTime(src.Value.Date) : null);

        config.NewConfig<LocalDate?, DateTime?>()
            .MapWith(src => src.HasValue ? src.Value.ToDateTimeUnspecified() : null);
    }    
}
