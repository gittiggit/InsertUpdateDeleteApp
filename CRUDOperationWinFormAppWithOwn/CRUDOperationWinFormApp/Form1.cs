using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace CRUDOperationWinFormApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            genderComboBox.Items.Add("Male");
            genderComboBox.Items.Add("Female");

            educationLevelComboBox.Items.Add("SSC");
            educationLevelComboBox.Items.Add("HSC");
            educationLevelComboBox.Items.Add("BSC");
            educationLevelComboBox.Items.Add("MSC");
        }


        public string studentNameStr = "";

        public bool IsUpdateMode = false;

        string conStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        
        private void saveButton_Click(object sender, EventArgs e)
        {
           // string conStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            string studentName = nameTextBox.Text;
            string regNo = regTextBox.Text;
            string studentGender = Convert.ToString(genderComboBox.SelectedItem);
            string studentAge = ageTextBox.Text;
            string educatonLevel = Convert.ToString(educationLevelComboBox.SelectedItem);


            if (IsUpdateMode)
            {
                if (educationLevelComboBox.SelectedItem != null)
                {
                    string query = "update tblStudent set Name ='" + nameTextBox.Text + "',RegNo='" + regTextBox.Text + "',Education='" + educationLevelComboBox.SelectedItem + "'";
                    using (SqlConnection con = new SqlConnection(conStr))
                    {
                        SqlCommand cmd = new SqlCommand(query, con);
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Updated Successfully");
                            saveButton.Text = "Save";
                            genderComboBox.Show();
                            ageTextBox.Show();
                            IsUpdateMode = false;
                            studentNameStr = "";
                        }
                        else
                        {
                            MessageBox.Show("Not Updated . ");
                        }

                    }
                }

                else 
                {
                    MessageBox.Show("Select an item from the combobox.");
                }
                
            }

            else 
            {

                if (studentName == "" || regNo == "" || studentGender == "" || studentAge == "" || educatonLevel == "")
                {
                    MessageBox.Show("Please enter the value.");
                }
                else
                {
                    if (IsRegNoExists(regNo))
                    {
                        MessageBox.Show("Registration number exist . Enter reg. no. correctly.");
                    }
                    else
                    {
                        InsertInformation();
                    }


                }

            }




            


            
        }


        private void InsertInformation() 
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("insert into tblStudent values('" + nameTextBox.Text + "','" + regTextBox.Text + "','" + genderComboBox.SelectedItem
                    + "','" + ageTextBox.Text + "','" + educationLevelComboBox.SelectedItem + "')", con);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Inserted successfully.");
                }
                else
                {
                    MessageBox.Show("Not inserted .");
                }



            }
        }

        private bool IsRegNoExists(string registrationNumber) 
        {
            using (SqlConnection con = new SqlConnection(conStr)) 
            {
                SqlCommand cmd = new SqlCommand("select * from tblStudent where RegNo ='"+regTextBox.Text+"'",con);
                bool isRegNoExists = false; 
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader()) 
                {
                    while (rdr.Read())
                    {
                        isRegNoExists = true;
                        break;
                    }
                
                }
                

                return isRegNoExists;

            }
        
        }

        //private void showAllStudentButton_Click(object sender, EventArgs e)
        //{
        //    List<Student> studentList = new List<Student>();
        //   // studentListView.Items.Clear();

        //    using(SqlConnection con = new SqlConnection(conStr))
        //    {
        //        SqlCommand cmd = new SqlCommand("select Name,RegNo,Education from tblStudent", con);
               
        //        con.Open();
        //        using (SqlDataReader rdr = cmd.ExecuteReader())
        //        {
                
        //            while(rdr.Read())
        //            {
        //                Student registeredStudent = new Student();
        //                registeredStudent.name = Convert.ToString(rdr["Name"]);
        //                registeredStudent.registrationNo = Convert.ToString(rdr["RegNo"]);
        //                registeredStudent.levelOfEducation = Convert.ToString(rdr["Education"]);
        //                studentList.Add(registeredStudent);


        //                // Without using Student class , you can use the following four commented lines
        //                // of code to add the student in the ListView control

        //                //ListViewItem listViewItem = new ListViewItem((string)rdr["Name"]);
        //                //listViewItem.SubItems.Add((string)rdr["RegNo"]);
        //                //listViewItem.SubItems.Add((string)rdr["Education"]);


        //               // studentListView.Items.Add(listViewItem);
                       
        //            }
                   
        //       }
        //    }

        //  //  LoadAllStudent(studentList);
        //}

        private void LoadAllStudent(List<Student> students)
        {
            studentListView.Items.Clear();

            foreach (Student student in students)
            {
                ListViewItem listViewItem = new ListViewItem(student.name);

                listViewItem.SubItems.Add(student.registrationNo);
                listViewItem.SubItems.Add(student.levelOfEducation);

                studentListView.Items.Add(listViewItem);


            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Student> studentList = new List<Student>();
            // studentListView.Items.Clear();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("select Name,RegNo,Education from tblStudent", con);

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {

                    while (rdr.Read())
                    {
                        Student registeredStudent = new Student();
                        registeredStudent.name = Convert.ToString(rdr["Name"]);
                        registeredStudent.registrationNo = Convert.ToString(rdr["RegNo"]);
                        registeredStudent.levelOfEducation = Convert.ToString(rdr["Education"]);
                        studentList.Add(registeredStudent);


                        // Without using Student class , you can use the following four commented lines
                        // of code to add the student in the ListView control

                        //ListViewItem listViewItem = new ListViewItem((string)rdr["Name"]);
                        //listViewItem.SubItems.Add((string)rdr["RegNo"]);
                        //listViewItem.SubItems.Add((string)rdr["Education"]);


                        // studentListView.Items.Add(listViewItem);

                    }

                }
            }

            LoadAllStudent(studentList);
        }

        private void studentListView_DoubleClick(object sender, EventArgs e)
        {
            //IsUpdateMode = true;
            //saveButton.Text = "Update";

            
            ListViewItem item = studentListView.SelectedItems[0];

            studentNameStr = (string)item.Text;

            Student student = GetStudentByName(studentNameStr);

            if(student !=null)
            {


                IsUpdateMode = true;
                saveButton.Text = "Update";
                studentNameStr = student.name;

                nameTextBox.Text = student.name;
                regTextBox.Text = student.registrationNo;

                ageTextBox.Hide();
                genderComboBox.Hide();

            }


        }



        private Student GetStudentByName(string selectedName) 
        {
            SqlConnection connection = new SqlConnection(conStr);

            //2. write query 

            string query = "SELECT * FROM tblStudent WHERE Name ='" +selectedName+"'";


            // 3. execute query 

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<Student> studentList = new List<Student>();

            while (reader.Read())
            {

                Student student = new Student();
                student.name =(string) reader["Name"];
                student.registrationNo = (string)reader["RegNo"];
                student.levelOfEducation = (string)reader["Education"];
                

                studentList.Add(student);

            }
            reader.Close();
            connection.Close();

            return studentList.FirstOrDefault();
        }

       
    }
}
