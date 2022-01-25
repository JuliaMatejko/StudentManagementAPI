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

        // GET: api/Students
        [HttpGet]
        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await studentService.GetAllStudentsAsync();
        }

        // GET api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentAsync(string id)
        {
            var student = await studentService.GetStudentAsync(id);

            if (student == null)
            {
                return NotFound($"Student with Id = {id} was not found");
            }

            return student;
        }

        // POST api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudentAsync([FromBody] Student student)
        {
            await studentService.CreateStudentAsync(student);

            return CreatedAtAction(nameof(GetStudentAsync), new { id = student.Id }, student);
        }

        // PUT api/Students/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudentAsync(string id, [FromBody] Student student)
        {
            var existingStudent = await studentService.GetStudentAsync(id);

            if (existingStudent == null)
            {
                return NotFound($"Student with Id = {id} not found");
            }

            await studentService.UpdateStudentAsync(id, student);

            return NoContent();
        }

        // DELETE api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudentAsync(string id)
        {
            var student = await studentService.GetStudentAsync(id);

            if (student == null)
            {
                return NotFound($"Student with Id = {id} not found");
            }

            await studentService.DeleteStudentAsync(student.Id);

            return Ok($"Student with Id = {id} deleted");
        }
    }
}
