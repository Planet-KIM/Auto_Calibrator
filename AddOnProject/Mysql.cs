using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AddOnProject
{
    public partial class Mysql : Form
    {
        private static string current_path = Directory.GetCurrentDirectory() + @"\Mysql";
        DirectoryInfo cur_path = new DirectoryInfo(current_path);


        // 밑의 정보를 암호화 해야합니다.
        private static string value = string.Empty;
        MySqlConnection conn;

        public class Database {
            public string SERVER = string.Empty;
            public string DB = string.Empty;
            public string UID = string.Empty;   
            public string PWD = string.Empty;
        }

        public Mysql()
        {
            InitializeComponent();

            // 폴더 유무 파악하고 없으면 생성하기
            if (!cur_path.Exists) {
                MessageBox.Show(current_path + " : " + " File Not Found!!");
                cur_path.Create(); }
            // 파일이 존재하면 json 파일을 읽어와서 초기화 작업하기
            else {
                using (StreamReader file = File.OpenText(current_path + @"\mysql_data.json"))
                using (JsonTextReader json_reader = new JsonTextReader(file)) {
                    JObject json = (JObject)JToken.ReadFrom(json_reader);
                    
                    Database _saradb = new Database();
                    _saradb.SERVER = (string)json["Server"].ToString();
                    _saradb.DB = (string)json["Database"].ToString();
                    _saradb.UID = (string)json["Uid"].ToString();
                    _saradb.PWD = (string)json["Pwd"].ToString();

                    value = "Server=" + _saradb.SERVER +
                        ";Database=" + _saradb.DB +
                        ";Uid=" + _saradb.UID +
                        ";Pwd=" + _saradb.PWD + ";";

                    conn = new MySqlConnection(value);
                }
            }
        }

        private void button_Mysql_Insert(object sender, EventArgs e)
        {
            String query = "INSERT INTO user(name, password) VALUES('" + nameBox.Text.ToString() + "', '" + pwdBox.Text.ToString() + "')";

            conn.Open();
            MySqlCommand command = new MySqlCommand(query, conn);

            try
            {
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("INSERT SUCCESS!!" + current_path);
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
