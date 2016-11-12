using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
namespace Zadanie2Forms
{
    partial class ChartForm
    {
        
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            //System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(12, 12);
            this.chart1.Name = "chart1";
            //series1.ChartArea = "ChartArea1";
            //series1.Legend = "Legend1";
            //series1.Name = "Series1";
            //this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(747, 515);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // ChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 539);
            this.Controls.Add(this.chart1);
            this.Name = "ChartForm";
            this.Text = "ChartForm";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;

        private void AddSeriesForParallelLoop(IDictionary<int, double> values)
        {
            var xValues = values.Keys;
            var yValues = values.Values;
            Series parallelLoopSeries = new Series();
            parallelLoopSeries.ChartType = SeriesChartType.Line;
            parallelLoopSeries.Color = Color.FromArgb(112, 255, 200);
            parallelLoopSeries.BorderColor = Color.FromArgb(164, 164, 164);
            parallelLoopSeries.Name = "Parallel loop CPU usage";
            parallelLoopSeries.XValueMember = "Exercies";
            parallelLoopSeries.YValueMembers = "Miliseconds";
            parallelLoopSeries.Points.DataBindXY(xValues, yValues);
            chart1.Series.Add(parallelLoopSeries);
            chart1.DataBind();
        }

        private void AddSeriesForNormaLoop(IDictionary<int,double> values)
        {
            var xValues = values.Keys;
            var yValues = values.Values;
            Series normalLoopSeries = new Series();
            normalLoopSeries.ChartType = SeriesChartType.Line;
            normalLoopSeries.Color = Color.Green;
            normalLoopSeries.BorderColor = Color.FromArgb(164, 164, 164);
            normalLoopSeries.Name = "Normal loop CPU usage";
            normalLoopSeries.XValueMember = "Exercies";
            normalLoopSeries.YValueMembers = "Miliseconds";
            normalLoopSeries.Points.DataBindXY(xValues, yValues);
            chart1.Series.Add(normalLoopSeries);
            chart1.DataBind();
        }

        private void SetAxisTitle(string ytitle, string xtitle)
        {
            chart1.ChartAreas[0].AxisY.Title = ytitle;
            chart1.ChartAreas[0].AxisX.Title = xtitle;
            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
        }
    }
}