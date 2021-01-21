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
using Microsoft.Office.Interop.Excel;

namespace AddOnProject
{
    public partial class Mysql : Form
    {
        // thisApplication 
        Microsoft.Office.Interop.Excel.Application _thisApplication = Globals.ThisAddIn.Application;


        // 폴더 경로 생성
        private static string current_path = Directory.GetCurrentDirectory() + @"\Mysql";
        DirectoryInfo cur_path = new DirectoryInfo(current_path);

        // 파일 경로 생성
        private static string json_file = current_path + @"\mysql_data.json";
        FileInfo jfile = new FileInfo(json_file);
      
        // Database에 넣을 값을 합쳐야 하기 때문에 생성자 생성
        private static string value = string.Empty;
        MySqlConnection conn;
        MySqlDataReader read;

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


        private void button_Fmu_Insert(object sender, EventArgs e)
        {
            if(FmuBox.Text.ToString() == "")
            {
                return;
            }
            else if(FmuBox.Text.ToString() == " ")
            {
                return;
            }

            string query = "INSERT INTO fmu(fmuname) VALUES('" +
             FmuBox.Text.ToString() +
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
            finally
            {
                FmuBox.Clear();
                conn.Close();
            }

        }

        private void button_File_Select(object sender, EventArgs e)
        {
            FmuBox.Clear();
            String file_path = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.Filter = "fmu files (*.fmu)|*.fmu|All files (*.*)|*.*";

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                file_path = openFileDialog.FileName;
                FmuBox.Text = file_path.Split('\\')[file_path.Split('\\').Length - 1];
            }
        }

        private void button_Fmu_Select(object sender, EventArgs e)
        {
            try
            {
                Workbook workbook = _thisApplication.ActiveWorkbook;
                Worksheet worksheet = workbook.Worksheets["FMU"];
                
                Range range = worksheet.Cells[8, 2];
                String filename = range.Value.ToString().Split('\\')[range.Value.ToString().Split('\\').Length - 1];

                if (FmuBox.Text.ToString().Contains(filename))
                {
                    MessageBox.Show(filename);
                }
                else { MessageBox.Show("FmuBox is Null : Select FMU file"); return; }

                string query = "SELECT fmuname, q, ref_dp, air_dp FROM test_data, fmu WHERE test_data.fmuid=fmu.id and fmuname='" + filename + "'";

                conn.Open();
                MySqlCommand command = new MySqlCommand(query, conn);
                read = command.ExecuteReader();
               
               
                /*
                // 이 구문은 response 받으려는 결과가 여러 값일 때만 이렇게 하면 됩니다.
                string result = "";
                while (read.Read())
                {
                    result = read["fmuname"].ToString() + "\n Q : " + read["q"].ToString() + "\n ref_dprla592000r : "
                           + read["ref_dp"].ToString() + "\n air_dp : " + read["air_dp"].ToString();
                }
                */

                // 한번만 호출시 이처럼 하면 됩니다. 
                read.Read();
                MessageBox.Show("\n Q : " + read["q"].ToString() + "\n ref_dp : "
                           + read["ref_dp"].ToString() + "\n air_dp : " + read["air_dp"].ToString());

                string[] result_name = { "Name(Test)", "Q", "Ref_dp", "Air_dp" };
                int cell_count = 16;
                foreach(string name in result_name)
                {
                    range = worksheet.Cells[cell_count, 1];
                    range.Value = name;
                    range.Font.Bold = true;
                    range.Font.Size = 13;
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Interior.Color = Color.LightSkyBlue;
                    cell_count++;
                }

                range = worksheet.Cells[16, 2];
                range.Value = "Value";
                range.Font.Bold = true;
                range.Font.Size = 13;
                range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                range.Interior.Color = Color.LightSkyBlue;

                string[] result_value = { read["q"].ToString(), read["ref_dp"].ToString(), read["air_dp"].ToString() };
                int cell_count2 = 17;
                foreach(string value in result_value)
                {
                    range = worksheet.Cells[cell_count2, 2];
                    range.Value = value;
                    range.Font.Size = 12;
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Interior.Color = Color.Gray;
                    cell_count2++;
                }

                read.Close();
                worksheet.Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
                this.Close();
            }
        }

