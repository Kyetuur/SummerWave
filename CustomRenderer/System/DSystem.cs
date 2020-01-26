using System.Drawing;
using System.Windows.Forms;
using SharpDX.Windows;
using SimulationEngine;

namespace SummerWave.Renderer.System
{
    public class DSystem
    {
        private RenderForm RenderForm { get; set; }
        public DSystemConfiguration Configuration { get; private set; }
        public DApplication DApplication { get; set; }
        public DTimer Timer { get; private set; }
        public DSystem() { }
        public static void StartRenderForm(string title, int width, int height,bool fullScreen,Surface surface)
        {
            DSystem system = new DSystem();
            system.Initialize(title, width, height, fullScreen,surface);
            system.RunRenderForm();
        }
        public virtual void Initialize(string title, int width, int height, bool fullScreen,Surface surface)
        {
            if (Configuration == null)
                Configuration = new DSystemConfiguration(title, width, height, fullScreen);

            InitializeWindows(title);
            DApplication = new DApplication();
            DApplication.Initialize(Configuration, RenderForm.Handle, surface);
            Timer = new DTimer();
            Timer.Initialize();
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
            RenderForm.Location = new Point((width / 2) - (Configuration.Width / 2), (height / 2) - (Configuration.Height / 2)); // Wysrodkowanie
        }
        private void RunRenderForm()
        {
            RenderLoop.Run(RenderForm, Frame);
        }
        public void Frame()
        {
            Timer.Frame2();
            DApplication.Frame(Timer.FrameTime);
        }

    }
}