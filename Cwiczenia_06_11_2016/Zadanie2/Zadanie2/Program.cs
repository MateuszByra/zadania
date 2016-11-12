using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadanie2Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Zadanie2
{
    class Program
    {
        static void Main(string[] args)
        {
            Calka calka = new Calka();
            calka.InputData();
            Chart.NormalLoopTasksTimes = calka.GetNormalLoopTasksTimes();
            Chart.ParallelLoopTasksTimes = calka.GetParallelLoopTasksTimes();
            Chart.Main();
        }
    }  
}
