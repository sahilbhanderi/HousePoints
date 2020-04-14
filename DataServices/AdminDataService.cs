using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HousePointsApp.Interfaces;
using HousePointsApp.Models;

namespace HousePointsApp.DataServices
{
    public class AdminDataService : IAdminDataService
    {
        // Make sure to update to your own db name
        private String CONNECTION_STRING = @"Data Source=(localdb)\MSSQLLocalDB; 
                                             Initial Catalog = The_Learning_Factory_Points_System;";
        //private String CONNECTION_STRING = @"Data Source=localhost;Initial Catalog=The_Learning_Factory_Points_System;" +
        //    "User ID=sa;Password=YourPasswordHere";
        public int CheckPoints(String studentId)
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();
            String getStudentSql = "SELECT * FROM STUDENT WHERE student_id = " + studentId + ";";

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

        public Boolean IncrementPoints(String studentId, int point)
        { 
            int student_points = CheckPoints(studentId);
            if (student_points == -1)
            {
                return false;
            }

            student_points += point;

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
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

                cnn.Close();
                return false;
            }
        }

        public Boolean DecrementPoints(String studentId, int point)
        {
            int student_points = CheckPoints(studentId);
            if (student_points == -1)
            {
                return false;
            }
            student_points-= point;

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
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());

                cnn.Close();
                return false;
            }
        }

        public Boolean SetPoints(String studentId, int points)
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();
            int student_points = CheckPoints(studentId);
            // Check if student exists
            if (student_points == -1)
            {
                return false;
            }
            String Set_Points = "UPDATE student SET total_points = " +
            points + " WHERE student_id = " + studentId + ";";

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

            String addPrizeSql = "INSERT INTO prizes (prize_name, point_value) "+
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

        public List<String> GetAllPrizesName()
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();
            String getAllPrizeSql = "SELECT prize_name FROM PRIZES ORDER BY prize_id ASC;";
            List<String>PrizeList = new List<String>();
            
            SqlCommand getAllPrizeCommand = new SqlCommand(getAllPrizeSql, cnn);
            SqlDataReader getAllPrizeReader = getAllPrizeCommand.ExecuteReader();
            // if there is rows, means read success, then get points
            if (getAllPrizeReader.HasRows == true)
            {   int count = 1;
                while (getAllPrizeReader.Read())
                {
                    PrizeList.Add( $"{getAllPrizeReader.GetValue(0).ToString()}");
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
