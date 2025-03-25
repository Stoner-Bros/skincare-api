using APP.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace APP.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

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
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<SkinTest> SkinTests { get; set; }
        public virtual DbSet<SkinTestQuestion> SkinTestQuestions { get; set; }
        public virtual DbSet<SkinTestResult> SkinTestResults { get; set; }
        public virtual DbSet<SkinTestAnswer> SkinTestAnswers { get; set; }
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
                .HasForeignKey(a => a.AccountId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Blogs)
                .WithOne(a => a.Account)
                .HasForeignKey(a => a.AccountId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Comments)
                .WithOne(a => a.Account)
                .HasForeignKey(a => a.AccountId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Blog>()
                .HasMany(a => a.Comments)
                .WithOne(a => a.Blog)
                .HasForeignKey(a => a.BlogId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
                .HasOne(a => a.Treatment)
                .WithMany(a => a.Bookings)
                .HasForeignKey(a => a.TreatmentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
                .HasOne(a => a.SkinTherapist)
                .WithMany(a => a.Bookings)
                .HasForeignKey(a => a.SkinTherapistId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
                .HasOne(a => a.Staff)
                .WithMany(a => a.Bookings)
                .HasForeignKey(a => a.StaffId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
                .HasOne(a => a.Customer)
                .WithMany(a => a.Bookings)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
                .HasOne(a => a.Guest)
                .WithMany(a => a.Bookings)
                .HasForeignKey(a => a.GuestId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BookingTimeSlot>()
                .HasOne(a => a.TimeSlot)
                .WithMany(a => a.BookingTimeSlots)
                .HasForeignKey(a => a.TimeSlotId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BookingTimeSlot>()
               .HasOne(a => a.Booking)
               .WithMany(a => a.BookingTimeSlots)
               .HasForeignKey(a => a.BookingId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ConsultingForm>()
               .HasOne(a => a.Staff)
               .WithMany(a => a.ConsultingForms)
               .HasForeignKey(a => a.StaffId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ConsultingForm>()
               .HasOne(a => a.Customer)
               .WithMany(a => a.ConsultingForms)
               .HasForeignKey(a => a.CustomerId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ConsultingForm>()
               .HasOne(a => a.Guest)
               .WithMany(a => a.ConsultingForms)
               .HasForeignKey(a => a.GuestId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Feedback>()
                .HasOne(a => a.Booking)
                .WithOne(a => a.Feedback)
                .HasForeignKey<Feedback>(a => a.BookingId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FeedbackReply>()
               .HasOne(a => a.Staff)
               .WithMany(a => a.FeedbackReplies)
               .HasForeignKey(a => a.StaffId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FeedbackReply>()
               .HasOne(a => a.Feedback)
               .WithMany(a => a.FeedbackReplies)
               .HasForeignKey(a => a.FeedbackId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasOne(a => a.Booking)
                .WithOne(a => a.Payment)
                .HasForeignKey<Payment>(a => a.BookingId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SkinTest>()
               .HasMany(a => a.SkinTestQuestions)
               .WithOne(a => a.SkinTest)
               .HasForeignKey(a => a.SkinTestId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SkinTest>()
               .HasMany(a => a.SkinTestAnswer)
               .WithOne(a => a.SkinTest)
               .HasForeignKey(a => a.SkinTestId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SkinTestResult>()
               .HasOne(a => a.Customer)
               .WithMany(a => a.SkinTestResults)
               .HasForeignKey(a => a.CustomerId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SkinTestResult>()
               .HasOne(a => a.Guest)
               .WithMany(a => a.SkinTestResults)
               .HasForeignKey(a => a.GuestId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SkinTestResult>()
                .HasOne(a => a.SkinTestAnswer)
                .WithOne(a => a.SkinTestResults)
                .HasForeignKey<SkinTestResult>(a => a.SkinTestAnswerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Customer>()
                .HasOne(a => a.Account)
                .WithOne(a => a.Customer)
                .HasForeignKey<Customer>(a => a.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SkinTherapist>()
                .HasOne(a => a.Account)
                .WithOne(a => a.SkinTherapist)
                .HasForeignKey<SkinTherapist>(a => a.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SkinTherapist>()
              .HasMany(a => a.SkinTherapistSchedules)
              .WithOne(a => a.SkinTherapist)
              .HasForeignKey(a => a.SkinTherapistId)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Staff>()
                .HasOne(a => a.Account)
                .WithOne(a => a.Staff)
                .HasForeignKey<Staff>(a => a.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Treatment>()
                .HasOne(a => a.Service)
                .WithMany(a => a.Treatments)
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
                 .HasOne(a => a.TreatmentResult)
                 .WithOne(a => a.Booking)
                 .HasForeignKey<TreatmentResult>(a => a.BookingId)
                 .OnDelete(DeleteBehavior.Cascade);

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
