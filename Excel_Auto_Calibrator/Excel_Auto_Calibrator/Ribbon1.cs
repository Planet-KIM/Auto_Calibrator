using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Office = Microsoft.Office.Core;

// TODO:  리본(XML) 항목을 설정하려면 다음 단계를 수행하십시오.

// 1. 다음 코드 블록을 ThisAddin, ThisWorkbook 또는 ThisDocument 클래스에 복사합니다.

//  protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
//  {
//      return new Ribbon1();
//  }

// 2. 단추 클릭 등의 사용자 작업을 처리하려면 이 클래스의 "리본 콜백" 영역에서 콜백
//    메서드를 만듭니다. 참고: 리본 디자이너에서 이 리본을 내보낸 경우 이벤트 처리기의 코드를
//    콜백 메서드로 이동하고 리본 확장성(RibbonX) 프로그래밍 모델에서 사용할 수 있도록
//    코드를 수정해야 합니다.

// 3. 리본 XML 파일의 컨트롤 태그에 특성을 할당하여 사용자 코드의 적절한 콜백 메서드를 식별합니다.  

// 자세한 내용은 Visual Studio Tools for Office 도움말에서 리본 XML 설명서를 참조하십시오.


namespace Excel_Auto_Calibrator
{
    [ComVisible(true)]
    public class Ribbon1 : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;

        public Ribbon1()
        {
        }

        #region IRibbonExtensibility 멤버

        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("Excel_Auto_Calibrator.Ribbon1.xml");
        }

        #endregion

        #region 리본 콜백
        //여기서 콜백 메서드를 만듭니다. 콜백 메서드를 추가하는 방법에 대한 자세한 내용은 https://go.microsoft.com/fwlink/?LinkID=271226을 참조하세요.

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        #endregion

        #region 도우미

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
