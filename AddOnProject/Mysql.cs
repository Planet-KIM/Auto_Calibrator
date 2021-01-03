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
        // 폴더 경로 생성
        private static string current_path = Directory.GetCurrentDirectory() + @"\Mysql";
        DirectoryInfo cur_path = new DirectoryInfo(current_path);

        // 파일 경로 생성
        private static string json_file = current_path + @"\mysql_data.json";
        FileInfo jfile = new FileInfo(json_file);
      
        // Database에 넣을 값을 합쳐야 하기 때문에 생성자 생성
        private static string value = string.Empty;
        MySqlConnection conn;

        // Database 생성자를 생성하기 위한 class
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
                try
                {
                    // 파일이 있는지 유무를 파악하기 위한 것. 없으면 예외발생
                    if (!jfile.Exists)
                    {
                        throw new FileNotFoundException(json_file + " : " + "File Not Found!");
                    }
                    // 파일이 존재하면 Json 파일을 읽어와서 초기화 작업
                    else {
                        using (StreamReader file = File.OpenText(current_path + @"\mysql_data.json"))
                        using (JsonTextReader json_reader = new JsonTextReader(file))
                        {
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
                catch(Exception e) { MessageBox.Show(e.Message); }         
            }
        }

        private void button_Mysql_Insert(object sender, EventArgs e)
        {
            // 난수생성 후 문자열로 변경
            Random number = new Random();
            string rnumber = number.Next(10000000, 99999999).ToString();

            // 암호화 생성 (랜던 값 키워드로 주자)
            var encrypt = new Encryption(rnumber);
            string pwd_encrypt = encrypt.result(EncryptType.Encrypt, pwdBox.Text.ToString());

            // INSERT 하기 위한 query문 생성하기
            string query = "INSERT INTO user(name, password) VALUES('" +
                nameBox.Text.ToString() + 
                "', '" +
                pwd_encrypt +
                "')";

            conn.Open();
            MySqlCommand command = new MySqlCommand(query, conn);

            try
            {
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("INSERT SUCCESS!!");
                }
                else
                {
                    throw new Exception("INSERT FAILED !!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally {
                conn.Close();
            }

        }
    }
}
