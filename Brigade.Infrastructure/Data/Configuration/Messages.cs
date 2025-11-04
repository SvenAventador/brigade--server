using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brigade.Infrastructure.Data.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="Domain.Entities.Messages"/> для Entity Framework Core.
    /// Определяет сопоставление свойств сущности с колонками таблицы 'Messages' в базе данных,
    /// включая первичный ключ, внешние ключи, связи и типы данных.
    /// </summary>
    public class Messages : IEntityTypeConfiguration<Domain.Entities.Messages>
    {
        /// <summary>
        /// Настраивает сущность <see cref="Domain.Entities.Messages"/>.
        /// </summary>
        /// <param name="builder"> Строитель, используемый для настройки сущности. </param>
        public void Configure(EntityTypeBuilder<Domain.Entities.Messages> builder)
        {
            builder.ToTable("Messages");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("Id")
                   .IsRequired();

            builder.Property(e => e.Text)
                   .HasColumnName("Text")
                   .IsRequired();

            builder.Property(e => e.SendAt)
                   .HasColumnName("SendAt")
                   .IsRequired();

            builder.Property(e => e.IsRead)
                   .HasColumnName("IsRead")
                   .IsRequired();

            builder.Property(e => e.SenderId)
                   .HasColumnName("SenderId")
                   .IsRequired();

            builder.HasOne(e => e.Sender)
                   .WithMany()
                   .HasForeignKey(e => e.SenderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.ChatId)
                   .HasColumnName("ChatId")
                   .IsRequired();

            builder.HasOne(e => e.Chat)
                   .WithMany()
                   .HasForeignKey(e => e.ChatId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}