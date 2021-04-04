using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using N00695523cumulativePartOne.Models;

namespace N00695523cumulativePartOne.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        // GET: Student/List/{SearchKey}
        public ActionResult List(string SearchKey = null)
        {
            // Call to StudentDataController method ListStudent(SearchKey), and pass information to the View(List).
            StudentDataController controller = new StudentDataController();
            IEnumerable<Student> Students = controller.ListStudents(SearchKey);
            return View(Students);
        }

        // GET: Student/Show{id}
        public ActionResult Show(int id)
        {
            // Call to StudentDataController method FindStudent(id), and pass information to the View(Show).
            StudentDataController controller = new StudentDataController();
            Student NewStudent = controller.FindStudent(id);

            return View(NewStudent);
        }

    }
}