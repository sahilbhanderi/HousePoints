using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoApp
{
    public partial class ContactForm : System.Web.UI.UserControl
    {
        public string Name { get; set; }
        public string Message { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Name = txtName.Text;
                Message = txtMessage.Text;
            }
            else
            {
                txtName.Text = Name;
                txtMessage.Text = Message;
            }
        }
    }
}