using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2
{
    class Program
    {//https://gist.github.com/DanielSWolf/0ab6a96899cc5377bf54

        static ProgressBar bar;
        static ProgressBar parallelBar;
        static int a, b, c, x1, x2, n, counter;
        static double deltaX;
        static void Main(string[] args)
        {
            counter = 1;
            bar = new ProgressBar();
            
            //bar.ShowMyBar();
           // bar.ShowProgressBar(n);
            //Console.ReadKey();
            int result = 0;
            int time = 1;

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
             deltaX = x1- x2 / n;
            var parallelLoopResult = CalculateParallel();
            var normalLoopResult = Calculate();
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
            Console.WriteLine($"Wynik przy użyciu Parallel.For: {parallelLoopResult[result]} \n Czas obliczeń: {parallelLoopResult[time]}");
            Console.WriteLine($"Wynik przy użyciu for: {normalLoopResult[result]} \n Czas obliczeń: {normalLoopResult[time]}");
            Console.ReadKey();

        }

        static double Function(double x)
        {
            return a * (x * x) + b * x + c;
        }

        private static double[] Calculate()
        {
            double result = 0;
            Stopwatch normalLoopTimer = new Stopwatch();
            normalLoopTimer.Start();
            int counter=1;
            for (int i = 1; i < n; i++)
            {
                result += Function(x1 + i * deltaX);
                bar.WriteBar(counter++);
            };
            normalLoopTimer.Stop();
            return new[] { result += ((Function(x1) + Function(x2)) / 2) * deltaX, normalLoopTimer.ElapsedMilliseconds };
        }

        private static double[] CalculateParallel()
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
                    bar.WriteBarForParallel(counter++);
                    
                }
            });
            parallelLoopTimer.Stop();
            return new[] { result += ((Function(x1) + Function(x2)) / 2) * deltaX, parallelLoopTimer.ElapsedMilliseconds };
        }

        private static void Progress()
        {
            Console.Write("=");
        }
    }
}
