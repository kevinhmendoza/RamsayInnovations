using AutoMapper;
using RamsayInnovations.Domain;
using RamsayInnovations.WebApi.Features.Students.Commands;
using RamsayInnovations.WebApi.Mappers.StudentMap;

namespace RamsayInnovations.WebApi.Mappers
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentDto, Student>();
            CreateMap<Student, StudentDto>();
            CreateMap<UpdateStudentCommand, Student>();

        }
    }
}
