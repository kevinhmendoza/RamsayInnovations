using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using RamsayInnovations.Domain;
using RamsayInnovations.Domain.SeedWorks;
using RamsayInnovations.WebApi.Extensions;
using RamsayInnovations.WebApi.Mappers.StudentMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RamsayInnovations.WebApi.Features.Students.Commands
{


    public class UpdateStudentCommand : IRequest<UpdateStudentCommandResponse>
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long Age { get; set; }
        public string Career { get; set; }
    }

    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, UpdateStudentCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly UpdateStudentCommandValidator _validator;
        public UpdateStudentCommandHandler(IValidator<UpdateStudentCommand> validator, IMapper mapper)
        {
            _mapper = mapper;
            _validator = validator as UpdateStudentCommandValidator;
        }
        public async Task<UpdateStudentCommandResponse> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid) return new UpdateStudentCommandResponse(validationResult);
            _mapper.Map(request, _validator.Student);
            await _validator.UnitOfWork.Commit();
            var studentDto = _mapper.Map<StudentDto>(_validator.Student);
            return new UpdateStudentCommandResponse(validationResult, studentDto);

        }
    }

    public class UpdateStudentCommandResponse
    {
        public StudentDto Student { get; set; }
        private ValidationResult ValidationResult { get; }
        public string Mensaje { get; set; }
        public bool IsValid => ValidationResult.IsValid;

        public UpdateStudentCommandResponse(ValidationResult validationResult, StudentDto studentDto = null)
        {
            ValidationResult = validationResult;
            Student = studentDto;
            if (!ValidationResult.IsValid)
            {
                Mensaje = ValidationResult.ToText();
            }
            else
            {
                Mensaje = $"Operación realizada satisfactoriamente.";
            }
        }



    }

    public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public Student Student { get; set; }
        public UpdateStudentCommandValidator(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            When(t => t.Id > 0, () =>
            {
                RuleFor(r => r).Must(DebeExistirStudent).WithMessage(t => $"No existe el estudiante [{t.Id}] a modificar");
            });
        }

        private bool DebeExistirStudent(UpdateStudentCommand request)
        {
            Student = UnitOfWork.StudentRepository.FindAsync(request.Id).Result;
            return Student != null;
        }



    }
}
