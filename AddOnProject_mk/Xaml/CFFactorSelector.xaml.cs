using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Office.Interop.Excel;
using Renci.SshNet.Messages;
using System.Drawing;

namespace AddOnProject
{
    /// <summary>
    /// CFFactorSelector.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CFFactorSelector : System.Windows.Window
    {
        Microsoft.Office.Interop.Excel.Application _thisApplication = Globals.ThisAddIn.Application;
        public CFFactorSelector()
        {
            InitializeComponent();
        }

        
        private void Confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            Workbook workbook = _thisApplication.ActiveWorkbook;
            Worksheet worksheet = workbook.Worksheets["InputSheet"];
            //Worksheet OutputSheet = workbook.Worksheets["OutputSheet"];
            
            Range range;
            int c = 1, r = 1;
            double step = 0, Temp = 0;

            int FAGetRow=0,FGetRow=0,HTGetRow=0,HTAGetRow=0;
            range = worksheet.Cells[r,c];

           
            while (true)
            {
             
                if (range.Value.ToString().Equals("CF_FrictionAir"))
                {
                    FAGetRow = r;
                }
                if (range.Value.ToString().Equals("CF_Friction"))
                {
                    FGetRow = r;
          
                }
                if (range.Value.ToString().Equals("CF_HeatTransferAir"))
                {
                    HTAGetRow = r;
                   
                }
                if (range.Value.ToString().Equals("CF_HeatTransfer"))
                {
                    HTGetRow = r;
                }
                r = r + 1;
                range = worksheet.Cells[r, c];

                if (range.Value.ToString().Equals("<ParameterListEnd>"))
                    break;
            }


         
            range = worksheet.Cells[FAGetRow, 3];
            range = range.Resize[4, 4];
            range.NumberFormat = "0.00";

            // CFFriction Air
            step = (Convert.ToDouble(Max_CFFrictionAir.Text) - Convert.ToDouble(Min_CFFrictionAir.Text)) / 3.0;
            range = worksheet.Cells[FAGetRow, 3];
            range.Value = Min_CFFrictionAir.Text;
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

            // CFFriction
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

      
            // CF HeatTransfer
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

            // HeatTransferAir
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

            range = worksheet.Range["C2:"+"C"+(FAGetRow-1)];
            Range range1 = worksheet.Range["C2:F"+ (FAGetRow - 1)];
            range.AutoFill(range1, XlAutoFillType.xlFillDefault);

            if((r-1)!=HTGetRow)
            {
                range = worksheet.Range["C" + (HTGetRow + 1) + ":" + "C" + (r - 1)];
                range1 = worksheet.Range["C" + (HTGetRow + 1) + ":" + "F" + (r - 1)];
                range.AutoFill(range1, XlAutoFillType.xlFillDefault);

            }    


            range = worksheet.Range["C"+FAGetRow+":"+"F"+(r-1)];
            range.Interior.Color = System.Drawing.Color.FromArgb(255, 255, 255);
        
            Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
