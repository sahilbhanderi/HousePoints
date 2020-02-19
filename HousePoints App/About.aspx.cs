using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoApp
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Title = "From code behind";
                Trace.Write("From code", "No postback");
            }
            else
            {
                Title = "Postback data: " + txtData.Text;
                Trace.Write("From code", "Postback: " + txtData.Text);
            }
        }

        protected void btnSendInfo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtData.Text))
            {
                throw new ApplicationException("Something unthinkable happened!");
            }
        }

        protected void Page_Error(object sender, EventArgs e)
        {
            // Log the information somewhere
            var ex = Server.GetLastError();
            Server.ClearError();
            Response.Write(ex.Message);
        }
    }
}