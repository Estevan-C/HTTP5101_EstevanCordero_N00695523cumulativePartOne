using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using N00695523cumulativePartOne.Models;

namespace N00695523cumulativePartOne.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET: Teacher/List
        public ActionResult List()
        {
            // Call to TeacherDataController method ListTeachers, and pass information to the View(List).
            TeacherDataController controller = new TeacherDataController();
            List<Teacher> Teachers = controller.ListTeachers();
            return View(Teachers);
        }

        // GET: Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            // Call to TeacherDataController method FindTeachers, and pass information to the View(Show).
            TeacherDataController controller = new TeacherDataController();
            Teacher TeachersInfo = controller.FindTeacher(id);

            return View(TeachersInfo);
        }

        
    }
}