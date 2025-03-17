using APP.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace APP.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //try
            //{
            //    var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            //    if (databaseCreator != null)
            //    {
            //        if (!databaseCreator.CanConnect()) databaseCreator.Create();
            //        if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountInfo> AccountInfos { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<BookingTimeSlot> BookingTimeSlots { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<ConsultingForm> ConsultingForms { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<FeedbackReply> FeedbackReplies { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<SkinTest> SkinTests { get; set; }
        public virtual DbSet<SkinTestQuestion> SkinTestQuestions { get; set; }
        public virtual DbSet<SkinTestResult> SkinTestResults { get; set; }
        public virtual DbSet<SkinTherapist> SkinTherapists { get; set; }
        public virtual DbSet<SkinTherapistSchedule> SkinTherapistSchedules { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<TimeSlot> TimeSlots { get; set; }
        public virtual DbSet<Treatment> Treatments { get; set; }
        public virtual DbSet<TreatmentResult> TreatmentResults { get; set; }

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<ExpiredToken> ExpiredTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasOne(a => a.AccountInfo)
                .WithOne(a => a.Account)
                .HasForeignKey<AccountInfo>(a => a.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Notifications)
                .WithOne(a => a.Account)
                .HasForeignKey(a => a.AccountId);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Blogs)
                .WithOne(a => a.Account)
                .HasForeignKey(a => a.AccountId);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Comments)
                .WithOne(a => a.Account)
                .HasForeignKey(a => a.AccountId);

            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.Account)
                .WithMany(a => a.RefreshTokens)
                .HasForeignKey(rt => rt.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExpiredToken>()
                .HasOne(rt => rt.Account)
                .WithMany(a => a.ExpiredTokens)
                .HasForeignKey(rt => rt.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Email)
                .IsUnique();

            modelBuilder.Entity<Account>()
                .ToTable(b =>
                {
                    b.HasCheckConstraint("CK_Email_Length", "LEN([email]) >= 6");
                    b.HasCheckConstraint("CK_Email_Valid", "CHARINDEX('@', [email]) > 0");
                    b.HasCheckConstraint("CK_Role_Valid", "[role] IN ('Customer', 'Skin Therapist', 'Staff', 'Manager')");
                });

            modelBuilder.Entity<AccountInfo>()
                .ToTable(b =>
                {
                    b.HasCheckConstraint("CK_Phone_Valid", "LEN([phone]) = 10");
                    b.HasCheckConstraint("CK_Fullname_Length", "LEN([full_name]) >= 6");
                });
        }
    }
}
