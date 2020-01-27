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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (sur == null)
                button3_Click(null, null);
            SummerWave.Renderer.System.DSystem.StartRenderForm("Surface Rendering", 1920, 1080,false,sur);
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
            sur = new Surface(res);
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
            sur.SimualtionSpeed = 0.3f;
            sur.WaveLen = 1.2f;
            sur.DampenFactor = 0.8f;
            sur.AddSource(new SurfacePoint(60, 60, 600f));
            sur.AddSource(new SurfacePoint(90, 60, 600f));
            setText("surface generated");
            button1.Show();
        }
    }
}
