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

namespace AddOnProject
{
    /// <summary>
    /// CFFactorSelector.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CFFactorSelector : System.Windows.Window
    {
        Microsoft.Office.Interop.Excel.Application _thisApplication= ;
        public CFFactorSelector()
        {
            InitializeComponent();
        }

        
        private void Confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            Workbook workbook = _thisApplication.ActiveWorkbook;
            Worksheet worksheet = workbook.Worksheets["InputSheet"];
            Range range = worksheet.Range["A1"];
          
            range.Value = Min_CFFrictionAir.Text;
        }
    }
}
