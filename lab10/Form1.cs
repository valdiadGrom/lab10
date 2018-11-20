using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace lab10
{
   
    public partial class Плеер : Form
    {
        WindowsMediaPlayer wmp = new WindowsMediaPlayer();
        double cp = 0;
        List<Track> PlayList = new List<Track>();
        public Плеер()
        {
            InitializeComponent();
            volumeBar1.Value = 10;
            Play.Enabled = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            wmp.settings.volume = volumeBar1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((cp > 0)&& (wmp.URL == PlayList[listBox1.SelectedIndex].Way))
            {
                wmp.controls.play();
                wmp.controls.currentPosition = cp;
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = true;
                wmp.URL = PlayList[listBox1.SelectedIndex].Way;
                wmp.controls.play();
                //listBox1.Items[listBox1.SelectedIndex] += "   " + PlayList[listBox1.SelectedIndex].Duration;
                //PlayList[listBox1.SelectedIndex].Duration = Convert.ToInt32(wmp.currentMedia.duration);
                //listBox1.Items[listBox1.SelectedIndex] = PlayList[listBox1.SelectedIndex].Name +
                //     "   " + PlayList[listBox1.SelectedIndex].Duration;

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            wmp.controls.pause();
            timer1.Enabled = false;
            cp = wmp.controls.currentPosition;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            wmp.controls.stop();
            cp = 0;
        }

        private void volumeBar1_Scroll(object sender, EventArgs e)
        {
            wmp.settings.volume = volumeBar1.Value;

            toolTip1.SetToolTip(volumeBar1, volumeBar1.Value.ToString());
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            wmp.controls.currentPosition = trackBar1.Value;

            toolTip1.SetToolTip(trackBar1, wmp.controls.currentPositionString);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            trackBar1.Maximum = Convert.ToInt32(wmp.currentMedia.duration);
            trackBar1.Value = Convert.ToInt32(wmp.controls.currentPosition);
            int dur =  Convert.ToInt32(wmp.currentMedia.duration);
            int durMin = dur / 60;
            int durSek = dur % 60;

            PlayList[listBox1.SelectedIndex].Duration = durMin.ToString() + "," + durSek.ToString() + " Мин.";

            listBox1.Items[listBox1.SelectedIndex] = PlayList[listBox1.SelectedIndex].Name +
                 "  |  " + PlayList[listBox1.SelectedIndex].Duration;

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if ((openFileDialog1.ShowDialog() == DialogResult.OK))
            {
                listBox1.Items.Add(Path.GetFileName(openFileDialog1.FileName));
                PlayList.Add(new Track() { Way = openFileDialog1.FileName ,
                                           Name = Path.GetFileName(openFileDialog1.FileName)});

            }
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Play.Enabled = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            label3.Text = "Список треков если по русски";
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = "Громкость";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //if (listBox1.Items.IsReadOnly)
            {
                if ((openFileDialog1.ShowDialog() == DialogResult.OK))
                {

                }
            }
        }
    }

    class Track
    {
        public string Name { get; set; }
        public string Way { get; set; }
        public string Duration { get; set; }
        
    }
}

