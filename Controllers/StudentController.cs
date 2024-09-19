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
        [HttpGet("All",Name ="GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            if(StudentSimulationData.StudentList.Count ==0)
            {
                return NotFound("Not Students Found!");
            }
            return Ok(StudentSimulationData.StudentList);
        }


        [HttpGet("Passed", Name = "GetAllStudentsPassed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //method to get all students who passed
        public ActionResult<IEnumerable<Student>> GetPassedStudents()
        {


            var passedStudents =StudentSimulationData.StudentList.Where(student => student.Grade>=50).ToList();
            if (passedStudents.Count ==0)
            {
                return NotFound("Not Students Passed!");
            }
            return Ok(passedStudents);//return the list of student who passed
        }

        [HttpGet("AverageGrade",Name = "GetAverageGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<double> GetAverageGrade()
        {
            //StudentSimulationData.StudentList.Clear();
            if (StudentSimulationData.StudentList.Count==0)
            {
                return NotFound("Not Students found");
            }

            var averageGrade= StudentSimulationData.StudentList.Average(student => student.Grade);
            return Ok(averageGrade);
        }


        [HttpGet("{id}",Name ="GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


        public ActionResult<Student> GetStudentById(int id)
        {
            if (id <1)
            {
                return BadRequest($"Not Accepted ID {id}");
            }
            var student=StudentSimulationData.StudentList.FirstOrDefault(student => student.Id==id);

            if (student==null)
            {
                return NotFound($"Student with ID {id} not found");
            }

            return Ok(student);

        }


        [HttpPost(Name ="AddStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<Student> AddStudent(Student newStudent)
        {
            if (newStudent == null || string.IsNullOrEmpty(newStudent.Name) || newStudent.Age <0 || newStudent.Grade<0)
            {
                return BadRequest("Invalid student data.");
            }
            newStudent.Id = StudentSimulationData.StudentList.Count > 0 ? StudentSimulationData.StudentList.Max(student => student.Id) +1 : 1;
            StudentSimulationData.StudentList.Add(newStudent);

            return CreatedAtRoute("GetStudentById",new {id=newStudent.Id},newStudent);

        }


        [HttpDelete("{id}", Name = "DeleteGetStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult DeleteStudent(int id)
        {
            if (id <1)
            {
                return BadRequest($"Not Accepted ID {id}");
            }
            var student = StudentSimulationData.StudentList.FirstOrDefault(student => student.Id==id);

            if (student==null)
            {
                return NotFound($"Student with ID {id} not found");
            }

            return Ok($"Student with ID {id} has been delete");
        }


        [HttpPut("{id}", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<Student> UpdateStudent(int id, Student updatedStudent)
        {
            if (id < 1 || updatedStudent == null || string.IsNullOrEmpty(updatedStudent.Name) || updatedStudent.Age < 0 || updatedStudent.Grade < 0)
            {
                return BadRequest("Invalid student data.");
            }
            var student = StudentSimulationData.StudentList.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            student.Name = updatedStudent.Name;
            student.Age = updatedStudent.Age;
            student.Grade = updatedStudent.Grade;

            return Ok(student);
        }

    }
}
