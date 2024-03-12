using CollegeApp.Data;
using CollegeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles ="Superadmin")]
    public class StudentController : ControllerBase
    {

        private readonly CollegeDbContext _dbContext;
        private readonly ILogger<StudentController> _logger;

        public StudentController (CollegeDbContext dbContext, ILogger<StudentController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet("all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            var Students = new List<StudentDTO>();
            foreach (var item in _dbContext.Students)
            {
                StudentDTO obj = new StudentDTO()
                {
                    Id = item.Id,
                    StudentName = item.StudentName,
                    Email = item.Email,
                    Adress = item.Adress
                };
                Students.Add(obj);
            }
            return Ok(Students);
        }

        [HttpGet("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [AllowAnonymous]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }
            var Student = _dbContext.Students.Where(n => n.Id == id).FirstOrDefault();
            if (Student == null)
            {
                return NotFound();
            }
            var StudentDTO = new StudentDTO
            {
                Id = Student.Id,
                StudentName = Student.StudentName,
                Email = Student.Email,
                Adress = Student.Adress
            };
            return Ok(StudentDTO);
        }

        [HttpGet("{name:alpha}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<StudentDTO> GetStudentByName(string name)
        {
            if (name == null)
            {
                return BadRequest();
            }
            var Student = _dbContext.Students.Where(n => n.StudentName == name).FirstOrDefault();
            if (Student == null)
            {
                return NotFound();
            }
            var StudentDTO = new StudentDTO
            {
                Id = Student.Id,
                StudentName = Student.StudentName,
                Email = Student.Email,
                Adress = Student.Adress
            };
            return Ok(StudentDTO);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<bool> DeleteStudent(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }
            var Student = _dbContext.Students.Where((n) => n.Id == id).FirstOrDefault();
            if (Student == null)
            {
                return NotFound();
            }
            _dbContext.Students.Remove(Student);
            _dbContext.SaveChanges();
            return Ok(true);
        }

        [HttpPost("create")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
        {
            if (model == null )
            {
                return BadRequest();
            }
            Student student = new Student
            {
                StudentName = model.StudentName,
                Email = model.Email,
                Adress = model.Adress
            };
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();
            model.Id = student.Id;
            return CreatedAtRoute("GetStudentById", new { id = model.Id }, model);
            //return Ok(model);
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult UpdateStudent([FromBody] StudentDTO model)
        {
            if(model == null || model.Id <=0)
            {
                return BadRequest();
            }
            var existingStudent = _dbContext.Students.Where(n =>  n.Id == model.Id).FirstOrDefault();
            if(existingStudent == null)
            {
                return NotFound();
            }
            existingStudent.StudentName = model.StudentName;
            existingStudent.Email = model.Email;
            existingStudent.Adress = model.Adress;
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPatch]
        [Route("{id}/updatepartial")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
            {
                return BadRequest();
            }
            var existingStudent = _dbContext.Students.Where(n => n.Id == id).FirstOrDefault();
            if (existingStudent == null)
            {
                return NotFound();
            }

            var StudentDTO = new StudentDTO()
            {
                Id = existingStudent.Id,
                StudentName = existingStudent.StudentName,
                Email = existingStudent.Email,
                Adress = existingStudent.Adress
            };

            patchDocument.ApplyTo(StudentDTO, ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            existingStudent.StudentName = StudentDTO.StudentName;
            existingStudent.Email = StudentDTO.Email;
            existingStudent.Adress = StudentDTO.Adress;
            _dbContext.SaveChanges();

            return NoContent();
        }

    }
}
