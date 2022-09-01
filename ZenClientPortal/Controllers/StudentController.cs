using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZenClientPortal.Models;

namespace ZenClientPortal.Controllers
{
    [RoutePrefix("api/student")]
    public class StudentController : ApiController
    {
        static List<Student> students = new List<Student>()
        {
            new Student() { Id = 1, Name = "Tom" },
            new Student() { Id = 2, Name = "Sam" },
            new Student() { Id = 3, Name = "John" }
        };

        [Route("")]
        public IEnumerable<Student> Get()
        {
            return students;
        }
        [HttpPost]
        public HttpResponseMessage Post(Student student)
        {
            students.Add(student);
            var response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Url.Link("GetStudentById", new { id = student.Id }));
            return response;
        }

        [Route("{id:int}",Name="GetStudentById")]
        public Student Get(int id)
        {
            return students.FirstOrDefault(s => s.Id == id);
        }

        [Route("{name:alpha:length(1,7)}")]
        public Student Get(string name)
        {
            return students.FirstOrDefault(s => s.Name.ToLower() == name.ToLower());
        }

        [Route("{id}/courses")]  
        public IEnumerable<string> GetStudentCourses(int id)
        {
            if (id == 1)
                return new List<string>() { "C#", "ASP.NET", "SQL Server" };
            else if (id == 2)
                return new List<string>() { "ASP.NET Web API", "C#", "SQL Server" };
            else
                return new List<string>() { "Bootstrap", "jQuery", "AngularJs" };
        }

        

        [Route("~/api/teachers")]
        public IEnumerable<Teachers> GetTeachers()
        {
            List<Teachers> teachers = new List<Teachers>()
    {
        new Teachers() { Id = 1, Name = "Rob" },
                new Teachers() { Id = 2, Name = "Mike" },
        new Teachers() { Id = 3, Name = "Mary" }
    };

            return teachers;
        }
    }
}
