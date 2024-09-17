using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.Models.ViewModels;

namespace Demo.PL.ModelProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile() {

            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
