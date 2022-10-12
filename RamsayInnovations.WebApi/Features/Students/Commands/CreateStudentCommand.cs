using AutoMapper;
using MediatR;
using RamsayInnovations.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RamsayInnovations.WebApi.Features.Students.Commands
{
  
    public class CreateStudentCommand : IRequest<CreateStudentCommandResponse>
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long Age { get; set; }
        public string Career { get; set; }
    }

    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, CreateStudentCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateStudentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreateStudentCommandResponse> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(studentDto);
            _unitOfWork.StudentRepository.Add(student);
            var student = await _unitOfWork.StudentRepository.FindAsync(request.StudentId);
        
            if (student == null)
            {
                return new CreateStudentCommandResponse($"El id {request.StudentId} del estudiante no existe en la BD");
            }
            return new CreateStudentCommandResponse(studentDto);

        }
    }

    public class CreateStudentCommandResponse
    {
        public StudentDto Student { get; set; }
        public string Message { get; set; }
        public CreateStudentCommandResponse(StudentDto studentDto)
        {
            Student = studentDto;
        }

        public CreateStudentCommandResponse(string message)
        {
            Message = message;
        }

    }
}
