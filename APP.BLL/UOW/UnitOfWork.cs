using APP.BLL.Interfaces;
using APP.DAL;
using APP.DAL.Implements;

namespace APP.BLL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;

        private readonly AppDbContext _context;
        public IAccountRepository Accounts { get; private set; }
        public IAccountInfoRepository AccountInfos { get; private set; }
        public IRefeshTokenRepository RefeshTokens { get; private set; }
        public IExpiredTokenRepository ExpiredTokens { get; private set; }
        public IServiceRepository Services { get; private set; }
        public ITreatmentRepository Treatments { get; private set; }
        public IBlogRepository Blogs { get; private set; }
        public ISkinTherapistRepository SkinTherapists { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public IStaffRepository Staffs { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public ITimeSlotRepository TimeSlots { get; private set; }
        public ISkinTestRepository SkinTests { get; private set; }
        public ISkinTestQuestionRepository SkinTestQuestions { get; private set; }
        public IBookingRepository Bookings { get; private set; }
        public IBookingTimeSlotRepository BookingTimeSlots { get; private set; }
        public ISkinTestAnswerRepository SkinTestAnswers { get; private set; }
        public IGuestRepository Guests { get; private set; }
        public ISkinTestResultRepository SkinTestResults { get; private set; }
        public IConsultingFormRepository ConsultingForms { get; private set; }
        public ISkinTherapistScheduleRepository SkinTherapistSchedules { get; private set; }
        public IPaymentRepository Payments { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Accounts = new AccountRepository(context);
            AccountInfos = new AccountInfoRepository(context);
            RefeshTokens = new RefeshTokenRepository(context);
            ExpiredTokens = new ExpiredTokenRepository(context);
            Services = new ServiceRepository(context);
            Treatments = new TreatmentRepository(context);
            Blogs = new BlogRepository(context);
            SkinTherapists = new SkinTherapistRepository(context);
            Customers = new CustomerRepository(context);
            Comments = new CommentRepository(context);
            Staffs = new StaffRepository(context);
            TimeSlots = new TimeSlotRepository(context);
            SkinTests = new SkinTestRepository(context);
            SkinTestQuestions = new SkinTestQuestionRepository(context);
            Bookings = new BookingRepository(context);
            BookingTimeSlots = new BookingTimeSlotRepository(context);
            SkinTestAnswers = new SkinTestAnswerRepository(context);
            Guests = new GuestRepository(context);
            SkinTestResults = new SkinTestResultRepository(context);
            ConsultingForms = new ConsultingFormRepository(context);
            SkinTherapistSchedules = new SkinTherapistScheduleRepository(context);
            Payments = new PaymentRepository(context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<int> SaveWithTransactionAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var result = await _context.SaveChangesAsync(CancellationToken.None);
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<int> SaveWithTransactionAsync(Func<Task> operation)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await operation();
                var result = await _context.SaveChangesAsync(CancellationToken.None);
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Ngăn GC gọi finalizer
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Giải phóng các tài nguyên quản lý
                    _context?.Dispose();
                }

                // Giải phóng tài nguyên không quản lý (nếu có)

                _disposed = true;
            }
        }
    }
}
