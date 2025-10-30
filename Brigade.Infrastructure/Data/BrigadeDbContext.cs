using Brigade.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Brigade.Infrastructure.Data
{
    public class BrigadeDbContext : DbContext
    {
        public BrigadeDbContext(DbContextOptions<BrigadeDbContext> options) 
            : base(options) { }

        #region Настройка DataSet для каждой сущности из Domain.Entities

        public DbSet<Chats> Chats { get; set; }
        public DbSet<CompanyProfile> CompanyProfiles { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<OrderApplication> OrderApplications { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Regions> Regions { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SupportTickets> SupportTickets { get; set; }
        public DbSet<Tariffs> Tariffs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserTariffs> UserTariffs { get; set; }

        #endregion

    }
}