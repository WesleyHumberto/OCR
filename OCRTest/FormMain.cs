using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Imaging.Filters;
using AForge.Imaging;
using System.IO;

namespace OCRTest
{
    public partial class FormMain : Form
    {
        int topoX=0;
        int topoY=0;
        int fimX=0;
        int fimY=0;
        public Bitmap image { get; set; }
      //  public Bitmap image;
        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            var r = ofd.ShowDialog(this);
            if (r == DialogResult.OK)
            {
                String filename = ofd.FileName;
                if (!File.Exists(filename))
                {
                    MessageBox.Show(this, "Error: File not exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //lblFileName.Text = filename;
                image = (Bitmap)Bitmap.FromFile(filename);
                pictureBox1.Image = image;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // var image = (Bitmap) pictureBox1.Image;
            var ocr = new tessnet2.Tesseract();
            ocr.Init(@"tessdata", "eng", false);
            ocr.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ-1234567890");
            //Crop filter = new Crop(new Rectangle(xDown, yDown, xUp-xDown, yUp-yDown));
            //image = filter.Apply(image);
            pictureBox1.Image = image;
            var result = ocr.DoOCR(image, Rectangle.Empty);
            StringBuilder sb = new StringBuilder();
            foreach (tessnet2.Word word in result)
            sb.Append(word.Text + " ");
            textBox1.Text = sb.ToString();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            topoX = e.X;
            topoY = e.Y;
            Console.WriteLine("y = " + e.Y + " | x = " + e.X);

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
          //  var image = (Bitmap)pictureBox1.Image;
            fimX = e.X;
            fimY = e.Y;
            Console.WriteLine("yF = " + e.Y + " | xF = " + e.X);
            AForge.Imaging.Filters.Crop filter = new AForge.Imaging.Filters.Crop(new Rectangle(topoX, topoY, fimX - topoX, fimY - topoY));
            image = filter.Apply(image);
            pictureBox1.Image = this.image;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            // currentImage = new ImageProcess.filters.Gray().process(currentImage);
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            image = new ImageProcess.filters.Gray().process(image);
            pictureBox1.Image = image;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            image = new ImageProcess.filters.Threshold().process(image);
            pictureBox1.Image = image;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            image = new ImageProcess.filters.Median().process(image);
            pictureBox1.Image = image;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            image = new ImageProcess.filters.Mean().process(image);
            pictureBox1.Image = image;
        }
    }
}
