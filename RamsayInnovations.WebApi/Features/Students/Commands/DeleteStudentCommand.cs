using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using RamsayInnovations.Domain;
using RamsayInnovations.Domain.SeedWorks;
using RamsayInnovations.WebApi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RamsayInnovations.WebApi.Features.Students.Commands
{


    public class DeleteStudentCommand : IRequest<DeleteStudentCommandResponse>
    {
        public long Id { get; set; }
    }

    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, DeleteStudentCommandResponse>
    {
        private readonly DeleteStudentCommandValidator _validator;
        public DeleteStudentCommandHandler(IValidator<DeleteStudentCommand> validator)
        {
            _validator = validator as DeleteStudentCommandValidator;
        }
        public async Task<DeleteStudentCommandResponse> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid) return new DeleteStudentCommandResponse(validationResult);

            _validator.UnitOfWork.StudentRepository.Delete(_validator.Student);

            await _validator.UnitOfWork.Commit();

            return new DeleteStudentCommandResponse(validationResult);

        }
    }

    public class DeleteStudentCommandResponse
    {
        private ValidationResult ValidationResult { get; }
        public string Mensaje { get; set; }
        public bool IsValid => ValidationResult.IsValid;
        public DeleteStudentCommandResponse(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
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

    public class DeleteStudentCommandValidator : AbstractValidator<DeleteStudentCommand>
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public Student Student { get; set; }
        public DeleteStudentCommandValidator(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            When(t => t.Id > 0, () =>
            {
                RuleFor(r => r).Must(DebeExistirStudent).WithMessage(t => $"No existe el estudiante [{t.Id}] a eliminar");
            });
        }

        private bool DebeExistirStudent(DeleteStudentCommand request)
        {
            Student = UnitOfWork.StudentRepository.FindAsync(request.Id).Result;
            return Student != null;
        }



    }
}
