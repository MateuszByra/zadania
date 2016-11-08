using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2
{
    class ProgressBar
    {
        public int CalkowitaIlosc { get; set; }
        public bool Parallel { get; set; }
        private float transition =>  (float)(Console.WindowWidth)/CalkowitaIlosc; // przejscie/ zmiana/ przeskok

        public void WriteBar(int i)
        {
            var totalWidth = (float)(Console.WindowWidth) / CalkowitaIlosc;
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 2, Console.WindowHeight / 2 - 2);
            Console.Write($"LOADING{new string('.', (((int)i / 3) % 4))}");
            UpdateProgress(i);
        }

        public void WriteBarForParallel(int i)
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 2);
            Console.Write($"LOADING PARALLEL{new string('.', (((int)i / 3) % 4))}");
            UpdateProgress(i);
        }

        private void UpdateProgress(int i)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(1, Console.WindowHeight / 2);
            Console.Write(new string(':', (int)(i * (transition))) + '>');
            Console.ResetColor();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 2, Console.WindowHeight / 2);
            Console.Write(((((float)i / (CalkowitaIlosc)) * 100).ToString("N2") + "%").PadLeft(8).PadRight(9));
            SetBorder();
            System.Threading.Thread.Sleep(1);
        }
        private void SetBorder()
        {
            Console.BackgroundColor = Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(0, Console.WindowHeight / 2 - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.Write(" ");
            Console.SetCursorPosition(Console.WindowWidth - 1, Console.CursorTop);
            Console.Write(" ");
            Console.Write(new string(' ', Console.WindowWidth));
            Console.ResetColor();

        }
    }
}
