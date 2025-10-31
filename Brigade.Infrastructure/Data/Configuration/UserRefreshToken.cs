using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brigade.Infrastructure.Data.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="Domain.Entities.UserRefreshToken"/> для Entity Framework Core.
    /// Определяет сопоставление свойств сущности с колонками таблицы 'Chats' в базе данных,
    /// включая первичный ключ, внешние ключи, связи и типы данных.
    /// </summary>
    public class UserRefreshToken : IEntityTypeConfiguration<Domain.Entities.UserRefreshToken>
    {
        /// <summary>
        /// Настраивает сущность <see cref="Domain.Entities.UserRefreshToken"/>.
        /// </summary>
        /// <param name="builder"> Строитель, используемый для настройки сущности. </param>
        public void Configure(EntityTypeBuilder<Domain.Entities.UserRefreshToken> builder)
        {
            builder.ToTable("UserRefreshTokens");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("Id")
                   .IsRequired(); 

            builder.Property(e => e.UserId)
                   .HasColumnName("UserId")
                   .IsRequired();

            builder.Property(e => e.RefreshToken)
                   .HasColumnName("RefreshToken")
                   .IsRequired();

            builder.Property(e => e.CreatedAt)
                   .HasColumnName("CreatedAt")
                   .IsRequired();

            builder.Property(e => e.ExpiresAt)
                   .HasColumnName("ExpiresAt")
                   .IsRequired(); 

            builder.Property(e => e.IsExpires)
                   .HasColumnName("IsExpires")
                   .IsRequired(); 

            builder.Property(e => e.RevokedAt)
                   .HasColumnName("RevokedAt");

            builder.Property(e => e.IsRevoked)
                   .HasColumnName("IsRevoked")
                   .IsRequired(); 

            builder.HasOne(e => e.User) 
                   .WithMany()
                   .HasForeignKey(e => e.UserId)
                   .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}