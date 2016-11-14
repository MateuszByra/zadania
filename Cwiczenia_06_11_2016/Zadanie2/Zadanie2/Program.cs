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
            if (args.Any())
            {
                var readPath = args[1];
                var writePath = args[2];

                FileReader fileReader = new FileReader();
                CalkaWithDataLogger calkaWithLogger = new CalkaWithDataLogger();
                var parametersList = fileReader.GetParametersListFromFile(readPath);
                foreach(var item in parametersList.ParametersList)
                {
                    calkaWithLogger.CalculateParallel(item);
                    calkaWithLogger.CalculateNormal(item);
                }
                calkaWithLogger.LogToFile(writePath);
            }
            else { 
            Calka calka = new Calka();
            calka.InputData();
            Chart.NormalLoopTasksTimes = calka.GetNormalLoopTasksTimes();
            Chart.ParallelLoopTasksTimes = calka.GetParallelLoopTasksTimes();
            Chart.Main();
            }
        }
    }  
}
