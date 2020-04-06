using System;
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
            SqlDataReader getStudentReader = getStudentCommand.ExecuteReader();
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
            {// set to -1 if fail to get total points
                student.total_points = -1;
            }
            return student.total_points;
        }

        public Boolean IncrementPoints(String studentId, int point)
        { 
            int student_points = CheckPoints(studentId);
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
            catch
            {
                return false;
            }
        }

        public Boolean DecrementPoints(String studentId, int point)
        {
            int student_points = CheckPoints(studentId);
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
                "prize_name = '" + prize + "';";

            SqlCommand deletePrizeCommand = new SqlCommand(deletePrizeSql, cnn);

            /*
             *try
             {
                 deletePrizeCommand.ExecuteNonQuery();

                 return true;
             }
             catch
             {
                 return false;
             }
             */
            deletePrizeCommand.ExecuteNonQuery();

            return true;
        }

        public Boolean UpdatePrizePoints(String prize, int prizePoints) 
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();

            String Set_Points = $"UPDATE prizes SET point_value = '{prizePoints}' WHERE prize_name = '{prize};'";

            SqlCommand Set_Points_Command = new SqlCommand(Set_Points, cnn);

            /*  try
              {
                  Set_Points_Command.ExecuteNonQuery();
                  return true;
              }
              catch
              {
                  return false;
              }
          */
            Set_Points_Command.ExecuteNonQuery();
            return true;
        }

        public string GetAllPrizes()
        {
            SqlConnection cnn = new SqlConnection(CONNECTION_STRING);
            cnn.Open();
            String getAllPrizeSql = "SELECT * FROM PRIZES ;";
            String PrizeList = "Prize List:\n";
            

            SqlCommand getAllPrizeCommand = new SqlCommand(getAllPrizeSql, cnn);
            SqlDataReader getAllPrizeReader = getAllPrizeCommand.ExecuteReader();
            // if there is rows, means read success, then get points
            if (getAllPrizeReader.HasRows == true)
            {
                while (getAllPrizeReader.Read())
                {
                    PrizeList += "Item: "+ getAllPrizeReader.GetValue(1).ToString() + 
                                 " Value: " + getAllPrizeReader.GetValue(2).ToString() + '\n';
                }
                cnn.Close();
            }
            else
            {// set to -1 if fail to get total points
                PrizeList = null;
            }
            return PrizeList;
        }
    }
}
