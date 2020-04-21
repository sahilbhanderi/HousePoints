using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using HousePointsApp.Interfaces;
using HousePointsApp.Models;
using Newtonsoft.Json;

namespace HousePointsApp.DataServices
{
    public class AdminDataService : IAdminDataService
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

        // This function queries a LionPath view to retrieve a student's student id

        public String GetStudentId(String campusId)
        {

            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String getStudentIdSql = "SELECT emplid FROM View_Students WHERE campus_id = '" + campusId + "'";
            SqlCommand getStudentIdCommand = new SqlCommand(getStudentIdSql, cnn);

            SqlDataReader getStudentIdReader = getStudentIdCommand.ExecuteReader();
            String student_id = "";

            while (getStudentIdReader.Read())
            {
                student_id = getStudentIdReader.GetValue(0).ToString();
            }

            cnn.Close();
            return student_id;
        }

        // This function queries a LionPath view to retrieve a student's first name

        public String GetFirstName(String campusId)
        {

            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String getFirstNameSql = "SELECT first_name FROM View_Students WHERE campus_id = '" + campusId + "'";
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

        public String GetLastName(String campusId)
        {
            // Query LionPath view for student's last name

            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String getLastNameSql = "SELECT last_name FROM View_Students WHERE campus_id = '" + campusId + "'";
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

        public int CheckPoints(String campusId)
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();
            String getStudentSql = "SELECT * FROM STUDENT WHERE campus_id = '" + campusId + "';";

            Student student = new Student();

            SqlCommand getStudentCommand = new SqlCommand(getStudentSql, cnn);
            SqlDataReader getStudentReader = getStudentCommand.ExecuteReader(); ;

            // if there is rows, means read success, then get points
            if (getStudentReader.HasRows == true)
            {
                while (getStudentReader.Read())
                {
                    student.total_points = Convert.ToInt32(getStudentReader.GetValue(4));
                }
                cnn.Close();
            }
            else
            {
                cnn.Close();

                student.total_points = -1;
            }
            return student.total_points;
        }

        public Boolean IncrementPoints(String campusId, int point)
        {
            int student_points = CheckPoints(campusId);
            if (student_points == -1)
            {
                return false;
            }

            student_points += point;

            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String Increment_Points = "UPDATE student SET total_points = " +
            student_points + " WHERE campus_id = '" + campusId + "';";

            SqlCommand incrementPointsCommand = new SqlCommand(Increment_Points, cnn);

            try
            {
                incrementPointsCommand.ExecuteNonQuery();
                return true;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

                cnn.Close();
                return false;
            }
        }

        public Boolean DecrementPoints(String campusId, int point)
        {
            int student_points = CheckPoints(campusId);
            if (student_points == -1)
            {
                return false;
            }
            student_points -= point;

            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String Increment_Points = "UPDATE student SET total_points = " +
            student_points + " WHERE campus_id = '" + campusId + "';";

            SqlCommand Increment_Points_Command = new SqlCommand(Increment_Points, cnn);

            try
            {
                Increment_Points_Command.ExecuteNonQuery();
                return true;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

                cnn.Close();
                return false;
            }
        }

        public Boolean SetPoints(String campusId, int points)
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();
            int student_points = CheckPoints(campusId);
            // Check if student exists
            if (student_points == -1)
            {
                return false;
            }
            String Set_Points = "UPDATE student SET total_points = " +
            points + " WHERE campus_id = '" + campusId + "';";

            SqlCommand Set_Points_Command = new SqlCommand(Set_Points, cnn);

            try
            {
                Set_Points_Command.ExecuteNonQuery();
                return true;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

                cnn.Close();
                return false;
            }
        }

        public Boolean AddAccount(String campusId)
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String addStudentSql = "INSERT INTO student (student_id, campus_id, first_name, last_name, total_points, house_assignment) " +
                "VALUES('" + GetStudentId(campusId) + "', '" + campusId + "', '" + GetFirstName(campusId) +
                "', '" + GetLastName(campusId) + "', 0, 'Nittany House');";

            SqlCommand addStudentCommand = new SqlCommand(addStudentSql, cnn);

