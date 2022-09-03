using BasicAPISettings.Api.Domain.Models.WeatherForecastAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasicAPISettings.Api.Data.Mappings
{
    internal class WeatherForecastMapping : IEntityTypeConfiguration<WeatherForecast>
    {
        public void Configure(EntityTypeBuilder<WeatherForecast> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("WeatherForecastId")
                .HasColumnType("int");

            builder.Property(p => p.Date).IsRequired(false); // can be null
            builder.Property(p => p.TemperatureC).IsRequired(); // not null
            builder.Property(p => p.TemperatureF).IsRequired();
            builder.Property(p => p.Summary).IsRequired();
        }
    }
}
