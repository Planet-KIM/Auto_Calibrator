using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AddOnProject
{
    public partial class MysqlRibbon
    {
        private void MysqlRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void event_Sum(object sender, RibbonControlEventArgs e)
        {
            // C# 에서는 무조건적으로 변수에다가 값을 할당시켜야만 할당을 해서 불러올 수 있습니다.
            double sum = 0;
            Application app = Globals.ThisAddIn.Application;

            foreach (var item in app.Selection.Cells) {
                String text = item.FormulaLocal;
                double value = 0;

                if (double.TryParse(text, out value))
                {
                    sum += value;
                }
            }
            System.Windows.Forms.MessageBox.Show(sum.ToString());
        }

        private void event_Avg(object sender, RibbonControlEventArgs e)
        {
            int count = 0;
            double result = 0;
            double avg = 0;
            Application app = Globals.ThisAddIn.Application;
            
            foreach(var item in app.Selection.Cells)
            {
                String text = item.FormulaLocal;
                double value = 0;

                if (double.TryParse(text, out value))
                {
                    avg += value;
                    count++;
                }
            }
            
            result = avg / count;
            System.Windows.Forms.MessageBox.Show(result.ToString());

        }

        private void event_New(object sender, RibbonControlEventArgs e)
        {
            //Mysql mysql = new Mysql();
            //mysql.Show();
            CFFactorSelector cFFactor = new CFFactorSelector();
            cFFactor.ShowDialog();
        }
    }
}
