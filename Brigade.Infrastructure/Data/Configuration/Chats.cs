using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brigade.Infrastructure.Data.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="Domain.Entities.Chats"/> для Entity Framework Core.
    /// Определяет сопоставление свойств сущности с колонками таблицы 'Chats' в базе данных,
    /// включая первичный ключ, внешние ключи, связи и типы данных.
    /// </summary>
    public class Chats : IEntityTypeConfiguration<Domain.Entities.Chats>
    {
        /// <summary>
        /// Настраивает сущность <see cref="Domain.Entities.Chats"/>.
        /// </summary>
        /// <param name="builder"> Строитель, используемый для настройки сущности. </param>
        public void Configure(EntityTypeBuilder<Domain.Entities.Chats> builder)
        {
            builder.ToTable("Chats");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("Id")
                   .IsRequired();

            builder.Property(e => e.OrderId)
                   .HasColumnName("OrderId");

            builder.Property(e => e.FirstParticipantId)
                   .HasColumnName("FirstParticipantId")
                   .IsRequired();

            builder.Property(e => e.SecondParticipantId)
                   .HasColumnName("SecondParticipantId")
                   .IsRequired();

            builder.HasOne(e => e.Orders)
                   .WithMany()
                   .HasForeignKey(e => e.OrderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.FirstParticipant)
                   .WithMany()
                   .HasForeignKey(e => e.FirstParticipantId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.SecondParticipant)
                   .WithMany()
                   .HasForeignKey(e => e.SecondParticipantId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}