        /*
        public void buttonOverall2(Office.IRibbonControl control)
        {
            // Sheet 값 초기화 하기 
            Workbook workbook = thisApplication.ActiveWorkbook;
            Worksheet outputsheet = workbook.Worksheets["OutputSheet"];
            Worksheet fmusheet = workbook.Worksheets["FMU"];
            Worksheet inputsheet = workbook.Worksheets["InputSheet"];

            try
            {
                // 초기로 삭제하기 (삭제되는 것 마련)
                Range delete_output = outputsheet.Range["C1:F1"];
                delete_output.EntireColumn.Delete();

                int i;
                for (i = 0; i < 1; i++)
                {
                    this.buttonOverall_Click(control);
                }

                int[] cells = { 11, 16, 6, 18 };

                // DB Test_data Cell
                Range fmu_range = fmusheet.Cells[19, 2];

                //비례상수 평균
                double alpha_sum = 0;

                for (int count = 3; count < 7; count++)
                {
                    // 백분율 계산 air_dp(result)
                    Range output_range2 = outputsheet.Cells[cells[0], count]; //[11,3] [11,4] [11, 5] [11, 6]

                    // 백분율 계산 후 여기에 출력
                    Range output_range1 = outputsheet.Cells[cells[1], count]; //[16,3] [16  ,4] [16, 5] [16, 6]
                    output_range1.Value = (double.Parse(fmu_range.Value2.ToString()) / double.Parse(output_range2.Value2.ToString())) * 100;
                    output_range1.Font.Size = 13;
                    output_range1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    output_range1.Interior.Color = Color.LightYellow;


                    if (100 < output_range1.Value)
                    {
                        double namugi = double.Parse(output_range1.Value2.ToString()) - 100;
                        output_range1.Value = 100 - namugi;
                    }

                    // Friction_air의 비례상수 구하기
                    Range input_range1 = inputsheet.Cells[cells[2], count]; //[6,3] [6,4] [6,5] [6,6]

                    // 비례 상수 출력하기
                    Range output_range3 = outputsheet.Cells[cells[3], count]; // [18,3] [18,4] [18,5] [18,6]
                    output_range3.Value = double.Parse(output_range2.Value2.ToString()) / double.Parse(input_range1.Value2.ToString());
                    output_range3.Font.Size = 13;
                    output_range3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    output_range3.Interior.Color = Color.LightYellow;

                    alpha_sum += output_range3.Value2;
                }

                // alpha 값에 대한 평균
                double alpha_avg = alpha_sum / 4;

                // Design - 백분율
                //Range outputR = outputsheet.Cells[15, 3];
                Range outputR = outputsheet.Range["C15:F15"];
                outputR.Merge(true);
                outputR.Value = "백분율(%) - air_dp";
                outputR.Font.Bold = true;
                outputR.Font.Size = 14;
                outputR.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                outputR.Interior.Color = Color.LightGray;

                // Design - 비례상수
                //Range outputA = outputsheet.Cells[17, 3];
                Range outputA = outputsheet.Range["C17:F17"];
                outputA.Merge(true);
                outputA.Value = "비례 상수(Alpha) - air_dp";
                outputA.Font.Bold = true;
                outputA.Font.Size = 14;
                outputA.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                outputA.Interior.Color = Color.LightGray;

                MessageBox.Show("\t 추천 값 \n" +
                    (double.Parse(fmu_range.Value2.ToString()) / alpha_avg).ToString());

                for (int count = 3; count < 7; count++)
                {
                    Range input_range2 = inputsheet.Cells[6, count]; //[6,3] [6,4] [6,5] [6,6]
                    input_range2.Value = double.Parse(fmu_range.Value2.ToString()) / alpha_avg;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            // 마지막 백분율에 따른 재실행
            Range total_output = outputsheet.Cells[16, 6];
            if (98 < double.Parse(total_output.Value2.ToString()))
            {
                this.buttonOverall3(control, 0.35, 0.25);
            }
            else if (98 > double.Parse(total_output.Value2.ToString()))
            {
                this.buttonOverall2(control);
            }

        }*/

