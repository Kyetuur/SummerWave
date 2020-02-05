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
using SummerWave.Renderer.Graphics.Models;

namespace GUI
{
    public partial class MainMenu : Form
    {
        private Surface _surface;
        public MainMenu()
        {
            
            InitializeComponent();

            //DSurface.red = 0.1f;
            //DSurface.green = 0.1f;
            //DSurface.blue = 0.1f;

            this.Top = 0;
            this.Left = 0;
            this.textBox1.KeyPress += new KeyPressEventHandler(CheckEnterKeyPress);
            this.textBox2.KeyPress += new KeyPressEventHandler(CheckEnterKeyPress);
            this.textBox3.KeyPress += new KeyPressEventHandler(CheckEnterKeyPress);
            this.textBox6.KeyPress += new KeyPressEventHandler(CreateNewPoint);
            HideLabelsAndTextBoxes();
        }

        private void CreateNewPoint(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                int X = 0;
                int Y = 0;
                float Z = 0f;

                int resultX = 0;
                int resultY = 0;
                float resultZ = 0f;

                int.TryParse(textBox4.Text, out X);
                int.TryParse(textBox5.Text, out Y);
                float.TryParse(textBox6.Text, out Z);

                if (X >= 0 && X <= 100)
                {
                    if (Y >= 0 && Y <= 100)
                    {
                        if (Z >= 0 && Z <= 5000)
                        {
                            _surface.AddSource(new SurfacePoint(X, Y, Z));
                        }
                    }
                }
            }

            
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

                e.Handled = true;
                //e.SuppressKeyPress = true;
            }
        }

        private void HideLabelsAndTextBoxes()
        {
            label1.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();

            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            textBox5.Hide();
            textBox6.Hide();
        }

        private void ShowLabelsAndTextBoxes()
        {
            label1.Show();
            label2.Show();
            label3.Show();
            label4.Show();
            label5.Show();
            label6.Show();
            label7.Show();
            label8.Show();
            label9.Show();
            label10.Show();

            textBox1.Show();
            textBox2.Show();
            textBox3.Show();
            textBox4.Show();
            textBox5.Show();
            textBox6.Show();

            trackBar1.Show();
            trackBar2.Show();
            trackBar3.Show();

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
            //_surface.AddSource(new SurfacePoint(60, 60, 600f));
            //_surface.AddSource(new SurfacePoint(90, 60, 600f));
            //_surface.AddSource(new SurfacePoint(10, 60, 300f));


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

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            SummerWave.Renderer.Graphics.Models.DSurface.red = trackBar1.Value;
            //SummerWave.Renderer.Graphics.Models.DSurface.ty = trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            DSurface.green = trackBar2.Value;
        }

        private void BlueScroll(object sender, EventArgs e)
        {
            DSurface.blue = trackBar3.Value;
        }


    }
}
