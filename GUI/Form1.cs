using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimulationEngine;
using SummerWave.Renderer;

namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SummerWave.Renderer.System.DSystem.StartRenderForm("Testowa nazwa", 1440, 900,sur);
        }

        public void setText(string str)
        {
            richTextBox1.Text = str;
        }

        Surface sur;

        private void button2_Click(object sender, EventArgs e)
        {
            sur.Recalculate(1);

            string surStr = "";
            foreach (var rows in sur.Grid)
            {
                foreach (var temps in rows)
                {
                    surStr += Math.Round(temps.Height).ToString();
                    surStr += "   ";
                }
                surStr += "\r\n";
            }
            setText(surStr);

        }

        private void button3_Click(object sender, EventArgs e)
        {

            int res = 100;
            sur = new Surface(4, res);
            List<List<float>> newHeights = new List<List<float>>();
            for (int i = 0; i < res; i++)
            {
                List<float> temp = new List<float>();
                for (int j = 0; j < res; j++)
                {

                    temp.Add(0);

                }
                newHeights.Add(temp);
            }


            sur.SummerWaves = true;
            sur.AddSource(new SurfacePoint(50, 50, 0.5));
            sur.AddSource(new SurfacePoint(51, 50, 0.5));

            sur.AddSource(new SurfacePoint(20, 60, 0.8));
            sur.AddSource(new SurfacePoint(10, 70, 30));
            string surStr = "";
            foreach (var rows in sur.Grid)
            {
                foreach (var temps in rows)
                {
                    surStr += Math.Round(temps.Height).ToString();
                    surStr += "   ";
                }
                surStr += "\r\n";
            }
            setText(surStr);
        }
    }
}
