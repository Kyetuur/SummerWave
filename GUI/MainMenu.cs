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
        private float _speed;
        public MainMenu()
        {
            
            InitializeComponent();
            this.textBox1.KeyPress += new KeyPressEventHandler(CheckEnterKeyPress);
        }

        private void CheckEnterKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                float tmpSpeed;
                float.TryParse(textBox1.Text, out tmpSpeed);
                //var tmpSpeed = float.TryParse(textBox1.Text);
                _surface.SimualtionSpeed = tmpSpeed;
            }
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

            _speed = _surface.SimualtionSpeed;
            showSurfaceButton.Hide();
        }

        private void ShowSurfaceButtonEvent(object sender, EventArgs e)
        {
            if (_surface == null)
            {
                Init();
            }
                
            //this.WindowState = FormWindowState.Normal;
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.Bounds = Screen.PrimaryScreen.Bounds;
            this.TopMost = true;
            this.BackColor = Color.Black;
            SummerWave.Renderer.System.DSystem.StartRenderForm("Surface Rendering", 1920, 1080, false, _surface);
            
        }

        private void GameChanger(object sender, EventArgs e)
        {
            _speed = 1.3f;
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
