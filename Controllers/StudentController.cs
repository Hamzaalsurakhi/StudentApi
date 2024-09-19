using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApi.DataSimulation;
using StudentApi.Models;

namespace StudentApi.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/StudentApi")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            return Ok(StudentSimulationData.StudentList);
        }
    }
}
