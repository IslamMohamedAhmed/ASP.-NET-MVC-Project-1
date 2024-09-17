using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.Models.ViewModels;

namespace Demo.PL.ModelProfiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile() { 
        
        CreateMap<DepartmentViewModel,Department>().ReverseMap();
        }

    }
}
