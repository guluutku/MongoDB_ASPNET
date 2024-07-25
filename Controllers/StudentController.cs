using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{

    private readonly IStudentService studentService;

    public StudentController(IStudentService studentService)
    {
        this.studentService = studentService;
    }

    // Get: api/<StudentController
    [HttpGet]
    public ActionResult<List<Student>> Get()
    {
        return studentService.Get();
    }

    // Get: api/<StudentController/5
    [HttpGet("{id}")]
    public ActionResult<Student> Get(string id)
    {
        var student = studentService.Get(id);
        if (student == null)
        {
            return NotFound($"Student with id = {id} not Found");
        }
        return student;
    }

    // POST
    [HttpPost]
    public ActionResult<Student> Post([FromBody] Student student)
    {
        studentService.create(student);

        return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public ActionResult Put(string id, [FromBody] Student student)
    {
        var existingStudent = studentService.Get(id);

        if (existingStudent == null)
        {
            return NotFound($"Student with id = {id} not Found");
        }

        studentService.Update(id, existingStudent);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(string id)
    {
        var existingStudent = studentService.Get(id);

        if (existingStudent == null)
        {
            return NotFound($"Student with id = {id} not Found");
        }

        studentService.Delete(id);

        return Ok($"Student with ID = {existingStudent.Id} deleted");
    }

}