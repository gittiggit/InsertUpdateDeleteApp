using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CRUDOperationWinFormAppWithoutClass
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


        
    }
}
