
using NReco.VideoConverter;
using ScreenRecorder;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisioForge.Core.Types;
using VisioForge.Core.Types.Output;
using VisioForge.Core.Types.VideoCapture;
using VisioForge.Core.VideoCapture;


namespace Screener
{
    public partial class ScreenRecorder : Form
    {

        private VideoCaptureCore capt;
        private bool Fullscreen = false;
        private bool ResolutionsChoiced = false;
        
        public ScreenRecorder()
        {
            InitializeComponent();
           
        }


        private async void btnRecord_Click(object sender, EventArgs e)
        {

            if (!ResolutionsChoiced)
            {
                MessageBox.Show("Du musst zu erst eine Auflösung wählen\n\n" +
                                "Klicke auf Fullscreen oder Select Area.");

            }
            else
            {
                btnRecord.Enabled = false;
                buttonSelectArea.Enabled = false;
                btnFullscreen.Enabled = false;
                if (!Fullscreen)
                {
                    capt.Screen_Capture_Source = new ScreenCaptureSourceSettings()
                    {
                        Mode = ScreenCaptureMode.Screen,
                        FrameRate = 120,
                        GrabMouseCursor = true,
                        FullScreen = false,
                        Top = _y,
                        Bottom = _height,
                        Left = _x,
                        Right = _width,

                    };
                }
                if (Fullscreen)
                {
                    capt.Screen_Capture_Source = new ScreenCaptureSourceSettings()
                    {
                        Mode = ScreenCaptureMode.Screen,
                        FrameRate = 120,
                        GrabMouseCursor = true,
                        FullScreen = true,

                    };
                }


                capt.Audio_PlayAudio = capt.Audio_RecordAudio = false;
                capt.Mode = VideoCaptureMode.ScreenCapture;
                capt.Output_Format = new MP4Output();
            
                capt.Output_Filename = "NewVideo.mp4";

                await capt.StartAsync();
               
            }
           
        }



        private async void btnStop_Click(object sender, EventArgs e)
        {
            btnRecord.Enabled = true;
            buttonSelectArea.Enabled = true;
            btnFullscreen.Enabled = true;
            await capt.StopAsync();
            await Task.Delay(5000);

            string now = DateTime.Now.ToString("[dd.MM.yyyy-HH.mm]");
            string logo = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/logo.png";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/NewVideo" + now + ".mp4";


           
            System.IO.File.Move("NewVideo.mp4", path);
            //FFMpegConverter wrap = new FFMpegConverter();
            //wrap.Invoke("-i " + path + " - i " + logo + " -filter_complex " + path);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            capt = new VideoCaptureCore(videoView1 as IVideoView);
        }

        private void buttonSelectArea_Click(object sender, EventArgs e)
        {
            ResolutionsChoiced = true;
            Fullscreen = false;
            this.Hide();
            SelectArea form1 = new SelectArea();
            form1.InstanceRef = this;
            form1.Show();
        }


        private void btnFullscreen_Click(object sender, EventArgs e)
        {
            ResolutionsChoiced = true;
            Fullscreen = true;
        }
    }
}
