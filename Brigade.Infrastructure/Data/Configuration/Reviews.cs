using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brigade.Infrastructure.Data.Configuration 
{
    /// <summary>
    /// Конфигурация сущности <see cref="Domain.Entities.Reviews"/> для Entity Framework Core.
    /// Определяет сопоставление свойств сущности с колонками таблицы 'Chats' в базе данных,
    /// включая первичный ключ, внешние ключи, связи и типы данных.
    /// </summary>
    public class Reviews : IEntityTypeConfiguration<Domain.Entities.Reviews>
    {
        /// <summary>
        /// Настраивает сущность <see cref="Domain.Entities.Reviews"/>.
        /// </summary>
        /// <param name="builder"> Строитель, используемый для настройки сущности. </param>
        public void Configure(EntityTypeBuilder<Domain.Entities.Reviews> builder)
        {
            builder.ToTable("Reviews");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("Id")
                   .IsRequired(); 

            builder.Property(e => e.Comment)
                   .HasColumnName("Comment")
                   .HasColumnType("TEXT")
                   .IsRequired(); 

            builder.OwnsOne(e => e.Rating, ownedNavigationBuilder =>
            {

                ownedNavigationBuilder.Property(p => p.Value)
                                      .HasColumnName("Rating") 
                                      .IsRequired();
            });

            builder.Property(e => e.OrderId)
                   .HasColumnName("OrderId")
                   .IsRequired(); 

            builder.HasOne(e => e.Order)
                   .WithMany() 
                   .HasForeignKey(e => e.OrderId)
                   .OnDelete(DeleteBehavior.Restrict); 

            builder.Property(e => e.AuthorId)
                   .HasColumnName("AuthorId")
                   .IsRequired(); 

            builder.HasOne(e => e.Author) 
                   .WithMany() 
                   .HasForeignKey(e => e.AuthorId)
                   .OnDelete(DeleteBehavior.Restrict); 

            builder.Property(e => e.TargetId)
                   .HasColumnName("TargetId")
                   .IsRequired(); 

            builder.HasOne(e => e.Target) 
                   .WithMany() 
                   .HasForeignKey(e => e.TargetId)
                   .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}