using Brigade.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brigade.Infrastructure.Data.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="Domain.Entities.Orders"/> для Entity Framework Core.
    /// Определяет сопоставление свойств сущности с колонками таблицы 'Orders' в базе данных,
    /// включая первичный ключ, внешние ключи, связи и типы данных.
    /// </summary>
    public class Orders : IEntityTypeConfiguration<Domain.Entities.Orders>
    {
        /// <summary>
        /// Настраивает сущность <see cref="Domain.Entities.Orders"/>.
        /// </summary>
        /// <param name="builder"> Строитель, используемый для настройки сущности. </param>
        public void Configure(EntityTypeBuilder<Domain.Entities.Orders> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("Id")
                   .IsRequired();

            builder.OwnsOne(e => e.Title, navigation =>
            {
                navigation.Property(p => p.Value)
                          .HasColumnName("Title")
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

            builder.Property(e => e.Status)
                   .HasColumnName("Status")
                   .HasColumnType("TEXT")
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(e => e.CustomerId)
                   .HasColumnName("CustomerId")
                   .IsRequired();

            builder.HasOne(e => e.Customer)
                   .WithMany()
                   .HasForeignKey(e => e.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.RegionId)
                   .HasColumnName("RegionId")
                   .IsRequired();

            builder.HasOne(e => e.Region)
                   .WithMany()
                   .HasForeignKey(e => e.RegionId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}