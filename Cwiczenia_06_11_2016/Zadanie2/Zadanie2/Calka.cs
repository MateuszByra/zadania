using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2
{
    public class Calka
    {
        private ProgressBar bar;
        protected int a, b, c, x1, x2, n;
        protected double deltaX;
        private Dictionary<int,double> parallelLoopTimes { get; set; }
        private Dictionary<int, double> normalLoopTimes { get; set; }

        public Calka()
        {
            bar = new ProgressBar();
            parallelLoopTimes = new Dictionary<int, double>();
            normalLoopTimes = new Dictionary<int, double>();
        }

        public void InputData()
        {
            var result = 0;
            var time = 1;

            Console.WriteLine("Wprowadz parametry funkcji:");
            Console.Write("a=");
            int.TryParse(Console.ReadLine(), out a);
            Console.Write("b=");
            int.TryParse(Console.ReadLine(), out b);
            Console.Write("c=");
            int.TryParse(Console.ReadLine(), out c);
            Console.Write("x1=");
            int.TryParse(Console.ReadLine(), out x1);
            Console.Write("x2=");
            int.TryParse(Console.ReadLine(), out x2);
            Console.WriteLine("Wprowadź ilość przedziałów");
            Console.Write("n=");
            int.TryParse(Console.ReadLine(), out n);

            bar.CalkowitaIlosc = n;
            deltaX = x1 - x2 / n;

            var parallelLoopResult = CalculateParallel();
            var normalLoopResult = Calculate();

            Console.Clear();
            Console.WriteLine($"Wynik przy użyciu Parallel.For: {parallelLoopResult[result]} \n Czas obliczeń: {parallelLoopResult[time]}");
            Console.WriteLine($"Wynik przy użyciu for: {normalLoopResult[result]} \n Czas obliczeń: {normalLoopResult[time]}");
            Console.WriteLine("Naciśnij dowolny przysik aby wyświetlić wykres.");
            Console.ReadKey();
        }

        protected double Function(double x)
        {
            return a * (x * x) + b * x + c;
        }

        protected virtual double[] Calculate()
        {
            double result = 0;
            Stopwatch normalLoopTimer = new Stopwatch();
            normalLoopTimer.Start();
            int counter = 1;
            Stopwatch oneTaskTime = new Stopwatch();
            for (int i = 1; i < n; i++)
            {
                oneTaskTime.Start();
                result += Function(x1 + i * deltaX);
                bar.WriteBar(counter++);
                oneTaskTime.Stop();
                normalLoopTimes.Add(i, oneTaskTime.ElapsedMilliseconds);
                oneTaskTime.Reset();
            };
            normalLoopTimer.Stop();
            return new[] { result += ((Function(x1) + Function(x2)) / 2) * deltaX, normalLoopTimer.ElapsedMilliseconds };
        }

        protected virtual double[] CalculateParallel()
        {
            object _lock = new object();
            double result = 0;
            Stopwatch parallelLoopTimer = new Stopwatch();
            parallelLoopTimer.Start();
            int counter = 1;
            Stopwatch oneTaskTime = new Stopwatch();
            Parallel.For(1, n, (i) => {
                oneTaskTime.Start();
                var fncResult = Function(x1 + i * deltaX);
                lock (_lock)
                {
                    result += fncResult;
                    bar.WriteBarForParallel(counter++);
                    oneTaskTime.Stop();
                    parallelLoopTimes.Add(i, oneTaskTime.ElapsedMilliseconds);
                    oneTaskTime.Reset();

                }
            });
            parallelLoopTimer.Stop();
            return new[] { result += ((Function(x1) + Function(x2)) / 2) * deltaX, parallelLoopTimer.ElapsedMilliseconds };
        }

        public IDictionary<int,double> GetNormalLoopTasksTimes()
        {
            return normalLoopTimes;
        }

        public IDictionary<int,double> GetParallelLoopTasksTimes()
        {
            var sorted = from pair in parallelLoopTimes
                         orderby pair.Key ascending
                         select pair;
            return sorted.ToDictionary(t=>t.Key, t=>t.Value);
        }
    }
}
