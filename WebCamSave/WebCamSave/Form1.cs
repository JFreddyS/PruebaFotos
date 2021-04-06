using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebCamSave
{
    public partial class Form1 : Form
    {
        private string Path = @"git@github.com:JFreddyS/PruebaFotos.git";
        private bool HayDispositivos;
        private FilterInfoCollection MisDispositivios;
        private VideoCaptureDevice MiWebCam;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargaDispositivos();
        }

        public void CargaDispositivos()
        {
            MisDispositivios = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (MisDispositivios.Count > 0)
            {
                HayDispositivos = true;
                for (int i = 0; i < MisDispositivios.Count; i++)
                    comboBox1.Items.Add(MisDispositivios[i].Name.ToString());
                comboBox1.Text = MisDispositivios[0].Name.ToString();
            }
            else
                HayDispositivos = false;
            
        }

        private void CerrarWebCam()
        {
            if (MiWebCam!=null && MiWebCam.IsRunning)
            {
                MiWebCam.SignalToStop();
                MiWebCam = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CerrarWebCam();
            int i = comboBox1.SelectedIndex;
            string NombreVideo = MisDispositivios[i].MonikerString;
            MiWebCam = new VideoCaptureDevice(NombreVideo);
            MiWebCam.NewFrame += new NewFrameEventHandler(Capturando);
            MiWebCam.Start();
        }

        private void Capturando(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap Imagen = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = Imagen;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CerrarWebCam();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MiWebCam !=null && MiWebCam.IsRunning) {
                pictureBox2.Image = pictureBox1.Image;
                pictureBox2.Image.Save(Path+"FotoPrueba.jpg", ImageFormat.Jpeg);
            }
        }
    }
}
