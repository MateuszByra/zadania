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
        /*private CalkaParametersWriteList ParallelResult { get; set; }
        private CalkaParametersWriteList NormalResult { get; set; }*/
        private CalkaParametersWriteList _result { get; set; }
        public CalkaWithDataLogger()
        {
            /*ParallelResult = new CalkaParametersWriteList();
            NormalResult = new CalkaParametersWriteList();*/
            _result = new CalkaParametersWriteList();
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

            //ParallelResult.Data.Add(new CalkaParametersWriteModel { Type="Parallel",A=a,B=b,C=c,X1=x1,X2=x2,N=n,Result=result[0],Miliseconds=result[1] });
            _result.Data.Add(new CalkaParametersWriteModel { Type = "Parallel", A = a, B = b, C = c, X1 = x1, X2 = x2, N = n, Result = result[0], Miliseconds = result[1] });
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
            _result.Data.Add(new CalkaParametersWriteModel { Type="Normal", A = a, B = b, C = c, X1 = x1, X2 = x2, N = n, Result = result[0], Miliseconds = result[1] });
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
            //ParallelResult.Data.AddRange(NormalResult.Data);
            using(StreamWriter writer = new StreamWriter(path))
            {
                foreach (var item in _result.Data)
                {
                    writer.WriteLine(JsonConvert.SerializeObject(item));
                }
            }
        }

        public void ParallelLogToFile(string path)
        {
            //ParallelResult.Data.AddRange(NormalResult.Data);
            object _lock = new object();
            using(StreamWriter writer = new StreamWriter(path))
            {
                Parallel.ForEach(_result.Data, x =>
                {
                    lock (_lock)
                    {
                        writer.WriteLine(JsonConvert.SerializeObject(x));
                    }
                    
                });
            }
            
        }
    }
}
