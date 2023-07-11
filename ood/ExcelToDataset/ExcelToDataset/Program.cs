using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Office.Interop.Excel;

namespace Excel
{
    class Program
    {
        static void Main(string[] args)
        {
            var e = new ExcelTable(@"C:\VL\VL.Excel\Eisner.xlsx", 1);
            var extents = e.GetExtents();


        }
    }
}
