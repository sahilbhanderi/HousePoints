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
        private String CONNECTION_STRING = @"Server=np:\\.\pipe\LOCALDB#A3474A90\tsql\query; Initial Catalog=Capstone;";
            

        public Student GetStudent(String studentId)
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();
            String getStudentSql = "SELECT * FROM STUDENT WHERE student_id = " + studentId + ";";

            Student student = new Student();

            SqlCommand getStudentCommand = new SqlCommand(getStudentSql, cnn);
            SqlDataReader getStudentReader = getStudentCommand.ExecuteReader();

            try
            {
                while (getStudentReader.Read())
                {
                    student.student_id = getStudentReader.GetValue(0).ToString();
                    student.first_name = getStudentReader.GetValue(1).ToString();
                    student.last_name = getStudentReader.GetValue(2).ToString();
                    student.total_points = Convert.ToInt32(getStudentReader.GetValue(3));
                }
                cnn.Close();

                return student;
            } catch
            {
                cnn.Close();

                return null;
            }
        }

        public Boolean CreateStudent(String studentId)
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            // Lionpath view stuff will go here to look-up student-id
            String first_name = "Corona";
            String last_name = "Virus";

            String insertStudentSql = "INSERT INTO STUDENT " +
                "(student_id, first_name, last_name, total_points) " +
                "VALUES (" + studentId + ", '" + first_name + "', '" +
                last_name + "'," + 0 + ");";

            SqlCommand insertStudentCommand = new SqlCommand(insertStudentSql, cnn);

            try
            {
                insertStudentCommand.ExecuteNonQuery();
                return true;
            } catch
            {
                return false;
            }
        }

        public Boolean DeleteStudent(String studentId)
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String deleteAttendanceSql = "DELETE FROM attendance WHERE " +
                "student_id = " + studentId + ";";

            String deleteStudentSql = "DELETE FROM student WHERE " +
                "student_id = " + studentId + ";";

            SqlCommand deleteAttendanceCommand = new SqlCommand(deleteAttendanceSql, cnn);
            SqlCommand deleteStudentCommand = new SqlCommand(deleteStudentSql, cnn);

            try
            {
                deleteAttendanceCommand.ExecuteNonQuery();
                deleteStudentCommand.ExecuteNonQuery();

                return true;
            } catch
            {
                return false;
            }
        }
     
    }
}
