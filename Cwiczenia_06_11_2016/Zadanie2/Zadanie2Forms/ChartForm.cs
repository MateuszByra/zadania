using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zadanie2Forms
{
    public partial class ChartForm : Form
    {
        private Dictionary<int, double> parallelValues = new Dictionary<int, double>() {
            {0,0 },
            {1,0.85},
            {2,0.25},
            {3,0.95},
            {4,0.12}
        };

        private Dictionary<int, double> normaLoopValues = new Dictionary<int, double>() {
            {0,0 },
            { 1,0.22},
            {2,0.48},
            {3,0.583},
            {4,0.521}
        };
        public ChartForm()
        {
            InitializeComponent();
            SetAxisTitle("Miliseconds","Task");
            AddSeriesForParallelLoop(parallelValues);
            AddSeriesForNormaLoop(normaLoopValues);
        }
    }
}
