using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Newtonsoft.Json;
using Zadanie2;
using System.Collections.Generic;
using Zadanie2.Models;
using System.Diagnostics;

namespace Zadanie2Tests
{
    [TestClass]
    public class UnitTest1
    {
        CalkaParametersList list { get; set; }
        [TestMethod]
        public void ReadTextFileTest()
        {
            var path = @"C:\Users\Maly\Desktop\parametry.json";
            using (StreamReader reader = new StreamReader(path))
            {
                var data = reader.ReadToEnd();
                list = JsonConvert.DeserializeObject<CalkaParametersList>(data);
            }

            Calculate();
        }

        private void Calculate()
        {
            CalkaWithDataLogger calka = new CalkaWithDataLogger();
            foreach(CalkaParametersReadModel item in list.ParametersList)
            {
                calka.CalculateParallel(item);
                calka.CalculateNormal(item);
            }
            var path = @"C:\Users\Maly\Desktop\logs.json";
            Stopwatch stwNormal = new Stopwatch();
            Stopwatch stwParallel = new Stopwatch();
            stwNormal.Start();   
            calka.LogToFile(path);
            stwNormal.Stop();
            stwParallel.Start();
            calka.ParallelLogToFile(@"C:\Users\Maly\Desktop\parallelLogs.json");
            stwParallel.Stop();
        }
    }
}
