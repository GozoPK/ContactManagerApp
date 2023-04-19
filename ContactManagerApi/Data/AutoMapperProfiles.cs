using AutoMapper;
using ContactManagerApi.DTOs;
using ContactManagerApi.Entities;

namespace ContactManagerApi.Data
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserToReturnDto>();

            CreateMap<Contact, ContactDto>()
                .ReverseMap();

            CreateMap<ContactForUpdateDto, Contact>();

            CreateMap<Email, EmailDto>()
                .ReverseMap();
        }
    }
}
