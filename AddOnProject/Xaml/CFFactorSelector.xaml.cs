using Microsoft.Office.Interop.Excel;
using System;
using System.Windows;
using System.Runtime.InteropServices;

namespace AddOnProject.Xaml
{
    /// <summary>
    /// CFFactorSelector.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CFFactorSelector : System.Windows.Window
    {
        public CFFactorSelector()
        {
            InitializeComponent();
        }
        Microsoft.Office.Interop.Excel.Application _thisApplication = Globals.ThisAddIn.Application;
        private void Confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            Workbook workbook = null;
            Worksheet worksheet = null;
            Sheets sheets = null;
            Range range = null;
            try
            {
                workbook = _thisApplication.ActiveWorkbook;
                sheets = workbook.Worksheets;

                for (int i = 1; i <= sheets.Count; i++)
                {
                    if (sheets[i].Name == "InputSheet")
                    {
                        worksheet = workbook.Worksheets["InputSheet"];
                    }
                }   
                
                if(worksheet==null)
                {
                    MessageBox.Show("InputSheet가 없습니다");
                    Close();
                    return;
                }

                int c = 1, r = 1; //반복문을 위해 선언해주는 coulmn과 row 입니다.
                double step = 0, Temp = 0;
                int FAGetRow = 0, FGetRow = 0, HTGetRow = 0, HTAGetRow = 0;
                //FAGetRow는 CF_FrictionAir의 행번호입니다.
                //FGetRow는 CF_Friction의 행번호입니다.
                //HTGetRow는 CF_HeatTrasnfer의 행번호입니다.
                //HTAGetRow는 CF_HeatTransferAir의 행번호입니다.

                range = worksheet.Cells[r, c];


                while (true)
                {

                    //range의 값이 "CF_FrictionAir"와 같다면 FAGetRow에 현재 row의 값을 저장합니다.
                    if (range.Value.ToString().Equals("CF_FrictionAir"))
                    {
                        FAGetRow = r;
                    }
                    //range의 값이 "CF_Friction"와 같다면 FGetRow 에 현재 row의 값을 저장합니다.
                    if (range.Value.ToString().Equals("CF_Friction"))
                    {
                        FGetRow = r;
                    }
                    //range의 값이 "CF_HeatTransferAir"와 같다면 HTAGetRow에 현재 row의 값을 저장합니다.
                    if (range.Value.ToString().Equals("CF_HeatTransferAir"))
                    {
                        HTAGetRow = r;
                    }
                    //range의 값이 "CF_HeatTransfer"와 같다면 HTGetRow에 현재 row의 값을 저장합니다.
                    if (range.Value.ToString().Equals("CF_HeatTransfer"))
                    {
                        HTGetRow = r;
                    }

                    //다음 행의 탐색을 위해 r값을 증가시킵니다.
                    r = r + 1;
                    //range에 다음행 Cell을 넣습니다.
                    range = worksheet.Cells[r, c];

                    //range의 값이 "<ParameterListEnd>"와 같으면 반복문을 중단합니다.
                    if (range.Value.ToString().Equals("<ParameterListEnd>"))
                        break;
                }


                if (FAGetRow != 0)
                {
                    // CF팩터의 값을 소수 둘째자리만 넣어주기 위한 코드입니다.
                    range = worksheet.Cells[FAGetRow, 3];
                    range = range.Resize[4, 4];
                    range.NumberFormat = "0.00";

                    // CFFriction Air의 값을 넣습니다.
                    step = (Convert.ToDouble(Max_CFFrictionAir.Text) - Convert.ToDouble(Min_CFFrictionAir.Text)) / 3.0;
                    range = worksheet.Cells[FAGetRow, 3];
                    range.Value = Min_CFFrictionAir.Text;
                    Temp = Convert.ToDouble(range.Value.ToString());

                    range = range.Offset[0, 1]; //현재 range의 다음 Column값입니다.
                    range.Value = Temp + step;
                    Temp = Convert.ToDouble(range.Value.ToString());

                    range = range.Offset[0, 1];
                    range.Value = Temp + step;
                    Temp = Convert.ToDouble(range.Value.ToString());

                    range = range.Offset[0, 1];
                    range.Value = Temp + step;
                    Temp = Convert.ToDouble(range.Value.ToString());

                }
                if (FGetRow != 0)
                {
                    // CFFriction의 값을 넣습니다.
                    step = (Convert.ToDouble(Max_CFFriction.Text) - Convert.ToDouble(Min_CFFriction.Text)) / 3.0;
                    range = worksheet.Cells[FGetRow, 3];
                    range.Value = Min_CFFriction.Text;
                    Temp = Convert.ToDouble(range.Value.ToString());

                    range = range.Offset[0, 1];
                    range.Value = Temp + step;
                    Temp = Convert.ToDouble(range.Value.ToString());

                    range = range.Offset[0, 1];
                    range.Value = Temp + step;
                    Temp = Convert.ToDouble(range.Value.ToString());

                    range = range.Offset[0, 1];
                    range.Value = Temp + step;
                    Temp = Convert.ToDouble(range.Value.ToString());

                }

                if (HTGetRow != 0)
                {
                    // CF HeatTransfer의 값을 넣습니다.
                    step = (Convert.ToDouble(Max_CFHeatTransfer.Text) - Convert.ToDouble(Min_CFHeatTransfer.Text)) / 3.0;
                    range = worksheet.Cells[HTGetRow, 3];
                    range.Value = Min_CFHeatTransfer.Text;
                    Temp = Convert.ToDouble(range.Value.ToString());

                    range = range.Offset[0, 1];
                    range.Value = Temp + step;
                    Temp = Convert.ToDouble(range.Value.ToString());

                    range = range.Offset[0, 1];
                    range.Value = Temp + step;
                    Temp = Convert.ToDouble(range.Value.ToString());

                    range = range.Offset[0, 1];
                    range.Value = Temp + step;
                    Temp = Convert.ToDouble(range.Value.ToString());

                }
                if (HTAGetRow != 0)
                {
                    // HeatTransferAir의 값을 넣습니다.
                    step = (Convert.ToDouble(Max_CFHeatTransferAir.Text) - Convert.ToDouble(Min_CFHeatTransferAir.Text)) / 3.0;
                    range = worksheet.Cells[HTAGetRow, 3];
                    range.Value = Min_CFHeatTransferAir.Text;
                    Temp = Convert.ToDouble(range.Value.ToString());

                    range = range.Offset[0, 1];
                    range.Value = Temp + step;
                    Temp = Convert.ToDouble(range.Value.ToString());

                    range = range.Offset[0, 1];
                    range.Value = Temp + step;
                    Temp = Convert.ToDouble(range.Value.ToString());

                    range = range.Offset[0, 1];
                    range.Value = Temp + step;
                    Temp = Convert.ToDouble(range.Value.ToString());
                }
                //CF팩터값을 제외하고 나머지 값들을 자동적으로 채워주는 코드입니다. AutoFill함수를 사용합니다.
                range = worksheet.Range["C2:" + "C" + (FAGetRow - 1)];
                Range range1 = worksheet.Range["C2:F" + (FAGetRow - 1)];
                range.AutoFill(range1, XlAutoFillType.xlFillDefault);


                //현재 r에는 <ParameterListEnd>의 행번호가 저장되어 있습니다.
                // <ParameterListEnd> 마지막행 - 1 이 HTGetRow가 아니라면...
                if ((r - 1) != HTGetRow)
                {
                    //값을 채워줍니다.
                    range = worksheet.Range["C" + (HTGetRow + 1) + ":" + "C" + (r - 1)];
                    range1 = worksheet.Range["C" + (HTGetRow + 1) + ":" + "F" + (r - 1)];
                    range.AutoFill(range1, XlAutoFillType.xlFillDefault);
                }

                //CF팩터 값부터 마지막 값까지 색깔을 채워줍니다.
                range = worksheet.Range["C" + FAGetRow + ":" + "F" + (r - 1)];
                range.Interior.Color = System.Drawing.Color.FromArgb(255, 255, 255);

                Close();
            }
            catch(System.Exception)
            {
                MessageBox.Show("에러가 발생");
            }
            finally
            {
                if (range != null) Marshal.ReleaseComObject(range);
                if (worksheet != null) Marshal.ReleaseComObject(worksheet);
                if (sheets != null) Marshal.ReleaseComObject(sheets);
                if (workbook != null) Marshal.ReleaseComObject(workbook);
                Close();
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
