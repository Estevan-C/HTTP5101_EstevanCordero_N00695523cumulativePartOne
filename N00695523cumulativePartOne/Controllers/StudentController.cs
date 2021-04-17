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
        public ActionResult Create(string StudentFname, string StudentLname, string StudentNumber, String EnrolDate)
        {

            /*// Identify the inputs and methods are running.

            Debug.WriteLine("I have access the create method!");
            Debug.WriteLine(StudentFname);
            Debug.WriteLine(StudentLname);
            Debug.WriteLine(StudentNumber);
            // Debug.WriteLine(EnrolDate.Date);
            // DateTime EnrolDate
            // When being passed in the parameters it expects only a Date but DateTime is the only data type*/
            // How do I grab only the date from DateTime from paremeter input?


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

        /// <summary>
        /// Route to the view page based on the student id that is selected.
        /// </summary>
        /// <param name="id">Id of the Student</param>
        /// <returns>The information on the student base on the id.</returns>
        /// <example>GET: /Student/Update/1</example>
        public ActionResult Update(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student SelectedStudent = controller.FindStudent(id);

            return View(SelectedStudent);
        }


        /// <summary>
        /// A post request containing the information of the student.
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <param name="StudentFname">Student First Name</param>
        /// <param name="StudentLname">Student Last Name</param>
        /// <param name="StudentNumber">Student Number</param>
        /// <param name="EnrolDate">Student Enrolment Date</param>
        /// <returns>To a web page that contains the information on the student</returns>
        [HttpPost]
        public ActionResult Update(int id, string StudentFname, string StudentLname, string StudentNumber, DateTime EnrolDate)
        {
            Student StudentInfo = new Student();
            StudentInfo.StudentFName = StudentFname;
            StudentInfo.StudentLName = StudentLname;
            StudentInfo.StudentNumber = StudentNumber;
            StudentInfo.EnrolDate = EnrolDate.Date; // Only Grab the date from DateTime

            StudentDataController controller = new StudentDataController();
            controller.UpdateStudent(id, StudentInfo);

            return RedirectToAction("Show/" + id);
        }
    }
}