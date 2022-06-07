using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screener
{
    partial class ScreenRecorder
    {
        public static ScreenRecorder _ScreenRecorder;
        public static int _x, _y, _width, _height;

       

        static int _screenWidth = Screen.PrimaryScreen.Bounds.Width;
        static int _screenHeight = Screen.PrimaryScreen.Bounds.Height;

      

    }
}
