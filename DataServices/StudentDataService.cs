using HousePointsApp.Interfaces;
using HousePointsApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HousePointsApp.DataServices
{
    public class StudentDataService : IStudentDataService
    {
        private String CONNECTION_STRING = @"Data Source=(localdb)\MSSQLLocalDB; 
                                             Initial Catalog = The_Learning_Factory_Points_System;";
        //private String CONNECTION_STRING = @"Data Source=localhost;Initial Catalog=The_Learning_Factory_Points_System;" +
        //    "User ID=sa;Password=YourPasswordHere";

        public String GetFirstName(String studentId)
        {
            // Query LionPath view for student's first name

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
            SqlDataReader getStudentReader = getStudentCommand.ExecuteReader();

            try
            {
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
            } catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

                cnn.Close();
                return null;
            }
        }

        // This function creates a record for a new student

        public Boolean CreateStudent(String studentId)
        {
            // Get first name, last name, and campus id for corresponding
            // student id

            String first_name = GetFirstName(studentId);
            String last_name = GetLastName(studentId);
            String campus_id = GetCampusId(studentId);

            // Create and execute a SQL query to create a new student record

            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String insertStudentSql = "INSERT INTO STUDENT " +
                "(student_id, campus_id, first_name, last_name, total_points) " +
                "VALUES (" + studentId + ", '" + campus_id + "', '" +
                first_name + "', '" + last_name + "'," + 0 + ");";

            SqlCommand insertStudentCommand = new SqlCommand(insertStudentSql, cnn);

            try
            {
                insertStudentCommand.ExecuteNonQuery();

                cnn.Close();
                return true;
            } catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

                cnn.Close();
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
                "'campus_id'= " + userId + ";";

            String deleteStudentSql = "DELETE FROM student WHERE " +
                "'campus_id'= " + userId + ";";

            SqlCommand deleteAttendanceCommand = new SqlCommand(deleteAttendanceSql, cnn);
            SqlCommand deleteStudentCommand = new SqlCommand(deleteStudentSql, cnn);

            try
            {
                deleteAttendanceCommand.ExecuteNonQuery();
                deleteStudentCommand.ExecuteNonQuery();

                cnn.Close();
                return true;
            } catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

                cnn.Close();
                return false;
            }
        }

    }
}