        /*
        // ref_air 값을 찾아주는 method입니다.
        // 예외 처리가 필요합니다. 셀이 비었을 경우나....
        public void buttonOverall3(Office.IRibbonControl control, double friction, double friction_control)
        {
            // Sheet 값 초기화 하기 
            Workbook workbook = thisApplication.ActiveWorkbook;
            Worksheet outputsheet = workbook.Worksheets["OutputSheet"];
            Worksheet fmusheet = workbook.Worksheets["FMU"];
            Worksheet inputsheet = workbook.Worksheets["InputSheet"];

            // 초기로 삭제하기
            Range delete_output = outputsheet.Range["C1:F1"];
            delete_output.EntireColumn.Delete();

            // simaution 하기 전에 friction 값을 변동하기 
            Range original = inputsheet.Cells[8, 3];
            original.Value = 0.1;

            for (int num = 4; num < 7; num++)
            {
                Range friction_value = inputsheet.Cells[8, num];
                friction_value.Value = friction;
                friction += friction_control;
            }

            try
            {
                // 수행할 overall 횟수
                this.buttonOverall_Click(control);

                // 참조할 셀 영역을 배열로 처리
                int[] cells = { 8, 24, 10, 22 };

                // DB Test_data Cell - ref_dp
                Range fmu_range = fmusheet.Cells[18, 2];
                // Ref_dp 0.1 result in cell
                Range output_original = outputsheet.Cells[cells[2], 3];

                // 참조영역 for문 하기
                for (int count = 3; count < 7; count++)
                {
                    // Friction의 비례상수 구하기
                    Range input_friction = inputsheet.Cells[cells[0], count]; //[8,3] [8,4] [8,5] [8,6]
                    Range output_alpha = outputsheet.Cells[cells[1], count]; // [24,3] [24,4] [24,5] [24,6]
                    Range output_result = outputsheet.Cells[cells[2], count]; // [10,3] [10,4] [10,5] [10,6]
                    Range output_rule = outputsheet.Cells[cells[3], count]; // [22, 3] [22, 4] [22, 5] [22,6 ]

                    // 백분율 - ref_dp
                    double rule_value = (double.Parse(output_result.Value2.ToString()) / double.Parse(fmu_range.Value2.ToString())) * 100;
                    output_rule.Value = rule_value;
                    output_rule.Font.Size = 13;
                    output_rule.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    output_rule.Interior.Color = Color.LightYellow;

                    if (100 < rule_value)
                    {
                        double namugi = rule_value - 100;
                        output_rule.Value = 100 - namugi;
                    }

                    // 비례상수 출력하기
                    output_alpha.Value = double.Parse(output_result.Value2.ToString()) * (1 / (double.Parse(output_original.Value2.ToString()) * (double.Parse(input_friction.Value2.ToString()) * 10)));
                    output_alpha.Font.Size = 13;
                    output_alpha.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    output_alpha.Interior.Color = Color.LightYellow;
                }

                // Design - 백분율
                Range outputR = outputsheet.Range["C21:F21"];
                outputR.Merge(true);
                outputR.Value = "백분율(%) - ref_dp";
                outputR.Font.Bold = true;
                outputR.Font.Size = 14;
                outputR.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                outputR.Interior.Color = Color.LightGray;

                // Design - 비례상수
                Range outputA = outputsheet.Range["C23:F23"];
                outputA.Merge(true);
                outputA.Value = "비례 상수(Alpha) - ref_dp";
                outputA.Font.Bold = true;
                outputA.Font.Size = 14;
                outputA.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                outputA.Interior.Color = Color.LightGray;

                string[] cellChar = { "D8", "E8", "F8" };

                int cellChar_count = 0;
                for (int cell = 4; cell <= 5; cell++)
                {
                    Range control_outputA = outputsheet.Cells[22, cell];
                    Range control_outputB = outputsheet.Cells[22, cell + 1];

                    if (double.Parse(control_outputA.Value2.ToString()) < double.Parse(control_outputB.Value2.ToString()))
                    {
                        cellChar_count++;
                        Range input_frictionA = inputsheet.Range[cellChar[cellChar_count]];
                        //MessageBox.Show(input_frictionA.Value2.ToString());
                        Range input_frictionB = inputsheet.Range[cellChar[cellChar_count - 1]];
                        input_frictionB.Value = double.Parse(input_frictionA.Value2.ToString());
                    }
                    else
                    {
                        cellChar_count++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/


    }

}
