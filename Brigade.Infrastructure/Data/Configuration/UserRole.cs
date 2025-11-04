using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brigade.Infrastructure.Data.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="Domain.Entities.UserRole"/> для Entity Framework Core.
    /// Определяет сопоставление свойств сущности с колонками таблицы 'UserRole' в базе данных,
    /// включая первичный ключ, внешние ключи, связи и типы данных.
    /// </summary>
    public class UserRole : IEntityTypeConfiguration<Domain.Entities.UserRole>
    {
        /// <summary>
        /// Настраивает сущность <see cref="Domain.Entities.UserRole"/>.
        /// </summary>
        /// <param name="builder"> Строитель, используемый для настройки сущности. </param>
        public void Configure(EntityTypeBuilder<Domain.Entities.UserRole> builder)
        {
            builder.ToTable("UserRoles");

            builder.HasKey(e => new { 
                e.UserId,
                e.RoleId 
            });

            builder.Property(e => e.UserId)
                   .HasColumnName("UserId")
                   .IsRequired(); 

            builder.Property(e => e.RoleId)
                   .HasColumnName("RoleId")
                   .IsRequired(); 

            builder.Property(e => e.IsActive)
                   .HasColumnName("IsActive") 
                   .IsRequired(); 

            builder.HasOne(e => e.User) 
                   .WithMany(u => u.UserRoles) 
                   .HasForeignKey(e => e.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Role)
                   .WithMany(u => u.UserRoles) 
                   .HasForeignKey(e => e.RoleId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}