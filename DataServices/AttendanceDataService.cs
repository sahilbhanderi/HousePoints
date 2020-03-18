using System;
using System.Data.SqlClient;
using HousePointsApp.Interfaces;
using HousePointsApp.Models;
namespace HousePointsApp.DataServices
{
    public class AttendanceDataService : IAttendanceDataService
    {
        private String CONNECTION_STRING = @"Data Source=localhost;Initial Catalog=Capstone;" +
        "User ID=sa;Password=CBwbi2005";

        public Boolean CreateAttendance(String studentId)
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Console.WriteLine(sqlFormattedDate.ToString());

            String createAttendanceSql = "INSERT INTO attendance (student_id, check_in) " +
                "VALUES(" + studentId + ", '" + sqlFormattedDate + "');";

            SqlCommand createAttendanceCommand = new SqlCommand(createAttendanceSql, cnn);

            try
            {
                createAttendanceCommand.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Attendance GetLatestAttendance(String studentId)
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            Attendance attendance = new Attendance();

            String getLatestAttendance = "SELECT * FROM attendance WHERE session_id = (SELECT MAX(session_id) FROM attendance " +
                "WHERE student_id =" + studentId + ");";

            SqlCommand getLatestAttendanceCommand = new SqlCommand(getLatestAttendance, cnn);

            SqlDataReader getLatestAttendanceReader = getLatestAttendanceCommand.ExecuteReader();

            while (getLatestAttendanceReader.Read())
            {
                attendance.session_id = Convert.ToInt32(getLatestAttendanceReader.GetValue(0));
                attendance.student_id = getLatestAttendanceReader.GetValue(1).ToString();
                attendance.check_in = Convert.ToDateTime(getLatestAttendanceReader.GetValue(2));
                if (getLatestAttendanceReader.GetValue(3) is DBNull)
                {
                    attendance.check_out = null;
                }
                else
                {
                    attendance.check_out = Convert.ToDateTime(getLatestAttendanceReader.GetValue(3));

                }

                if (getLatestAttendanceReader.GetValue(4) is DBNull)
                {
                    attendance.session_points = null;
                }
                else
                {
                    attendance.session_points = Convert.ToInt32(getLatestAttendanceReader.GetValue(4));
                }

            }

            return attendance;
        }

        public Boolean UpdateSession(String sessionId)
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

            String UpdateSession = "UPDATE attendance SET check_out = '" +
                sqlFormattedDate + "' WHERE session_id = " + sessionId + ";";

            String UpdatePoints = "UPDATE attendance SET session_points = " +
                1 + " WHERE session_id = " + sessionId + ";";

            SqlCommand setUpdatedSession = new SqlCommand(UpdateSession, cnn);
            SqlCommand setUpdatedPoints = new SqlCommand(UpdatePoints, cnn);

            try
            {
                setUpdatedSession.ExecuteNonQuery();
                setUpdatedPoints.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
