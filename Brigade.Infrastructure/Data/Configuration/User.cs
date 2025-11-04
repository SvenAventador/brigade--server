using Brigade.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brigade.Infrastructure.Data.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="Domain.Entities.User"/> для Entity Framework Core.
    /// Определяет сопоставление свойств сущности с колонками таблицы 'User' в базе данных,
    /// включая первичный ключ, внешние ключи, связи и типы данных.
    /// </summary>
    public class User : IEntityTypeConfiguration<Domain.Entities.User>
    {
        /// <summary>
        /// Настраивает сущность <see cref="Domain.Entities.User"/>.
        /// </summary>
        /// <param name="builder"> Строитель, используемый для настройки сущности. </param>
        public void Configure(EntityTypeBuilder<Domain.Entities.User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("Id")
                   .IsRequired();

            builder.OwnsOne(e => e.Email, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.Property(p => p.Value) 
                                      .HasColumnName("Email") 
                                      .IsRequired()
                                      .HasMaxLength(Consts.MAX_EMAIL_LENGTH); 
            });

            builder.Property(e => e.HashPassword)
                   .HasColumnName("HashPassword")
                   .IsRequired();

            builder.OwnsOne(e => e.FullName, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.Property(p => p.Value) 
                                      .HasColumnName("FullName") 
                                      .IsRequired()
                                      .HasMaxLength(Consts.MAX_NAME_LENGTH);
            });

            builder.OwnsOne(e => e.Phone, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.Property(p => p.Value) 
                                      .HasColumnName("Phone") 
                                      .IsRequired()
                                      .HasMaxLength(Consts.MAX_PHONE_LENGTH); 
            });

            builder.Property(e => e.PreferencesContact)
                   .HasColumnName("PreferedContact")
                   .HasConversion<string>()
                   .IsRequired(); 

            builder.Property(e => e.RegistrationDate)
                   .HasColumnName("RegistrationDate") 
                   .IsRequired(); 

            builder.Property(e => e.LastEnter)
                   .HasColumnName("LastEnter")
                   .IsRequired(); 

            builder.Property(e => e.IsConfirmed)
                   .HasColumnName("IsConfirmed")
                   .IsRequired(); 

            builder.Property(e => e.Photo)
                   .HasColumnName("Photo")
                   .HasColumnType("TEXT");

            builder.Property(e => e.EmailConfirmationToken)
                   .HasColumnName("EmailConfirmationToken"); 

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