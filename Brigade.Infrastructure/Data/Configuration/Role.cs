using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brigade.Infrastructure.Data.Configuration
{
    /// <summary>
    /// Конфигурация сущности <see cref="Domain.Entities.Reviews"/> для Entity Framework Core.
    /// Определяет сопоставление свойств сущности с колонками таблицы 'Chats' в базе данных,
    /// включая первичный ключ, внешние ключи, связи и типы данных.
    /// </summary>
    public class Role : IEntityTypeConfiguration<Domain.Entities.Role>
    {
        /// <summary>
        /// Настраивает сущность <see cref="Domain.Entities.Reviews"/>.
        /// </summary>
        /// <param name="builder"> Строитель, используемый для настройки сущности. </param>
        public void Configure(EntityTypeBuilder<Domain.Entities.Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("Id")
                   .IsRequired(); 

            builder.Property(e => e.Type)
                   .HasColumnName("Type")
                   .HasColumnType("role_type_enum")
                   .IsRequired(); 
        }
    }
}