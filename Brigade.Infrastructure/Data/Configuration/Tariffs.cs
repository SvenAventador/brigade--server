using Brigade.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brigade.Infrastructure.Data.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="Domain.Entities.Tariffs"/> для Entity Framework Core.
    /// Определяет сопоставление свойств сущности с колонками таблицы 'Tariffs' в базе данных,
    /// включая первичный ключ, внешние ключи, связи и типы данных.
    /// </summary>
    public class Tariffs : IEntityTypeConfiguration<Domain.Entities.Tariffs>
    {
        /// <summary>
        /// Настраивает сущность <see cref="Domain.Entities.Tariffs"/>.
        /// </summary>
        /// <param name="builder"> Строитель, используемый для настройки сущности. </param>
        public void Configure(EntityTypeBuilder<Domain.Entities.Tariffs> builder)
        {
            builder.ToTable("Tariffs");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("Id")
                   .IsRequired(); 

            builder.OwnsOne(e => e.Name, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.Property(p => p.Value)
                                      .HasColumnName("Name") 
                                      .IsRequired()
                                      .HasMaxLength(Consts.MAX_NAME_LENGTH); 
            });

            builder.Property(e => e.Description)
                   .HasColumnName("Description")
                   .HasColumnType("TEXT"); 

            builder.Property(e => e.Price)
                   .HasColumnName("Price")
                   .HasColumnType("DECIMAL(10,2)") 
                   .IsRequired(); 

            builder.Property(e => e.DurationDays)
                   .HasColumnName("DurationDays")
                   .IsRequired(); 
        }
    }
}