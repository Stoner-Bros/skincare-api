using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using APP.Entity.Entities;
using AutoMapper;

namespace APP.Entity.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountCreationRequest, Account>();
            CreateMap<AccountCreationRequest, AccountInfo>();
            CreateMap<AccountUpdationRequest, Account>();
            CreateMap<AccountUpdationRequest, AccountInfo>();
            CreateMap<Account, AccountResponse>();

            CreateMap<ServiceCreationRequest, Service>();
            CreateMap<ServiceUpdationRequest, Service>();
            CreateMap<Service, ServiceResponse>();

            CreateMap<TreatmentRequest, Treatment>();
            CreateMap<Treatment, TreatmentResponse>();

            CreateMap<BlogCreationRequest, Blog>();
            CreateMap<BlogUpdationRequest, Blog>();
            CreateMap<Blog, BlogResponse>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Account.AccountInfo.FullName));

            CreateMap<SkinTherapistCreationRequest, SkinTherapist>();
            CreateMap<SkinTherapistUpdationRequest, SkinTherapist>();
            CreateMap<SkinTherapist, SkinTherapistResponse>();

            CreateMap<CommentCreationRequest, Comment>();
            CreateMap<CommentUpdationRequest, Comment>();
            CreateMap<Comment, CommentResponse>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Account.AccountInfo.FullName));

            CreateMap<Customer, CustomerResponse>();

            CreateMap<Customer, CustomerAnswerResponse>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Account.AccountInfo.Phone))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Account.AccountInfo.FullName))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Account.AccountInfo.AccountId));

            CreateMap<Guest, GuestAnswerResponse>();

            CreateMap<StaffCreationRequest, Staff>();
            CreateMap<StaffUpdationRequest, Staff>();
            CreateMap<Staff, StaffResponse>();

            CreateMap<TimeSlot, TimeSlotResponse>();

            CreateMap<SkinTestCreationRequest, SkinTest>();
            CreateMap<SkinTestUpdationRequest, SkinTest>();
            CreateMap<SkinTest, SkinTestResponse>()
                .ForMember(dest => dest.SkinTestQuestions, opt => opt.MapFrom(src => src.SkinTestQuestions));

            CreateMap<SkinTestQuestionCreationRequest, SkinTestQuestion>();
            CreateMap<SkinTestQuestionUpdationRequest, SkinTestQuestion>();
            CreateMap<SkinTestQuestion, SkinTestQuestionResponse>();

            CreateMap<BookingCreationRequest, Booking>();
            CreateMap<BookingUpdationRequest, Booking>();

            CreateMap<SkinTestAnswer, SkinTestAnswerResponse>()
                .ForMember(dest => dest.SkinTest, opt => opt.MapFrom(src => src.SkinTest))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
                .ForMember(dest => dest.Guest, opt => opt.MapFrom(src => src.Guest));
            CreateMap<SkinTestAnswerRequest, SkinTestAnswer>();

            CreateMap<SkinTestResult, SkinTestResultResponse>();
            CreateMap<SkinTestResultRequest, SkinTestResult>();

            CreateMap<ConsultingFormCreationRequest, ConsultingForm>();
            CreateMap<ConsultingFormUpdationRequest, ConsultingForm>();

            CreateMap<SkinTherapistScheduleCreationRequest, SkinTherapistSchedule>();
            CreateMap<SkinTherapistScheduleUpdationRequest, SkinTherapistSchedule>();
            CreateMap<SkinTherapistSchedule, SkinTherapistScheduleResponse>();

            CreateMap<FeedbackCreationRequest, Feedback>();
            CreateMap<FeedbackUpdationRequest, Feedback>();
            CreateMap<Feedback, FeedbackResponse>()
                .ForMember(dest => dest.FeedbackReplies, opt => opt.MapFrom(src => src.FeedbackReplies));


            CreateMap<FeedbackReplyCreationRequest, FeedbackReply>();
            CreateMap<FeedbackReplyUpdationRequest, FeedbackReply>();
            CreateMap<FeedbackReply, FeedbackReplyResponse>()
                .ForMember(dest => dest.StaffName, opt => opt.MapFrom(src => src.Staff.Account.AccountInfo.FullName));

        }
    }
}
