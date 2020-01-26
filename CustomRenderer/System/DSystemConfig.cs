using System.Windows.Forms;

namespace SummerWave.Renderer.System
{
    public class DSystemConfiguration
    {
        public string Title { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public static bool FullScreen { get; private set; }
        public static float ScreenDepth { get; private set; }
        public static float ScreenNear { get; private set; }
        public static FormBorderStyle BorderStyle { get; private set; }
        public static string ShaderFilePath { get; private set; }


        public DSystemConfiguration(string title, int width, int height, bool fullScreen)
        {
            FullScreen = fullScreen;
            Title = title;
            if (!FullScreen)
            {
                Width = width;
                Height = height;
            }
            else
            {
                Width = Screen.PrimaryScreen.Bounds.Width;
                Height = Screen.PrimaryScreen.Bounds.Height;
            }
        }
        static DSystemConfiguration()
        {
            FullScreen = false;
            ScreenDepth = 1000.0f;   // 1000.0f
            ScreenNear = 0.1f;      // 0.1f
            BorderStyle = FormBorderStyle.None;
            ShaderFilePath = @"..\Shaders\";
        }
    }
}