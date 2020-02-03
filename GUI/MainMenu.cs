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
        private Surface _surface;
        public MainMenu()
        {
            
            InitializeComponent();
            this.textBox1.KeyPress += new KeyPressEventHandler(CheckEnterKeyPress);
            this.textBox2.KeyPress += new KeyPressEventHandler(CheckEnterKeyPress);
            this.textBox3.KeyPress += new KeyPressEventHandler(CheckEnterKeyPress);
            HideLabelsAndTextBoxes();
        }

        private void CheckEnterKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                float tmpSpeed;
                float waveLength;
                float factor;

                float.TryParse(textBox1.Text, out tmpSpeed);
                float.TryParse(textBox2.Text, out waveLength);
                float.TryParse(textBox3.Text, out factor);

                _surface.SimualtionSpeed = tmpSpeed;
                _surface.WaveLen = waveLength;
                _surface.DampenFactor = factor;
            }
        }

        private void HideLabelsAndTextBoxes()
        {
            label1.Hide();
            label2.Hide();
            label3.Hide();

            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
        }

        private void ShowLabelsAndTextBoxes()
        {
            label1.Show();
            label2.Show();
            label3.Show();

            textBox1.Show();
            textBox2.Show();
            textBox3.Show();
        }

        private void Init()
        {
            int res = 100;
            _surface = new Surface(res);
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
            _surface.SummerWaves = true;
            _surface.SimualtionSpeed = 0.3f;
            _surface.WaveLen = 1.2f;
            _surface.DampenFactor = 0.8f;
            _surface.AddSource(new SurfacePoint(60, 60, 600f));
            _surface.AddSource(new SurfacePoint(90, 60, 600f));

            
            showSurfaceButton.Hide();
            ShowLabelsAndTextBoxes();
        }

        private void ShowSurfaceButtonEvent(object sender, EventArgs e)
        {
            if (_surface == null)
            {
                Init();
            }

            //this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            //this.Bounds = Screen.PrimaryScreen.Bounds;
            this.TopMost = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.BackColor = Color.Black;
            SummerWave.Renderer.System.DSystem.StartRenderForm("Surface Rendering", 1920, 1080, false, _surface);

        }

        private void GameChanger(object sender, EventArgs e)
        {
            _surface.SimualtionSpeed = 1.4f;
        }

        private new void Enter(object sender, EventArgs e)
        {
            _surface.SimualtionSpeed = 1.4f;
        }

        //moze do testow
        private void button2_Click(object sender, EventArgs e)
        {
            _surface.Recalculate(1);

            string surStr = "";
            foreach (var rows in _surface.Grid)
            {
                foreach (var temps in rows)
                {
                    surStr += Math.Round(temps.Height).ToString();
                    surStr += "   ";
                }
                surStr += "\r\n";
            }
            //setText(surStr);
        }

    }
}
