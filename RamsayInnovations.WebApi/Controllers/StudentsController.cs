using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RamsayInnovations.Domain;
using RamsayInnovations.Domain.SeedWorks;
using RamsayInnovations.Infrastructure;
using RamsayInnovations.WebApi.Features.Students.Queries;
using RamsayInnovations.WebApi.Mappers.StudentMap;

namespace RamsayInnovations.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public StudentsController(IMapper mapper, IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            var response = await _mediator.Send(new GetStudentsQuery());
            return response.Students;
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(long id)
        {
            var response = await _mediator.Send(new GetStudentQuery { StudentId = id });
            if (response.Student == null)
            {
                return NotFound(response.Message);
            }
            return response.Student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(long id, Student student)
        {
            if (!StudentExists(id))
            {
                return NotFound($"El id {id} del estudiante no existe en la BD");
            }

            if (id != student.Id)
            {
                return BadRequest("El id del estudiante no coincide con el id de la URL");
            }

            _unitOfWork.StudentRepository.Edit(student);

            try
            {
                await _unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                string message = DynamicException.Formatted(ex);
                return BadRequest(message);
            }

            return Ok("El estudiante se modifico exitosamente");
        }

        // POST: api/Students
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(StudentDto studentDto)
        {

            var student = _mapper.Map<Student>(studentDto);
            try
            {

                _unitOfWork.StudentRepository.Add(student);
                await _unitOfWork.Commit();
            }
            catch (DbUpdateException ex)
            {
                string message = DynamicException.Formatted(ex);
                return BadRequest(message);
            }

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(long id)
        {
            var student = await _unitOfWork.StudentRepository.FindAsync(id);
            if (student == null)
            {
                return NotFound($"El id {id} del estudiante no existe en la BD");
            }

            try
            {
                _unitOfWork.StudentRepository.Delete(student);
                await _unitOfWork.Commit();
            }
            catch (DbUpdateException ex)
            {
                string message = DynamicException.Formatted(ex);
                return BadRequest(message);
            }


            return Ok($"El estudiante fue elminado exitosamente");
        }

        private bool StudentExists(long id)
        {
            return _unitOfWork.StudentRepository.Any(e => e.Id == id);
        }

    }






}
