using System;
using System.Data.SqlClient;
using HousePointsApp.Interfaces;
using HousePointsApp.Models;
namespace HousePointsApp.DataServices

{
    public class AttendanceDataService : IAttendanceDataService
    {
        private String CONNECTION_STRING = @"Data Source=localhost;Initial Catalog=The_Learning_Factory_Points_System;" +
            "User ID=sa;Password=YourPasswordHere";

        public String GetCampusId(String studentId)
        {
            // Lionpath View Query

            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String getCampusIdSql = "SELECT campus_id FROM vw_LF_Students WHERE student_id =" + studentId;
            SqlCommand getCampusIdCommand = new SqlCommand(getCampusIdSql, cnn);

            SqlDataReader getCampusIdReader = getCampusIdCommand.ExecuteReader();
            String campus_id = "";

            while (getCampusIdReader.Read())
            {
                campus_id = getCampusIdReader.GetValue(0).ToString();
            }

            return campus_id;
        }

        public Boolean CreateAttendance(String studentId)
        {
            // Get the campus id (abc123) for given student id
            String campus_id = GetCampusId(studentId);

            // Create and execute a SQL query that creates a new attendance
            // record for a student

            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

            String createAttendanceSql = "INSERT INTO attendance (campus_id, check_in) VALUES('" + campus_id + "', '" + sqlFormattedDate + "');";

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
            // Get campus id from Lionpath view
            String campusId = GetCampusId(studentId);

            // Create and execute SQL query to find latest attendance log for
            // a student

            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            Attendance attendance = new Attendance();

            String getLatestAttendance = "SELECT * FROM attendance WHERE session_id = (SELECT MAX(session_id) FROM attendance " +
                "WHERE campus_id = '" + campusId + "');";

            SqlCommand getLatestAttendanceCommand = new SqlCommand(getLatestAttendance, cnn);

            SqlDataReader getLatestAttendanceReader = getLatestAttendanceCommand.ExecuteReader();

            while (getLatestAttendanceReader.Read())
            {
                attendance.session_id = Convert.ToInt32(getLatestAttendanceReader.GetValue(0));
                attendance.campus_id = getLatestAttendanceReader.GetValue(1).ToString();
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
            // Create and execute SQL query to update the check out time and points
            // for a student's attendance record

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
