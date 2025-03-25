using APP.DAL.Implements;

namespace APP.BLL.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts { get; }
        IAccountInfoRepository AccountInfos { get; }
        IServiceRepository Services { get; }
        ITreatmentRepository Treatments { get; }
        IRefeshTokenRepository RefeshTokens { get; }
        IExpiredTokenRepository ExpiredTokens { get; }
        IBlogRepository Blogs { get; }
        ISkinTherapistRepository SkinTherapists { get; }
        ICustomerRepository Customers { get; }
        ICommentRepository Comments { get; }
        IStaffRepository Staffs { get; }
        ITimeSlotRepository TimeSlots { get; }
        ISkinTestRepository SkinTests { get; }
        ISkinTestQuestionRepository SkinTestQuestions { get; }
        IBookingRepository Bookings { get; }
        IBookingTimeSlotRepository BookingTimeSlots { get; }
        ISkinTestAnswerRepository SkinTestAnswers { get; }
        IGuestRepository Guests { get; }
        Task<int> SaveAsync();
        Task<int> SaveWithTransactionAsync();
        Task<int> SaveWithTransactionAsync(Func<Task> operation);
    }

}
