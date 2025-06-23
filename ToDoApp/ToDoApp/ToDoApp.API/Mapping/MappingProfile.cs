using AutoMapper;
using ToDoApp.Core.Entities;
using ToDoApp.API.DTOs;

namespace ToDoApp.API.Mapping

{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoTask, ToDoTaskDto>().ReverseMap();
            CreateMap<TodoTask, ToDoTaskCreateDto>().ReverseMap();
            CreateMap<TodoTask, ToDoTaskUpdateDto>().ReverseMap();
        }
    }
}
