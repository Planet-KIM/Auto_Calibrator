using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {

            var engine = IronPython.Hosting.Python.CreateEngine();
            var scope = engine.CreateScope();

            try
            {
                var source = engine.CreateScriptSourceFromFile(@"Test.py");
                source.Execute(scope);

                var getPythonFuncResult = scope.GetVariable<Func<string>>("getPythonFunc");
                Console.WriteLine("def 실행 테스트 : " + getPythonFuncResult());

                var sum = scope.GetVariable<Func<int, int, int>>("sum");
                //Console.WriteLine(sum(1, 2));
                int sumvalue =  sum(1, 2);
                Console.WriteLine(sumvalue);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
