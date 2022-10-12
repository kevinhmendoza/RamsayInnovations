using AutoMapper;
using MediatR;
using RamsayInnovations.Domain.SeedWorks;
using RamsayInnovations.WebApi.Mappers.StudentMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RamsayInnovations.WebApi.Features.Students.Queries
{

    public class GetStudentQuery : IRequest<GetStudentQueryResponse>
    {
        public long StudentId { get; set; }
    }

    public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, GetStudentQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetStudentQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetStudentQueryResponse> Handle(GetStudentQuery request, CancellationToken cancellationToken)
        {
            var student = await _unitOfWork.StudentRepository.FindAsync(request.StudentId);
            if (student == null)
            {
                return new GetStudentQueryResponse($"El id {request.StudentId} del estudiante no existe en la BD");
            }
            var studentDto = _mapper.Map<StudentDto>(student);         
            return new GetStudentQueryResponse(studentDto);

        }
    }

    public class GetStudentQueryResponse
    {
        public StudentDto Student { get; set; }
        public string Message { get; set; }
        public GetStudentQueryResponse(StudentDto studentDto)
        {
            Student = studentDto;
        }

        public GetStudentQueryResponse(string message)
        {
            Message = message;
        }

    }
}
