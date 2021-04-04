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
                NewStudent.EnrolDate = Convert.ToDateTime(ResultSet["enroldate"]);

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

    }
}
