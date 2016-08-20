using LM.Core.Data;
using LM.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
       
        static void Main(string[] args)
        {


            var current = Assembly.LoadFrom(@"Y:\framework\LM.Web\LM.Data\bin\Debug\LM.Data.dll"); 
            var s = current.GetTypes().Where(a => typeof(DbContext).IsAssignableFrom(a)).ToList();
            var dfdf = current.GetTypes().Where(a => a.Namespace == "LM.Data.Model").ToList();
            foreach (var item in s)
            {

            }
            Console.ReadKey();
        }
    }
}
