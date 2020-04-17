using HousePointsApp.Interfaces;
using HousePointsApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HousePointsApp.DataServices
{
    public class StudentDataService : IStudentDataService
    {
        private String CONNECTION_STRING = @GetConnectionString();

        private static String GetConnectionString()
        {
            String json = File.ReadAllText("appsettings.json");

            // Query LionPath view for student's first name
            object v = JsonConvert.DeserializeObject(json);
            dynamic array = v;
            String CONNECTION_STRING = array["DB_CONNECTION_STRING"];
            return CONNECTION_STRING;
        }

        public String GetFirstName(String studentId)
        {

            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String getFirstNameSql = "SELECT first_name FROM View_Students WHERE emplid = '" + studentId + "'";
            SqlCommand getFirstNameCommand = new SqlCommand(getFirstNameSql, cnn);

            SqlDataReader getFirstNameReader = getFirstNameCommand.ExecuteReader();
            String first_name = "";

            while (getFirstNameReader.Read())
            {
                first_name = getFirstNameReader.GetValue(0).ToString();
            }

            cnn.Close();
            return first_name;
        }

        // This function queries a LionPath view to retrieve a student's last name

        public String GetLastName(String studentId)
        {
            // Query LionPath view for student's last name

            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String getLastNameSql = "SELECT last_name FROM View_Students WHERE emplid = '" + studentId + "'";
            SqlCommand getLastNameCommand = new SqlCommand(getLastNameSql, cnn);

            SqlDataReader getLastNameReader = getLastNameCommand.ExecuteReader();
            String last_name = "";

            while (getLastNameReader.Read())
            {
                last_name = getLastNameReader.GetValue(0).ToString();
            }

            cnn.Close();
            return last_name;
        }

        // This function queries a LionPath view to retrieve a student's campus id

        public String GetCampusId(String studentId)
        {
            // Query LionPath view for student's campus id

            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String getCampusIdSql = "SELECT campus_id FROM View_Students WHERE emplid = '" + studentId + "'";
            SqlCommand getCampusIdCommand = new SqlCommand(getCampusIdSql, cnn);

            SqlDataReader getCampusIdReader = getCampusIdCommand.ExecuteReader();
            String campus_id = "";

            while (getCampusIdReader.Read())
            {
                campus_id = getCampusIdReader.GetValue(0).ToString();
            }

            cnn.Close();
            return campus_id;
        }

        // This function retrieves the record for a specific student

        public Student GetStudent(String studentId)
        {
            // Create and execute SQL query to retrieve a student record

            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();
            String getStudentSql = "SELECT * FROM student WHERE student_id = '" + studentId + "';";

            Student student = new Student();

            SqlCommand getStudentCommand = new SqlCommand(getStudentSql, cnn);


            try
            {
                SqlDataReader getStudentReader = getStudentCommand.ExecuteReader();
                if (getStudentReader.HasRows == false)
                {
                    return null;
                }
                while (getStudentReader.Read())
                {
                    student.student_id = getStudentReader.GetValue(0).ToString();
                    student.campus_id = getStudentReader.GetValue(1).ToString();
                    student.first_name = getStudentReader.GetValue(2).ToString();
                    student.last_name = getStudentReader.GetValue(3).ToString();
                    student.total_points = Convert.ToInt32(getStudentReader.GetValue(4));
                }
                cnn.Close();

                return student;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

                cnn.Close();
                return null;
            }
        }

        // This function creates a record for a new student

        public Boolean CreateStudent(String studentId, String house)
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            //Check if student valid first
            String checkValidStudent = "SELECT * FROM View_Students WHERE emplid = '" + studentId + "';";

            SqlCommand checkValidStudentCommand = new SqlCommand(checkValidStudent, cnn);
            try
            {
                SqlDataReader checkValidStudentReader = checkValidStudentCommand.ExecuteReader();
                if (checkValidStudentReader.HasRows == false) //Student DNE in psu server
                    return false;

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }

            cnn.Close();

            // Get first name, last name, and campus id for corresponding
            // student id

            String first_name = GetFirstName(studentId);
            String last_name = GetLastName(studentId);
            String campus_id = GetCampusId(studentId);

            // Create and execute a SQL query to create a new student record

            SqlConnection cnn_insert = new SqlConnection(CONNECTION_STRING);
            cnn_insert.Open();

            String insertStudentSql = "INSERT INTO STUDENT " +
                "(student_id, campus_id, first_name, last_name, total_points, house_assignment) " +
                "VALUES (" + studentId + ", '" + campus_id + "', '" +
                first_name + "', '" + last_name + "'," + 0 + ", '" + house + "');";

            SqlCommand insertStudentCommand = new SqlCommand(insertStudentSql, cnn_insert);

            try
            {
                insertStudentCommand.ExecuteNonQuery();

                cnn_insert.Close();
                return true;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

                cnn_insert.Close();
                return false;
            }
        }

        // This function deletes both the student and all attendance records
        // for a specific student

        public Boolean DeleteStudent(String userId)
        {
            // Create and execute a SQL query to delete a student's record based
            // on the campusId (abc123) provided by administrator

            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String deleteAttendanceSql = "DELETE FROM attendance WHERE " +
                "campus_id = '" + userId + "';";

            String deleteStudentSql = "DELETE FROM student WHERE " +
                "campus_id = '" + userId + "';";

            SqlCommand deleteAttendanceCommand = new SqlCommand(deleteAttendanceSql, cnn);
            SqlCommand deleteStudentCommand = new SqlCommand(deleteStudentSql, cnn);

            try
            {
                deleteAttendanceCommand.ExecuteNonQuery();
                deleteStudentCommand.ExecuteNonQuery();

                cnn.Close();
                return true;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

                cnn.Close();
                return false;
            }
        }

        // Return the five student with highest points, if there is less than 5 return all students available.
        public List<Student> GetTopFiveScoringStudent()
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();
            List<Student> TopFiveStudent = new List<Student>();
            String getTopFiveStudentSql = "SELECT TOP 5 * FROM student ORDER BY total_points DESC;";
            SqlCommand getTopFiveStudentCommand = new SqlCommand(getTopFiveStudentSql, cnn);
            try
            {
                SqlDataReader topFiveStudentReader = getTopFiveStudentCommand.ExecuteReader();
                while (topFiveStudentReader.Read())
                {
                    Student tempStudent = new Student();
                    tempStudent.first_name = topFiveStudentReader.GetValue(2).ToString();
                    tempStudent.last_name = topFiveStudentReader.GetValue(3).ToString();
                    tempStudent.total_points = Convert.ToInt32(topFiveStudentReader.GetValue(4));
                    TopFiveStudent.Add(tempStudent);
                }

                cnn.Close();
                return TopFiveStudent;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

                cnn.Close();
                return null;
            }

        }

        public (List<House>, int) GetHousePoints()
        {
            // Open a connection to SQL Server

            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            // Set up query that will get list of houses and their points in
            // descending order

            List<House> housePoints = new List<House>();

            String selectHouses = "SELECT house_assignment, SUM(total_points) " +
                "FROM student GROUP BY house_assignment ORDER BY SUM(total_points) DESC;";
            SqlCommand selectHousesCommand = new SqlCommand(selectHouses, cnn);

            int houses_filled = 0;

            try
            {
                SqlDataReader housePointsReader = selectHousesCommand.ExecuteReader();
                while (housePointsReader.Read())
                {
                    House tempHouse = new House();

                    houses_filled++;

                    tempHouse.house_name = housePointsReader.GetValue(0).ToString();
                    tempHouse.house_points = Convert.ToInt32(housePointsReader.GetValue(1));

                    housePoints.Add(tempHouse);
                }

                cnn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

                cnn.Close();
                return (null, 0);
            }

            return (housePoints, houses_filled);
        }

        public List<(String campus_id, String house)> GetHouseAssignments()
        {
            // Open a connection to the SQL server

            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            // Set up query to get student campus_ids and house assignments

            String getHouses = "SELECT campus_id, house_assignment FROM student " +
                "ORDER BY campus_id";
            SqlCommand getHousesCommand = new SqlCommand(getHouses, cnn);

            List<(String campus_id, String house)> houseList = new List<(String campus_id, String house)>();

            try
            {
                SqlDataReader getHousesReader = getHousesCommand.ExecuteReader();
                while (getHousesReader.Read())
                {
                    (String id, String assignment) temp;

                    temp.id = getHousesReader.GetValue(0).ToString();
                    temp.assignment = getHousesReader.GetValue(1).ToString();

                    houseList.Add(temp);
                }
                cnn.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

                cnn.Close();
                return null;
            }

            return houseList;
        }
    }
}

