using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace SPOutputParameters
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                ListItem li = new ListItem("Select gender", "-1");
                //DropDownList1.Items.Add(li);
                DropDownList1.Items.Insert(0, li);

                ListItem li1 = new ListItem("Select department", "-1");
                //DropDownList1.Items.Add(li);
                DropDownList2.Items.Insert(0, li1);
            }
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "" || TextBox2.Text == "" || DropDownList1.SelectedIndex == 0 || DropDownList2.SelectedIndex == 0)
            {
                Response.Write("Enter the value.");
            }
            else 
            {
                Label1.Text = "Your id is : " + Convert.ToString(InsetrEmployeeAndGetEmpID());
            }
           
        }


        private int InsetrEmployeeAndGetEmpID() 
        {
            string conStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using(SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("spInsertEmployeeInfoAndGetEmpId",con);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", TextBox1.Text);
                    cmd.Parameters.AddWithValue("@Gender", DropDownList1.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@Salary", TextBox2.Text);
                    cmd.Parameters.AddWithValue("@DID", Convert.ToInt32(DropDownList2.SelectedItem.Value));

                    
               
                    
                SqlParameter outputParameter = new SqlParameter();
                outputParameter.ParameterName = "@EmployeeId";
                outputParameter.SqlDbType = System.Data.SqlDbType.Int;
                outputParameter.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(outputParameter);
                con.Open();
                cmd.ExecuteNonQuery();

                return (int)outputParameter.Value;
                



            }
        }
    }
}