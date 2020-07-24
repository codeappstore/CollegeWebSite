using AutoMapper;
using CollegeWebsite.DataAccess.Pages.Dtos;
using CollegeWebsite.DataAccess.Pages.Model;

namespace CollegeWebsite.DataAccess.Pages.Profiles
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
