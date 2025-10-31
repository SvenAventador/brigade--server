using Brigade.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brigade.Infrastructure.Data.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="Domain.Entities.CompanyProfile"/> для Entity Framework Core.
    /// Определяет сопоставление свойств сущности с колонками таблицы 'Chats' в базе данных,
    /// включая первичный ключ, внешние ключи, связи и типы данных.
    /// </summary>
    public class CompanyProfile : IEntityTypeConfiguration<Domain.Entities.CompanyProfile>
    {
        /// <summary>
        /// Настраивает сущность <see cref="Domain.Entities.CompanyProfile"/>.
        /// </summary>
        /// <param name="builder"> Строитель, используемый для настройки сущности. </param>
        public void Configure(EntityTypeBuilder<Domain.Entities.CompanyProfile> builder)
        {
            builder.ToTable("CompanyProfiles");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("Id")
                   .IsRequired();

            builder.Property(e => e.Description)
                   .HasColumnName("Description")
                   .HasColumnType("TEXT");

            builder.OwnsOne(e => e.LegalName, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.Property(p => p.Value) 
                                      .HasColumnName("LegalName") 
                                      .IsRequired()
                                      .HasMaxLength(Consts.MAX_NAME_LENGTH); 
            });

            builder.OwnsOne(e => e.INN, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.Property(p => p.Value) 
                                      .HasColumnName("INN")
                                      .IsRequired()
                                      .HasMaxLength(Consts.INN_LENGTH) 
                                      .IsFixedLength();
            });

            builder.Property(e => e.UserId)
                   .HasColumnName("UserId") 
                   .IsRequired();

            builder.HasOne(e => e.User)
                   .WithMany()
                   .HasForeignKey(e => e.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}