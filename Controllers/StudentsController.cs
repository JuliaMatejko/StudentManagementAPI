using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Models;
using StudentManagementAPI.Services;

namespace StudentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentsController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        // GET: api/<StudentsController>
        [HttpGet]
        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await studentService.GetAllAsync();
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(string id)
        {
            var student = await studentService.GetAsync(id);

            if (student == null)
            {
                return NotFound($"Student with Id = {id} was not found");
            }

            return student;
        }

        // POST api/<StudentsController>
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent([FromBody] Student student)
        {
            await studentService.CreateAsync(student);

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(string id, [FromBody] Student student)
        {
            var existingStudent = await studentService.GetAsync(id);

            if (existingStudent == null)
            {
                return NotFound($"Student with Id = {id} not found");
            }

            await studentService.UpdateAsync(id, student);

            return NoContent();
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(string id)
        {
            var student = await studentService.GetAsync(id);

            if (student == null)
            {
                return NotFound($"Student with Id = {id} not found");
            }

            await studentService.DeleteAsync(student.Id);

            return Ok($"Student with Id = {id} deleted");
        }
    }
}
