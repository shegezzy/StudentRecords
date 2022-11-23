using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentRecords.Data;
using StudentRecords.DTO;
using StudentRecords.Service;

namespace StudentRecords.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentDbContext _context;
        private readonly ImessageProducer _messagePublisher;

        public StudentController(IStudentDbContext context, ImessageProducer messagePublisher)
        {
            _context = context;
            _messagePublisher = messagePublisher;
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewStudent(StudentDTO studentDto)
        {
            Student student = new()
            {
                StudentName = studentDto.StudentName,
                Age = studentDto.Age,
                CourseTitle = studentDto.CourseTitle
            };

            _context.Student.Add(student);

            await _context.SaveChangesAsync();

            _messagePublisher.SendMessage(student);

            return Ok(new { id = student.Id });
        }
    }
}
