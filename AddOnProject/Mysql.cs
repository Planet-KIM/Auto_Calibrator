using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AddOnProject
{
    public partial class Mysql : Form
    {
        MySqlConnection conn = new MySqlConnection(
            "Server=localhost;Database=saradb;Uid=kdw59520;Pwd=rlaehdnjs12!"
            );

        public Mysql()
        {
            InitializeComponent();
        }

        private void button_Mysql_Insert(object sender, EventArgs e)
        {
            String query = "INSERT INTO user (name, password) VALUES('" + nameBox.Text.ToString() + "', " + pwdBox.Text.ToString() + ")";

            conn.Open();
            MySqlCommand command = new MySqlCommand(query, conn);

            try
            {
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("INSERT SUCCESS!!");
                }
                else {
                    MessageBox.Show("INSERT FAILED !! ");
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conn.Close();

        }
    }
}
