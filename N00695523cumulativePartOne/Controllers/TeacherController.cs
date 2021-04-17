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

        /// <summary>
        /// Route to the view page based on the teacher id that is selected.
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>The information on the teacher base on the id.</returns>
        /// <example>GET: /Teacher/Update/1</example>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        public ActionResult Ajax_Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        /// <summary>
        /// A post request containing the information of the teacher.
        /// </summary>
        /// <param name="id">Teacher Id</param>
        /// <param name="TeacherFname">Teacher first name</param>
        /// <param name="TeacherLname">Teacher last name</param>
        /// <param name="TeacherEmployeeNumber">Teacher employee number</param>
        /// <param name="TeacherHireDate">Teacher hire date</param>
        /// <param name="TeacherSalary">Teacher salary</param>
        /// <returns>To a webpage that contains the information on the teacher.</returns>
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string TeacherEmployeeNumber, DateTime TeacherHireDate, decimal TeacherSalary)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFirstName = TeacherFname;
            TeacherInfo.TeacherLastName = TeacherLname;
            TeacherInfo.EmployeeNumber = TeacherEmployeeNumber;
            TeacherInfo.HireDate = TeacherHireDate;
            TeacherInfo.Salary = TeacherSalary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/" + id);

        }

    }

}