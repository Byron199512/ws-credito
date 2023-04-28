using AutoMapper;
using WebApplication1.Controllers.Entities;
using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Utilities
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ClientDTO, Client>().ReverseMap();
            CreateMap<DocumentDTO, DocumentClient>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<UserResponseDTO, User>().ReverseMap();
            CreateMap<RolDTO, Rol>().ReverseMap();
            CreateMap<StatusDTO, Status>().ReverseMap();
            CreateMap<RequestTypeDTO, RequestType>().ReverseMap();
            CreateMap<RequestDTO, Request>().ReverseMap();
            CreateMap<RequestResponseDTO, Request>().ReverseMap();




        }
    }
}
