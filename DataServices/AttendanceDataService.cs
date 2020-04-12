using System;
using System.Data.SqlClient;
using HousePointsApp.Interfaces;
using HousePointsApp.Models;
namespace HousePointsApp.DataServices

{
    public class AttendanceDataService : IAttendanceDataService
    {
        private String CONNECTION_STRING = @"Data Source=(localdb)\MSSQLLocalDB; 
                                             Initial Catalog = The_Learning_Factory_Points_System;";
        //private String CONNECTION_STRING = @"Data Source=localhost;Initial Catalog=The_Learning_Factory_Points_System;";
        //    "User ID=sa;Password=YourPasswordHere";

        public String GetCampusId(String studentId)
        {
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

            return campus_id;
        }

        // This function returns the check_in time for a specific attendance session

        public DateTime GetCheckIn(String sessionId)
        {
            // Create connection to database

            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            // Query attendance table for check_in time associated with session

            String GetCheckIn = "SELECT check_in FROM attendance WHERE session_id = " + sessionId;
            SqlCommand GetCheckInCommand = new SqlCommand(GetCheckIn, cnn);
            SqlDataReader GetCheckInReader = GetCheckInCommand.ExecuteReader();

            DateTime check_in = DateTime.Now;

            while (GetCheckInReader.Read())
            {
                check_in = Convert.ToDateTime(GetCheckInReader.GetValue(0));
            }

            cnn.Close();

            return check_in;
        }

        // This function calculates the points assigned to a specific session
        // given a check_in and check_out time

        public int GetSessionPoints(DateTime check_in, DateTime check_out)
        {
            // Calculate the difference in check in and check out time for student

            TimeSpan difference = check_out.Subtract(check_in);

            // Extract only the hour and minute difference between check in / check out

            int hours = difference.Hours;
            int minutes = difference.Minutes;

            /* Determine session points based on following model:
             *
             * 1) Student earns 1 point per hour spent at Learning Factory each day
             * 2) If minute value of time spent exceeds 30 minutes, they gain an extra point
             * 3) If minute value of time spent is less than 30 minutes, they do not gain extra point
             * 4) If a student forgets to check out, their time is auto-filled and they only get one point total
             *
             */

            int session_points;

            if (minutes > 30)
            {
                session_points = hours + 1;
            } else
            {
                session_points = hours;
            }

            return session_points;
        }

        // This function creates a new attendance record for a student

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

        // This function retrieves the latest attendance record for a specific student

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

            cnn.Close();
            return attendance;
        }

        // This function updates the check_out time and point value for an attendance record.
        // It also updates total_points for a student record.

        public Boolean UpdateSession(String sessionId)
        {
            // Find campus id for corresponding session id, as this will be
            // needed for updating the point value in the student table

            SqlConnection temp_cnn = new SqlConnection(CONNECTION_STRING);
            temp_cnn.Open();

            String GetCampusId = "SELECT campus_id FROM attendance WHERE " +
                "session_id = " + sessionId;
            SqlCommand GetCampusIdCommand = new SqlCommand(GetCampusId, temp_cnn);
            SqlDataReader GetCampusIdReader = GetCampusIdCommand.ExecuteReader();

            String campus_id = "";

            while (GetCampusIdReader.Read())
            {
                campus_id = GetCampusIdReader.GetValue(0).ToString();
            }

            temp_cnn.Close();

            // Create a new database connection that will be used to execute
            // remaining queries

            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            // Determine the check_in and check_out time for session that will
            // be used to determine point value assigned

            DateTime check_in = GetCheckIn(sessionId);

            DateTime check_out = DateTime.Now;
            string sqlFormattedDate = check_out.ToString("yyyy-MM-dd HH:mm:ss.fff");

            // Create SQL statements to update check_out time and point value
            // in attendance table and total_points in student table

            int session_points = GetSessionPoints(check_in, check_out);

            String UpdateSession = "UPDATE attendance SET check_out = '" +
                sqlFormattedDate + "' WHERE session_id = '" + sessionId + "';";

            String UpdatePoints = "UPDATE attendance SET session_points = " +
                session_points + " WHERE session_id = '" + sessionId + "';";

            String UpdateTotalPoints = "UPDATE student SET total_points = " +
                "total_points + " + session_points + " WHERE campus_id = '" +
                campus_id + "'";

            SqlCommand setUpdatedSession = new SqlCommand(UpdateSession, cnn);
            SqlCommand setUpdatedPoints = new SqlCommand(UpdatePoints, cnn);
            SqlCommand setUpdatedTotalPoints = new SqlCommand(UpdateTotalPoints, cnn);

            try
            {
                setUpdatedSession.ExecuteNonQuery();
                setUpdatedPoints.ExecuteNonQuery();
                setUpdatedTotalPoints.ExecuteNonQuery();

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
    }
}
