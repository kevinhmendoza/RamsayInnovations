using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RamsayInnovations.Domain;
using RamsayInnovations.Domain.SeedWorks;
using RamsayInnovations.Infrastructure;
using RamsayInnovations.WebApi.Features.Students.Commands;
using RamsayInnovations.WebApi.Features.Students.Queries;
using RamsayInnovations.WebApi.Mappers.StudentMap;

namespace RamsayInnovations.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            var response = await _mediator.Send(new GetStudentsQuery());
            return Ok(response.Students);
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
            return Ok(response.Student);
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(long id, UpdateStudentCommand request)
        {
            if (id != request.Id)
            {
                return BadRequest("El id del estudiante no coincide con el id de la URL");
            }

            var response = await _mediator.Send(request);
            if (response.IsValid)
            {
                return Ok(response);
            }
            else return BadRequest(response.Mensaje);


        }

        // POST: api/Students
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(CreateStudentCommand request)
        {
            var response = await _mediator.Send(request);
            if (response.IsValid)
            {
                return Ok(response);
            }
            else return BadRequest(response.Mensaje);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(long id)
        {
            var response = await _mediator.Send(new DeleteStudentCommand { Id = id });
            if (response.IsValid)
            {
                return Ok(response);
            }
            else return BadRequest(response.Mensaje);
        }



    }






}
