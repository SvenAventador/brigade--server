using Brigade.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brigade.Infrastructure.Data.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="Domain.Entities.OrderApplication"/> для Entity Framework Core.
    /// Определяет сопоставление свойств сущности с колонками таблицы 'OrderApplication' в базе данных,
    /// включая первичный ключ, внешние ключи, связи и типы данных.
    /// </summary>
    public class OrderApplication : IEntityTypeConfiguration<Domain.Entities.OrderApplication>
    {
        /// <summary>
        /// Настраивает сущность <see cref="Domain.Entities.OrderApplication"/>.
        /// </summary>
        /// <param name="builder"> Строитель, используемый для настройки сущности. </param>
        public void Configure(EntityTypeBuilder<Domain.Entities.OrderApplication> builder)
        {
            builder.ToTable("OrderApplications");

            builder.HasKey(e => new { 
                e.OrderId, 
                e.PerformerId 
            });

            builder.Property(e => e.Status)
                   .HasColumnName("Status")
                   .HasColumnType("TEXT")
                   .HasConversion<string>()
                   .IsRequired();

            builder.HasOne(e => e.Order) 
                  .WithMany() 
                  .HasForeignKey(e => e.OrderId) 
                  .OnDelete(DeleteBehavior.Restrict); 
            
            builder.HasOne(e => e.Performer) 
                   .WithMany() 
                   .HasForeignKey(e => e.PerformerId) 
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}