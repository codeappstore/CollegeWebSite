using AutoMapper;
using CollegeWebsite.DataAccess.Models.Pages.Dtos;
using CollegeWebsite.DataAccess.Models.Pages.Model;

namespace CollegeWebsite.DataAccess.Models.Pages.Profiles
{
    public class StaticPagesProfile : Profile
    {
        public StaticPagesProfile()
        {
            CreateMap<StaticPages, StaticPagesReadDto>();
            CreateMap<StaticPagesCreateDto, StaticPages>();
            CreateMap<StaticPagesUpdateDto, StaticPages>();
            CreateMap<StaticPages, StaticPagesUpdateDto>();
        }
    }
}
