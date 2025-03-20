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
        }
    }
}
