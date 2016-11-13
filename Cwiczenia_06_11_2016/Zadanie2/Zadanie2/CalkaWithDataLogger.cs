using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadanie2.Models;

namespace Zadanie2
{
    public class CalkaWithDataLogger : Calka
    {
        private CalkaParametersWriteList ParallelResult { get; set; }
        private CalkaParametersWriteList NormalResult { get; set; }
        public CalkaWithDataLogger()
        {
            ParallelResult = new CalkaParametersWriteList {Type="Parallel" };
            NormalResult = new CalkaParametersWriteList { Type = "Normal" };
        }

        public void CalculateParallel(CalkaParametersReadModel parametersModel)
        {
            a = parametersModel.A;
            b = parametersModel.B;
            c = parametersModel.C;
            x1 = parametersModel.X1;
            x2 = parametersModel.X2;
            n = parametersModel.N;
            deltaX = x1 - x2 / n;
            var result = CalculateParallel();

            ParallelResult.Data.Add(new CalkaParametersWriteModel {A=a,B=b,C=c,X1=x1,X2=x2,N=n,Result=result[0],Miliseconds=result[1] });
        }

        public void CalculateNormal(CalkaParametersReadModel parametersModel)
        {
            a = parametersModel.A;
            b = parametersModel.B;
            c = parametersModel.C;
            x1 = parametersModel.X1;
            x2 = parametersModel.X2;
            n = parametersModel.N;
            deltaX = x1 - x2 / n;
            var result = Calculate();
            NormalResult.Data.Add(new CalkaParametersWriteModel { A = a, B = b, C = c, X1 = x1, X2 = x2, N = n, Result = result[0], Miliseconds = result[1] });
        }

        protected override double[] CalculateParallel()
        {
            object _lock = new object();
            double result = 0;
            Stopwatch parallelLoopTimer = new Stopwatch();
            parallelLoopTimer.Start();
            int counter = 1;
            Parallel.For(1, n, (i) => {
                var fncResult = Function(x1 + i * deltaX);
                lock (_lock)
                {
                    result += fncResult;
                }
            });
            parallelLoopTimer.Stop();
            return new[] { result += ((Function(x1) + Function(x2)) / 2) * deltaX, parallelLoopTimer.ElapsedMilliseconds };
        }

        protected virtual double[] Calculate()
        {
            double result = 0;
            Stopwatch normalLoopTimer = new Stopwatch();
            normalLoopTimer.Start();
            int counter = 1;
            for (int i = 1; i < n; i++)
            {
                result += Function(x1 + i * deltaX);
            };
            normalLoopTimer.Stop();
            return new[] { result += ((Function(x1) + Function(x2)) / 2) * deltaX, normalLoopTimer.ElapsedMilliseconds };
        }

        public void LogToFile(string path)
        { 
            var parallelResult=JsonConvert.SerializeObject(ParallelResult);
            var normalResult =JsonConvert.SerializeObject(NormalResult);
            using(StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(parallelResult+"\n"+normalResult);   
            }
        }
    }
}
