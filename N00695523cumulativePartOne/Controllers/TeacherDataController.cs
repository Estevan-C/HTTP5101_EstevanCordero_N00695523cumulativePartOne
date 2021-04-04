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
        public List<Teacher> ListTeachers()
        {
            
            MySqlConnection Conn = School.AccessDatabase(); //Connects to Database

            Conn.Open(); // Opens connection to the database

            MySqlCommand cmd = Conn.CreateCommand(); // Makes a var name cmd to allow use to make the queries.

            cmd.CommandText = "Select * from Teachers"; // Place the following query into the cmd.CommandText

            MySqlDataReader ResultSet = cmd.ExecuteReader(); // Makes a var ResultSet to read and execute the command above.

            List<Teacher> Teachers = new List<Teacher> { }; // Empty list used to store the Teacher Objects.

            // While loop to read over and grab all of information store in the Teacher table.
            while (ResultSet.Read())
            {
                Teacher NewTeacher = new Teacher();

                NewTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                NewTeacher.TeacherFullName = ResultSet["teacherfname"].ToString() + " " + ResultSet["teacherlname"].ToString();
                // Combine the Teachers First and Last Name to a single string.
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

            cmd.CommandText = "Select * from Teachers where teacherid=" + teacherId;
            //SQL Query to grab all info from Teacher table base on ID.

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                SelectedTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                SelectedTeacher.TeacherFullName = ResultSet["teacherfname"].ToString() + " " + ResultSet["teacherlname"].ToString();
                SelectedTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                SelectedTeacher.HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                SelectedTeacher.Salary = Convert.ToDecimal(ResultSet["salary"]);
            }

            return SelectedTeacher;
        }
    }
}
