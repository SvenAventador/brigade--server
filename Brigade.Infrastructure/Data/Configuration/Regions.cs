using Brigade.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brigade.Infrastructure.Data.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="Domain.Entities.Regions"/> для Entity Framework Core.
    /// Определяет сопоставление свойств сущности с колонками таблицы 'Regions' в базе данных,
    /// включая первичный ключ, внешние ключи, связи и типы данных.
    /// </summary>
    public class Regions : IEntityTypeConfiguration<Domain.Entities.Regions>
    {
        /// <summary>
        /// Настраивает сущность <see cref="Domain.Entities.Regions"/>.
        /// </summary>
        /// <param name="builder"> Строитель, используемый для настройки сущности. </param>
        public void Configure(EntityTypeBuilder<Domain.Entities.Regions> builder)
        {
            builder.ToTable("Regions");

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
        }
    }
}