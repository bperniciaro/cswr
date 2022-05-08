using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIWithWebForm.Models;

//namespace WebAPIWithWebForm
//{
  public class CollegeController : ApiController
  {
    Student[] stud = new Student[]
    {
            new Student{StudRollNO=1, StudName="Amit",
            StudAddress="Noida", StudMONO=123, StudCourse="MCA"},
            new Student{StudRollNO=2, StudName="Rahgvendra",
            StudAddress="Hardoi", StudMONO=321, StudCourse="BCA"},
            new Student{StudRollNO=3, StudName="Ankur",
            StudAddress="Lucknow", StudMONO=567, StudCourse="MBA"}
    };
    public IEnumerable<Student> GetallStudents()
    {
      return stud;
    }
    public IHttpActionResult GetStudent(int id)
    {
      var record = stud.FirstOrDefault(x => x.StudRollNO == id);
      if (record == null)
      {
        return NotFound();
      }
      return Ok(record);
    }
  }
//}
