using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zadanie2Forms
{
    public static class Chart
    {
        public static IDictionary<int,double> ParallelLoopTasksTimes { get; set; }
        public static IDictionary<int, double> NormalLoopTasksTimes { get; set; }
        /// <summary>1
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ChartForm(ParallelLoopTasksTimes,NormalLoopTasksTimes));
        }
    }
}
