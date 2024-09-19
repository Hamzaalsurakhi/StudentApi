using StudentApi.Models;

namespace StudentApi.DataSimulation
{
    public class StudentSimulationData
    {
        //static list of student
        public static readonly List<Student> StudentList = new List<Student>
        {
            //initiakiza the list with  ssmr student object
            new Student {Id=1,Name="Hamza",Age=25,Grade=88},
             new Student {Id=2,Name="ZAID",Age=23,Grade=70},
              new Student {Id=3,Name="LAITH",Age=21,Grade=48},
               new Student {Id=4,Name="Abood",Age=20,Grade=77}



        };
    }
}
