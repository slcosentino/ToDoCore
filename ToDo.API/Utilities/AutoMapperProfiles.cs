using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.DTOs;
using Entities = ToDo.Entities;


namespace ToDo.API.Utilities
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {            
            CreateMap<ToDoDTO, Entities.ToDo>()
                .ForMember(a => a.FolderId, b => b.MapFrom( a => a.Folder.Id))
                //.ForMember(dest => dest.Folder.Id, origin => origin.MapFrom<Entities.ToDo>(e => e.FolderId))
                //.ForMember(x => x.Folder, x=> x.MapFrom(dto => dto.IdFolder)  )
                .ReverseMap();

            CreateMap<Entities.Folder, FolderDTO>().ReverseMap();
        }
    }
}
