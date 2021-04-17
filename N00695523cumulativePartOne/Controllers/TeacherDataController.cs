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
    public class TeacherDataController : ApiController
    {

        private SchoolDbContext School = new SchoolDbContext();
        
        /// <summary>
        /// Returns a list of teachers in the database table.
        /// </summary>
        /// <example>Get api/TeacherData/ListTeacher</example>
        /// <returns>
        /// A list of teachers (Id, first, and last name).
        /// </returns>

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            
            MySqlConnection Conn = School.AccessDatabase(); //Connects to Database

            Conn.Open(); // Opens connection to the database

            MySqlCommand cmd = Conn.CreateCommand(); // Makes a var name cmd to allow use to make the queries.

            //cmd.CommandText = "Select * from Teachers"; // Place the following query into the cmd.CommandText
            cmd.CommandText = "Select * from Teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";
            
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader(); // Makes a var ResultSet to read and execute the command above.

            List<Teacher> Teachers = new List<Teacher> { }; // Empty list used to store the Teacher Objects.

            // While loop to read over and grab all of information store in the Teacher table.
            while (ResultSet.Read())
            {
                Teacher NewTeacher = new Teacher();

                NewTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                NewTeacher.TeacherFirstName = ResultSet["teacherfname"].ToString();
                NewTeacher.TeacherLastName = ResultSet["teacherlname"].ToString();
                Teachers.Add(NewTeacher);
            }

            Conn.Close(); // Close the DB connection.

            return Teachers; // Returns List
            
        }

        /// <summary>
        /// Grabs Teachers ID,  Full Name, Employee Number, Hire Date, and Salary from the database
        /// </summary>
        /// <param name="teacherid">The Id is the teacherid from the table</param>
        /// <returns>
        /// Id = 1
        /// Full Name = Alexander Bennett
        /// Employee Number = T378
        /// Hire Date = 2016-08-05 00:00:00
        /// Salary = 55.30
        /// </returns>

        [HttpGet]
        [Route("api/TeacherData/ShowTeacher/{teacherid}")] 
        public Teacher FindTeacher(int teacherId)
        {
            Teacher SelectedTeacher = new Teacher();

            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from Teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", teacherId);
            cmd.Prepare();
            //SQL Query to grab all info from Teacher table base on ID.

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                SelectedTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                SelectedTeacher.TeacherFirstName = ResultSet["teacherfname"].ToString();
                SelectedTeacher.TeacherLastName = ResultSet["teacherlname"].ToString();
                SelectedTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                SelectedTeacher.HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                SelectedTeacher.Salary = Convert.ToDecimal(ResultSet["salary"]);
            }

            return SelectedTeacher;
        }


        /// <summary>
        /// Deletes a Teacher based on the ID that was entered.
        /// </summary>
        /// <param name="id">The id of the teacher.</param>
        /// <example>POST api/TeacherData/DeleteTeacher/1</example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            // Create a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection
            Conn.Open();

            // Create a command (query) to execute
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL QUERY
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            // Close connection
            Conn.Close();

        }

        /// <summary>
        /// Inserts user input as Values for the Teacher Table.
        /// </summary>
        /// <param name="NewTeacher">Values being passed</param>
        /// <example>
        /// Estevan
        /// Cordero
        /// 123456
        /// 2021-04-02 12:00AM
        /// 40.25
        /// </example>
        [HttpPost]
        public void AddTeacher(Teacher NewTeacher)
        {
            // Create a connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection
            Conn.Open();

            // Create a command (query) to execute
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL QUERY
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherFname, @TeacherLname, @EmployeeNumber, @HireDate, @Salary)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFirstName);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLastName);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            // Close connection
            Conn.Close();
        }
    }
}
