using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brigade.Infrastructure.Data.Configuration
{
    public class Chats : IEntityTypeConfiguration<Domain.Entities.Chats>
    {
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

            builder.Property(e => e.SecondParticipant)
                   .HasColumnName("SecondParticipantId") 
                   .IsRequired();

             builder.HasOne(e => e.Orders) 
                  .WithMany() 
                  .HasForeignKey(e => e.OrderId)
                  .OnDelete(DeleteBehavior.ClientSetNull); 

             builder.HasOne<User>() 
                  .WithMany() 
                  .HasForeignKey(e => e.FirstParticipantId)
                  .OnDelete(DeleteBehavior.ClientSetNull);

             builder.HasOne<User>() 
                  .WithMany() 
                  .HasForeignKey(e => e.SecondParticipantId)
                  .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}