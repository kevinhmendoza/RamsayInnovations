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
        private readonly IMapper _mapper;
        private readonly CreateStudentCommandValidator _validator;
        public CreateStudentCommandHandler(IValidator<CreateStudentCommand> validator, IMapper mapper)
        {
            _mapper = mapper;
            _validator = validator as CreateStudentCommandValidator;
        }
        public async Task<CreateStudentCommandResponse> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid) return new CreateStudentCommandResponse(validationResult);

            var student = _validator.UnitOfWork.StudentRepository.Add(new Student
            {
                Username = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                Career = request.Career
            });

            await _validator.UnitOfWork.Commit();
            var studentDto = _mapper.Map<StudentDto>(student);

            return new CreateStudentCommandResponse(validationResult, studentDto);

        }
    }

    public class CreateStudentCommandResponse
    {
        public StudentDto Student { get; set; }
        private ValidationResult ValidationResult { get; }
        public string Mensaje { get; set; }
        public bool IsValid => ValidationResult.IsValid;

        public CreateStudentCommandResponse(ValidationResult validationResult, StudentDto studentDto = null)
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

    public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public CreateStudentCommandValidator(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            RuleFor(t => t.Username).NotNull().NotEmpty().WithMessage("Debe enviar el UserName del estudiante a registrar!");


            When(t => !string.IsNullOrEmpty(t.Username), () =>
            {
                RuleFor(r => r).Must(ExisteUserName).WithMessage(t => $"Ya existe el usuario [{t.Username}] del estudiante a registrar ");
            });
        }

        private bool ExisteUserName(CreateStudentCommand request)
        {
            var existeUserName = UnitOfWork.StudentRepository.Any(t => t.Username == request.Username);
            return !existeUserName;
        }



    }
}
