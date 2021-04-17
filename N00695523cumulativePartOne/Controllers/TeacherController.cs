using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using N00695523cumulativePartOne.Models;
using System.Diagnostics;

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
        public ActionResult List(string SearchKey = null)
        {
            // Call to TeacherDataController method ListTeachers, and pass information to the View(List).
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
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


        // GET: /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }


        //POST: /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET: /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //POST: /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string TeacherEmployeeNumber, DateTime TeacherHireDate, decimal TeacherSalary)
        {
            /*// Identify the inputs and methods are running.

            Debug.WriteLine("I have access the create method!");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(TeacherEmployeeNumber);
            Debug.WriteLine(TeacherHireDate);
            Debug.WriteLine(TeacherSalary);*/

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFirstName = TeacherFname;
            NewTeacher.TeacherLastName = TeacherLname;
            NewTeacher.EmployeeNumber = TeacherEmployeeNumber;
            NewTeacher.HireDate = TeacherHireDate;
            NewTeacher.Salary = TeacherSalary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }
        
    }
}