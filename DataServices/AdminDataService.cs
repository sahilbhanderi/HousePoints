using System;
using System.Data.SqlClient;
using HousePointsApp.Interfaces;
using HousePointsApp.Models;

namespace HousePointsApp.DataServices
{
    public class AdminDataService : IAdminDataService
    {
        private String CONNECTION_STRING = @"Data Source=localhost;Initial Catalog=Capstone;" +
        "User ID=sa;Password=YourPassword";

        public int CheckPoints(String studentId)
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();
            String getStudentSql = "SELECT * FROM STUDENT WHERE student_id = " + studentId + ";";

            Student student = new Student();

            SqlCommand getStudentCommand = new SqlCommand(getStudentSql, cnn);
            SqlDataReader getStudentReader = getStudentCommand.ExecuteReader();

            while (getStudentReader.Read())
            {
                student.total_points = Convert.ToInt32(getStudentReader.GetValue(3));
            }
            cnn.Close();

            if (student.total_points is DBNull) student.total_points = null;

            return student.total_points;
        }

        public Boolean IncrementPoints(String studentId)
        { 
            int student_points = CheckPoints(studentId);
            student_points++;

            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String Increment_Points = "UPDATE student SET total_points = " +
            student_points + " WHERE student_id = " + studentId + ";";

            SqlCommand incrementPointsCommand = new SqlCommand(Increment_Points, cnn);

            try
            {
                incrementPointsCommand.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean DecrementPoints(String studentId)
        {
            int student_points = CheckPoints(studentId);
            student_points--;

            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String Increment_Points = "UPDATE student SET total_points = " +
            student_points + " WHERE student_id = " + studentId + ";";

            SqlCommand Increment_Points_Command = new SqlCommand(Increment_Points, cnn);

            try
            {
                Increment_Points_Command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean SetPoints(String studentId, int points)
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String Set_Points = "UPDATE student SET total_points = " +
            points + " WHERE student_id = " + studentId + ";";

            SqlCommand Set_Points_Command = new SqlCommand(Set_Points, cnn);

            try
            {
                Set_Points_Command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean DeleteAccount(String studentId)
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
            }
            catch
            {
                return false;
            }
        }

        public Boolean AddAccount(String studentID, String firstName, String lastName) 
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String addStudentSql = "INSERT INTO student (student_id, first_name, last_name, current_points) "+
                "VALUES(" + studentID + ", '" + firstName + "', '" + lastName +"', 0);";

            SqlCommand addStudentCommand = new SqlCommand(addStudentSql, cnn);

            try
            {
                addStudentCommand.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }

        }

        public Boolean AddPrize(String prize, int pointValue)
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String addPrizeSql = "INSERT INTO prizes (prize_name, point_value) "+
                "VALUES('" + prize + "', " + pointValue + ");";

            SqlCommand addPrizeCommand = new SqlCommand(addPrizeSql, cnn);

            try
            {
                addPrizeCommand.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean DeletePrize(String prize)
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String deletePrizeSql = "DELETE FROM prizes WHERE " +
                "prize_name = " + prize + ";";

            SqlCommand deletePrizeCommand = new SqlCommand(deletePrizeSql, cnn);

            try
            {
                deletePrizeCommand.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean UpdatePrizePoints(String prize, int prizePoints) 
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String Set_Points = "UPDATE prizes SET point_value = " +
            prizePoints + " WHERE prize_name = " + prize + ";";

            SqlCommand Set_Points_Command = new SqlCommand(Set_Points, cnn);

            try
            {
                Set_Points_Command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
