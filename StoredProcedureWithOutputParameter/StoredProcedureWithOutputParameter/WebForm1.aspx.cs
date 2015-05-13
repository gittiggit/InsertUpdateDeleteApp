using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;


namespace StoredProcedureWithOutputParameter
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string conStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using(SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spInsertEmployeeAndGetID", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name",TextBox1.Text);
                cmd.Parameters.AddWithValue("@Gender",DropDownList1.SelectedIndex.ToString());
                cmd.Parameters.AddWithValue("@Salary",TextBox2.Text);

                
                SqlParameter outputParameter = new SqlParameter();
                outputParameter.ParameterName = "@EmployeeID";
                outputParameter.DbType = System.Data.DbType.Int32;
                outputParameter.Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.Add(outputParameter);

                con.Open();
                cmd.ExecuteReader();

                string EmpID = outputParameter.Value.ToString();
                Label1.Text = "Employee ID : " + EmpID;

            }
        }
    }
}