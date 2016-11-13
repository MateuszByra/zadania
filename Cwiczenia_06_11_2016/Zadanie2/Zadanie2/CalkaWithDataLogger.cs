using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2
{
    public class CalkaWithDataLogger : Calka
    {
        private Dictionary<CalkaParametersModel, double []> parametersWithParallelResultDict { get; set; }
        private Dictionary<CalkaParametersModel, double[]> parametersWithNormalResultDict { get; set; }
        public CalkaWithDataLogger()
        {
            parametersWithParallelResultDict = new Dictionary<CalkaParametersModel, double[]>();
            parametersWithNormalResultDict = new Dictionary<CalkaParametersModel, double[]>();
        }

        public void CalculateParallel(CalkaParametersModel parametersModel)
        {
            a = parametersModel.A;
            b = parametersModel.B;
            c = parametersModel.C;
            x1 = parametersModel.X1;
            x2 = parametersModel.X2;
            n = parametersModel.N;
            deltaX = x1 - x2 / n;
            parametersWithParallelResultDict.Add(parametersModel, CalculateParallel());
        }

        public void CalculateNormal(CalkaParametersModel parametersModel)
        {
            a = parametersModel.A;
            b = parametersModel.B;
            c = parametersModel.C;
            x1 = parametersModel.X1;
            x2 = parametersModel.X2;
            n = parametersModel.N;
            deltaX = x1 - x2 / n;
            parametersWithNormalResultDict.Add(parametersModel, Calculate());
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

        public void LogToFile()
        {//zrobic zapis parallel? z rozdzieleniem na normal i parallel wyniki

        }
    }
}
