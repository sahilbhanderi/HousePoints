using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoApp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["testDB"].ConnectionString;
            var insertStatement = "INSERT into LF (Number, Points) values (@Number, @Points)";
            var selectStatement = "UPDATE LF SET Points = Points + 1 WHERE Number = ";
            int add_entry = 0;
            string point_balance;
            /////////////////////////////////
            txtData.Focus();

            if (IsPostBack)
            {
                
                if( txtData.Text.Substring(0, 3) != "%A9")
                {

                    plsSwipe.Text = "Invalid ID. Please try again.";

                }
                else
                {
                   plsSwipe.Text = txtData.Text.Substring(2, 9);

                    using (var sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        selectStatement += txtData.Text.Substring(2, 9);
                        selectStatement += "; SELECT Points FROM LF WHERE Number = " + txtData.Text.Substring(2, 9);

                        using (var sqlSelect = new SqlCommand(selectStatement, sqlConnection))
                        {

                            try //to get entry from database
                            {
                                point_balance = sqlSelect.ExecuteScalar().ToString();

                                //GET NAME HERE FROM LIONPATH DB

                                //display point total 

                                Label2.Text = "Welcome back! Your current point balance is: " + point_balance;
                            }
                            catch
                            {
                                Label2.Text = "1st Time Welcome Message Here";
                                //create entry
                                add_entry = 1;
                                
                            }
                        }

                        if (add_entry == 1)
                        {
                            using (var sqlInsert = new SqlCommand(insertStatement, sqlConnection))
                            {
                                sqlInsert.Parameters.AddWithValue("Number", txtData.Text.Substring(2, 9));
                                sqlInsert.Parameters.AddWithValue("Points", 1);
                                sqlInsert.ExecuteNonQuery();
                            }
                        }

                    }

                    
                }

                
                //reset for next user
                txtData.Text = string.Empty;
                txtData.Focus();
                add_entry = 0;

                //timeout and then reset to default or blank message
            }
            else
            {
                txtData.Focus();

            }
        }
    }
}