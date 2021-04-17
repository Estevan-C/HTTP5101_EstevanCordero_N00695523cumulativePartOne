using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using N00695523cumulativePartOne.Models;
using MySql.Data.MySqlClient;

namespace N00695523cumulativePartOne.Controllers
{
    public class StudentDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();


        /// <summary>
        /// Returns a list of students from the database table.
        /// Included an id that will be used for the Search Bar.
        /// </summary>
        /// /// <param name="SearchKey">What is used to search into the table of students</param>
        /// <returns>
        /// Students Id, First/Last Name, Number, Enrol Date.
        /// </returns>

        [HttpGet]
        [Route("api/StudentData/ListStudents/{SearchKey?}")]
        public IEnumerable<Student> ListStudents(string SearchKey=null)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            // SQL Query to search through table base on name using @key(SearchKey)
            cmd.CommandText = "Select * from Students where lower(studentfname) like lower(@key) or lower(studentlname) like lower(@key) or lower(concat(studentfname, ' ', studentlname)) like lower(@key)";

            // Changing the Searchkey var into @key to avoid any database errors.
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();


            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Student> Students = new List<Student> { };

            while (ResultSet.Read())
            {
                Student NewStudent = new Student();

                NewStudent.StudentId = Convert.ToInt32(ResultSet["studentid"]);
                NewStudent.StudentFName = ResultSet["studentfname"].ToString();
                NewStudent.StudentLName = ResultSet["studentlname"].ToString();
                NewStudent.StudentNumber = ResultSet["studentnumber"].ToString();
                //NewStudent.EnrolDate = Convert.ToDateTime(ResultSet["enroldate"]);

                Students.Add(NewStudent);
            }
            Conn.Close();

            return Students;
        }

        /// <summary>
        /// Finds a student based on id.
        /// </summary>
        /// <param name="id">The Id is the studentid from the table</param>
        /// <returns>
        /// Student Information base on Id.
        /// </returns>

        [HttpGet]
        public Student FindStudent(int studentId)
        {
            Student NewStudent = new Student();

            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            // Changing the studentId into @id to avoid any database errors.
            cmd.CommandText = "Select * from Students where studentid = @id";
            cmd.Parameters.AddWithValue("@id", studentId);
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {

                NewStudent.StudentId = Convert.ToInt32(ResultSet["studentid"]);
                NewStudent.StudentFName = ResultSet["studentfname"].ToString();
                NewStudent.StudentLName = ResultSet["studentlname"].ToString();
                NewStudent.StudentNumber = ResultSet["studentnumber"].ToString();
                NewStudent.EnrolDate = Convert.ToDateTime(ResultSet["enroldate"]);

            }

            return NewStudent;
        }

        /// <summary>
        /// Deletes a Student based on the ID that was entered.
        /// </summary>
        /// <param name="id">The id of the student.</param>
        /// <example>POST api/StudentData/DeleteStudent/1</example>
        [HttpPost]
        public void DeleteStudent(int id)
        {
            // Create a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection
            Conn.Open();

            // Create a command (query) to execute
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL QUERY
            cmd.CommandText = "Delete from students where studentid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            // Close connection
            Conn.Close();
        }

        /// <summary>
        /// Inserts user input as Values for the Student Table.
        /// </summary>
        /// <param name="NewStudent">Values being passed</param>
        /// <example>
        /// Estevan
        /// Cordero
        /// 123456
        /// 2021-04-02 12:00AM
        /// </example>
        [HttpPost]
        public void AddStudent(Student NewStudent)
        {
            // Create a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection
            Conn.Open();

            // Create a command (query) to execute
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL QUERY
            cmd.CommandText = "insert into students (studentfname, studentlname, studentnumber, enroldate) values (@StudentFname, @StudentLname, @StudentNumber, @EnrolDate)";
            cmd.Parameters.AddWithValue("@StudentFname", NewStudent.StudentFName);
            cmd.Parameters.AddWithValue("@StudentLname", NewStudent.StudentLName);
            cmd.Parameters.AddWithValue("@StudentNumber", NewStudent.StudentNumber);
            cmd.Parameters.AddWithValue("@EnrolDate", NewStudent.EnrolDate);
            //Even if no value is entered since it can accept null it will display the first date it can be display.
            // for the mysql convert the date time into something that it can understand 
            // string mydateconvert = datetime.ToString("yyyy-mm-dd")
            // my sql doesn't want to figure so you must tell what the date is and is expecting a format so the c# will have to
            // do the proccess and understanding google how convert a datetime into a string
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            // Close connection
            Conn.Close();
        }

        /// <summary>
        /// Updates an existing information for a Student
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <param name="StudentInfo">Student info that will be passed</param>
        /// <example>
        /// Fred
        /// Robert
        /// N123456
        /// April 30 2021
        /// 1(Id does not need to be updated only the content of the Teacher does)
        /// </example>
        [HttpPost]
        public void UpdateStudent(int id, Student StudentInfo)
        {
            //Create an instance of a connnection
            MySqlConnection Conn = School.AccessDatabase();

            //Open connection
            Conn.Open();

            //Establish a command (query) for the database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "update students set studentfname=@StudentFname, studentlname=@StudentLname, studentnumber=@StudentNumber, enroldate=@EnrolDate where studentid=@StudentId";
            cmd.Parameters.AddWithValue("@StudentFname", StudentInfo.StudentFName);
            cmd.Parameters.AddWithValue("@StudentLname", StudentInfo.StudentLName);
            cmd.Parameters.AddWithValue("@StudentNumber", StudentInfo.StudentNumber);
            cmd.Parameters.AddWithValue("@EnrolDate", StudentInfo.EnrolDate); // Come back with teachers advics on how to fix date problem
            cmd.Parameters.AddWithValue("@StudentId", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

    }
}
