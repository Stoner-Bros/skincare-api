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

            CreateMap<StaffCreationRequest, Staff>();
            CreateMap<StaffUpdationRequest, Staff>();
            CreateMap<Staff, StaffResponse>();
        }
    }
}
