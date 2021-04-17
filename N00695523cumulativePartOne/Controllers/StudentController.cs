using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using N00695523cumulativePartOne.Models;
using System.Diagnostics;

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

        // GET: /Student/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student NewStudent = controller.FindStudent(id);

            return View(NewStudent);
        }

        //POST: /Student/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            StudentDataController controller = new StudentDataController();
            controller.DeleteStudent(id);
            return RedirectToAction("List");
        }

        //GET: /Student/New
        public ActionResult New()
        {
            return View();
        }

        //POST: /Student/Create
        [HttpPost]
        public ActionResult Create(string StudentFname, string StudentLname, string StudentNumber, string EnrolDate)
        {

            /*// Identify the inputs and methods are running.

            Debug.WriteLine("I have access the create method!");
            Debug.WriteLine(StudentFname);
            Debug.WriteLine(StudentLname);
            Debug.WriteLine(StudentNumber);
            // Debug.WriteLine(EnrolDate.Date);
            // DateTime EnrolDate
            // When being passed in the parameters it expects only a Date but DateTime is the only data type*/

            Student NewStudent = new Student();
            NewStudent.StudentFName = StudentFname;
            NewStudent.StudentLName = StudentLname;
            NewStudent.StudentNumber = StudentNumber;
            NewStudent.EnrolDate = Convert.ToDateTime(EnrolDate);
            // This is to allow a null value to pass through.

            StudentDataController controller = new StudentDataController();
            controller.AddStudent(NewStudent);


            return RedirectToAction("List");
        }
    }
}