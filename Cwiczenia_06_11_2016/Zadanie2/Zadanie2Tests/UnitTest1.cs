using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Newtonsoft.Json;
using Zadanie2;
using System.Collections.Generic;

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
            foreach(CalkaParametersModel item in list.ParametersList)
            {
                calka.CalculateParallel(item);
                calka.CalculateNormal(item);
            }
            calka.LogToFile();
        }
    }
}
