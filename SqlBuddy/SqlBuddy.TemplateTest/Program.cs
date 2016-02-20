using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlBuddy.TemplateTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            using(var cmd = RunFront.dbo.Procedures.AllowedDivisionList())
            {
            }
        }
    }
}
