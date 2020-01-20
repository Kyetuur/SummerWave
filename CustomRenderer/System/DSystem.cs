using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using SharpDX.Windows;
using SimulationEngine;
using SummerWave.Renderer.Graphics.Input;

namespace SummerWave.Renderer.System
{
    public class DSystem
    {
        private RenderForm RenderForm { get; set; }
        public DSystemConfiguration Configuration { get; private set; }
        public DApplication DApplication { get; set; }
        public DTimer Timer { get; private set; }

        public DSystem() { }

        public static void StartRenderForm(string title, int width, int height,Surface surface)
        {
            DSystem system = new DSystem();
            system.Initialize(title, width, height, false, false, 0,surface);
            system.RunRenderForm();
        }

        public virtual bool Initialize(string title, int width, int height, bool vSync, bool fullScreen, int testTimeSeconds,Surface surface)
        {
            bool result = false;

            if (Configuration == null)
                Configuration = new DSystemConfiguration(title, width, height, fullScreen, vSync);

            InitializeWindows(title);

            DApplication = new DApplication();

            if (!DApplication.Initialize(Configuration, RenderForm.Handle,surface))
                return false;


            Timer = new DTimer();
            if (!Timer.Initialize())
            {
                MessageBox.Show("Could not initialize Timer object", "Error", MessageBoxButtons.OK);
                return false;
            }

            return result;
        }
        private void InitializeWindows(string title)
        {
            int width = Screen.PrimaryScreen.Bounds.Width;
            int height = Screen.PrimaryScreen.Bounds.Height;

            RenderForm = new RenderForm(title)
            {
                ClientSize = new Size(Configuration.Width, Configuration.Height),
                FormBorderStyle = DSystemConfiguration.BorderStyle
            };

            RenderForm.Show();
            RenderForm.Location = new Point((width / 2) - (Configuration.Width / 2), (height / 2) - (Configuration.Height / 2));
        }
        private void RunRenderForm()
        {
            RenderLoop.Run(RenderForm, () =>
            {
                if (!Frame())
                    ShutDown();
            });
        }
        public bool Frame()
        {

            Timer.Frame2();
            //Thread.Sleep(1500);
            if (!DApplication.Frame(Timer.FrameTime))
                return false;

            return true;
        }
        public void ShutDown()
        {
            ShutdownWindows();
            Timer = null;

            DApplication?.Shutdown();
            DApplication = null;
            Configuration = null;
        }
        private void ShutdownWindows()
        {
            RenderForm?.Dispose();
            RenderForm = null;
        }
    }
}