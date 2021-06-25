using AutoMapper;
using ToDo.API.DTOs;
using ToDo.DTOs;



namespace ToDo.API.Utilities
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Entities.UserCredential, UserCredentialDTO>().ReverseMap();
            CreateMap<ToDoDTO, Entities.ToDo>().ReverseMap();
            CreateMap<Entities.Folder, FolderDTO>().ReverseMap();
            CreateMap<Entities.Pagination, PaginationDTO>().ReverseMap();
            CreateMap<Entities.AppToken, AppTokenDTO>().ReverseMap();
        }
    }
}