            try
            {
                addStudentCommand.ExecuteNonQuery();

                return true;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

                cnn.Close();
                return false;
            }

        }

        public Boolean AddPrize(String prize, int pointValue)
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String addPrizeSql = "INSERT INTO prizes (prize_name, point_value) " +
                "VALUES('" + prize + "', " + pointValue + ");";

            SqlCommand addPrizeCommand = new SqlCommand(addPrizeSql, cnn);

            try
            {
                addPrizeCommand.ExecuteNonQuery();

                return true;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

                cnn.Close();
                return false;
            }
        }

        public Boolean DeletePrize(String prize)
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String deletePrizeSql = "DELETE FROM prizes WHERE " +
                "prize_name = '" + prize + "';";

            SqlCommand deletePrizeCommand = new SqlCommand(deletePrizeSql, cnn);

            try
            {
                if (CheckPrizeExist(prize))
                {
                    deletePrizeCommand.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

                cnn.Close();
                return false;
            }
        }

        public Boolean UpdatePrizePoints(String prize, int prizePoints)
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String Set_Points = "UPDATE prizes SET point_value = " +
                prizePoints + " WHERE prize_name = '" + prize + "';";
            SqlCommand Set_Points_Command = new SqlCommand(Set_Points, cnn);

            try
            {
                if (CheckPrizeExist(prize))
                {
                    Set_Points_Command.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

                cnn.Close();
                return false;
            }

        }

        public Boolean CheckPrizeExist(String prize)
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();
            String CheckPrizeExist = "SELECT * FROM prizes WHERE prize_name = '" + prize + "';";
            SqlCommand CheckPrizeExistCmd = new SqlCommand(CheckPrizeExist, cnn);
            SqlDataReader CheckPrizeExistReader = CheckPrizeExistCmd.ExecuteReader();
            if (CheckPrizeExistReader.HasRows == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<int> GetPrizesId()
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String getPrizeIdSql = "SELECT prize_id FROM prizes ORDER BY prize_id ASC;";
            List<int> PrizeList = new List<int>();

            SqlCommand getPrizeIdCommand = new SqlCommand(getPrizeIdSql, cnn);
            SqlDataReader getPrizeIdReader = getPrizeIdCommand.ExecuteReader();

            if (getPrizeIdReader.HasRows == true)
            {
                while (getPrizeIdReader.Read())
                {
                    PrizeList.Add(Convert.ToInt32(getPrizeIdReader.GetValue(0)));
                }
                cnn.Close();
            }
            else
            {
                PrizeList = null;
                cnn.Close();
            }
            return PrizeList;
        }

        public List<String> GetAllPrizesName()
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();
            String getAllPrizeSql = "SELECT prize_name FROM prizes ORDER BY prize_id ASC;";
            List<String> PrizeList = new List<String>();

            SqlCommand getAllPrizeCommand = new SqlCommand(getAllPrizeSql, cnn);
            SqlDataReader getAllPrizeReader = getAllPrizeCommand.ExecuteReader();
            // if there is rows, means read success, then get points
            if (getAllPrizeReader.HasRows == true)
            {
                int count = 1;
                while (getAllPrizeReader.Read())
                {
                    PrizeList.Add($"{getAllPrizeReader.GetValue(0).ToString()}");
                    count++;

                }
                cnn.Close();
            }
            else
            {// set to null if fail
                PrizeList = null;
                cnn.Close();
            }
            return PrizeList;
        }

        public List<String> GetAllPrizesValue()
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();
            String getAllPrizeSql = "SELECT point_value FROM PRIZES ORDER BY prize_id ASC;";
            List<String> PrizeList = new List<String>();

            SqlCommand getAllPrizeCommand = new SqlCommand(getAllPrizeSql, cnn);
            SqlDataReader getAllPrizeReader = getAllPrizeCommand.ExecuteReader();
            // if there is rows, means read success, then get points
            if (getAllPrizeReader.HasRows == true)
            {
                int count = 1;
                while (getAllPrizeReader.Read())
                {
                    PrizeList.Add($"{getAllPrizeReader.GetValue(0).ToString()}");
                    count++;

                }
                cnn.Close();
            }
            else
            {// set to null if fail
                PrizeList = null;
                cnn.Close();
            }
            return PrizeList;
        }

        public List<Student> GetAllStudents()
        //Returns all students and their total points from the student table,
        //null if table empty
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();
            String getAllStudents = "SELECT * FROM student";
            List<Student> studentList = new List<Student>();

            SqlCommand getAllStudentsCommand = new SqlCommand(getAllStudents, cnn);

            try
            {
                SqlDataReader getAllStudentsReader = getAllStudentsCommand.ExecuteReader();
                if (getAllStudentsReader.HasRows == true)
                {
                    Student tempStudent = new Student();
                    while (getAllStudentsReader.Read())
                    {
                        tempStudent.first_name = getAllStudentsReader.GetValue(2).ToString();
                        tempStudent.last_name = getAllStudentsReader.GetValue(3).ToString();
                        tempStudent.total_points = Convert.ToInt32(getAllStudentsReader.GetValue(4));
                        studentList.Add(tempStudent);
                    }

                    cnn.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

                studentList = null;
                cnn.Close();
            }

            return studentList;
        }

        public Boolean CheckIsAdmin(String AdminID, String password)
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();
            String getAllAdmin = $"SELECT access_code FROM admin WHERE employee_id = '{AdminID}';";
            Boolean isAdmin = false;
            SqlCommand getAllAdminCommand = new SqlCommand(getAllAdmin, cnn);
            try
            {
                SqlDataReader getAllAdminReader = getAllAdminCommand.ExecuteReader();
                // if there is rows, means read success, then get points
                if (getAllAdminReader.HasRows == true)
                {
                    while (getAllAdminReader.Read())
                    {
                        if (password == $"{getAllAdminReader.GetValue(0).ToString()}")
                        {
                            isAdmin = true;
                            break;
                        }
                    }

                }
                cnn.Close();
                return isAdmin;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

                cnn.Close();
                return isAdmin;
            }

        }
    }
}