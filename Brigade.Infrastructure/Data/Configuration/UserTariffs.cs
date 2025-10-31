using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brigade.Infrastructure.Data.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="Domain.Entities.UserTariffs"/> для Entity Framework Core.
    /// Определяет сопоставление свойств сущности с колонками таблицы 'Chats' в базе данных,
    /// включая первичный ключ, внешние ключи, связи и типы данных.
    /// </summary>
    public class UserTariffs : IEntityTypeConfiguration<Domain.Entities.UserTariffs>
    {
        /// <summary>
        /// Настраивает сущность <see cref="Domain.Entities.UserTariffs"/>.
        /// </summary>
        /// <param name="builder"> Строитель, используемый для настройки сущности. </param>
        public void Configure(EntityTypeBuilder<Domain.Entities.UserTariffs> builder)
        {
            builder.ToTable("UserTariffs");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("Id")
                   .IsRequired(); 

            builder.Property(e => e.Amount)
                   .HasColumnName("Amount") 
                   .HasColumnType("DECIMAL(10,2)") 
                   .IsRequired(); 

            builder.Property(e => e.Status)
                   .HasColumnName("Status")
                   .HasConversion<string>()
                   .HasColumnType("tariff_status_enum")
                   .IsRequired(); 

            builder.OwnsOne(e => e.ValidityPeriod, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.Property(p => p.Start)
                                      .HasColumnName("StartDate") 
                                      .IsRequired();

                ownedNavigationBuilder.Property(p => p.End)
                                      .HasColumnName("EndDate")
                                      .IsRequired();
            });

            builder.Property(e => e.UserId)
                   .HasColumnName("UserId")
                   .IsRequired(); 

            builder.HasOne(e => e.User) 
                   .WithMany() 
                   .HasForeignKey(e => e.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.TariffId)
                   .HasColumnName("TariffId")
                   .IsRequired(); 

            builder.HasOne(e => e.Tariff) 
                   .WithMany() 
                   .HasForeignKey(e => e.TariffId)
                   .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}