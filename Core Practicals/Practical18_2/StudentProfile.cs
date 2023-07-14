using AutoMapper;
using DataAccess.Models;
using Practical18.Models;

namespace Practical18_2
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentViewModel>().ReverseMap();
        }
        
    }
}
