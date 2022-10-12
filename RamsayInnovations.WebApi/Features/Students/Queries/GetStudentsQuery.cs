using AutoMapper;
using MediatR;
using RamsayInnovations.Domain.SeedWorks;
using RamsayInnovations.WebApi.Mappers.StudentMap;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RamsayInnovations.WebApi.Features.Students.Queries
{
    public class GetStudentsQuery : IRequest<GetStudentsQueryResponse>
    {
    }

    public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, GetStudentsQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetStudentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetStudentsQueryResponse> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _unitOfWork.StudentRepository.GetAllAsync();
            var studentsDto = _mapper.Map<List<StudentDto>>(students);
            return new GetStudentsQueryResponse(studentsDto);

        }
    }

    public class GetStudentsQueryResponse
    {
        public List<StudentDto> Students { get; set; }
        public GetStudentsQueryResponse(List<StudentDto> studentsDto = null)
        {
            Students = studentsDto;
        }

    }
}
