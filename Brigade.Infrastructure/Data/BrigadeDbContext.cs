using Brigade.Domain.Entities;
using Brigade.Domain.Enums;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<RoleType>("role_type_enum");
            modelBuilder.HasPostgresEnum<OrderApplicationStatus>("order_application_status_enum");
            modelBuilder.HasPostgresEnum<OrderStatus>("order_status_enum");
            modelBuilder.HasPostgresEnum<PreferencesContactMethod>("preferences_contact_method_enum");
            modelBuilder.HasPostgresEnum<SupportStatus>("support_status_enum");
            modelBuilder.HasPostgresEnum<TariffStatus>("tariff_status_enum");

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BrigadeDbContext).Assembly);
        }
    }
}