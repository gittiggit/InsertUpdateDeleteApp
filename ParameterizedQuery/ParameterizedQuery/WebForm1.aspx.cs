using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;


namespace ParameterizedQuery
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
          string conStr =   ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using(SqlConnection con = new SqlConnection(conStr))
            {

                SqlCommand cmd = new SqlCommand("Select * from tblEmployee where Name like @EmployeeName",con);
                cmd.Parameters.AddWithValue("@EmployeeName", TextBox1.Text + "%");
                con.Open();

                GridView1.DataSource =  cmd.ExecuteReader();
                GridView1.DataBind();
            }
   
        }
    }
}