using Brigade.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brigade.Infrastructure.Data.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="Domain.Entities.SupportTickets"/> для Entity Framework Core.
    /// Определяет сопоставление свойств сущности с колонками таблицы 'SupportTickets' в базе данных,
    /// включая первичный ключ, внешние ключи, связи и типы данных.
    /// </summary>
    public class SupportTickets : IEntityTypeConfiguration<Domain.Entities.SupportTickets>
    {
        /// <summary>
        /// Настраивает сущность <see cref="Domain.Entities.SupportTickets"/>.
        /// </summary>
        /// <param name="builder"> Строитель, используемый для настройки сущности. </param>
        public void Configure(EntityTypeBuilder<Domain.Entities.SupportTickets> builder)
        {
            builder.ToTable("SupportTickets");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("Id")
                   .IsRequired(); 

            builder.OwnsOne(e => e.Subject, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.Property(p => p.Value)
                                      .HasColumnName("Subject") 
                                      .IsRequired()
                                      .HasMaxLength(Consts.MAX_NAME_LENGTH); 
            });

            builder.Property(e => e.Description)
                   .HasColumnName("Description")
                   .HasColumnType("TEXT");

            builder.Property(e => e.Status)
                   .HasColumnName("Status")
                   .HasColumnType("TEXT")
                   .HasConversion<string>()
                   .IsRequired();

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