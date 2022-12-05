using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using openCV; //ddlls files and ref
using System.Drawing.Imaging;
using System.Threading;

namespace Final_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //intialize variables 
        //(IplImage,Bitmap) -> data types 
        IplImage imageSource1, imageSource2, imageSource_resized1, imageSource_resized2;


        Bitmap bmp, bmp2;
        IplImage img, img3;
      


        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }


        //------------------------------- Open Image------------------------------------------------------------ 

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            openFileDialog1.FileName = " "; //brwose page which contains file and set to " "
            openFileDialog1.Filter = "JPEG|*JPG|Bitmap|*.bmp|All|"; // filter data with exetintions 

            if (openFileDialog1.ShowDialog() == DialogResult.OK)//select photo and press ok 
            {
                try// try this fild if found (exiption) error show message
                {
                    //to get the source photo and resized in picture pox 
                    imageSource1 = cvlib.CvLoadImage(openFileDialog1.FileName, cvlib.CV_LOAD_IMAGE_COLOR);
                    CvSize size = new CvSize(pictureBox1.Width, pictureBox1.Height);
                      imageSource_resized1 = cvlib.CvCreateImage(size, imageSource1.depth, imageSource1.nChannels);
                    //depth is the data in the photo and channels is the pixels  
                    cvlib.CvResize(ref imageSource1, ref imageSource_resized1, cvlib.CV_INTER_LINEAR);
                    pictureBox1.BackgroundImage = (Image)imageSource_resized1;

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }
        //------------------------------- convert photo to gray --------------------------------------------------------- 
        private void convertToGrayLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //photo is a matrix ,
            bmp = (Bitmap)imageSource_resized1;//datatype
            int width = bmp.Width;
            int height = bmp.Height;
            Color p;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    p = bmp.GetPixel(x, y);
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;
                    int avg = (r + g + b) / 3;
                    bmp.SetPixel(x, y, Color.FromArgb(a, avg, avg, avg));
                    pictureBox4.BackgroundImage = (Image)bmp;



                }

            }



        }
        //------------------------------- convert photo to red colored ---------------------------------------------------------
        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {

            img = cvlib.CvCreateImage(new CvSize(imageSource1.width, imageSource1.height), imageSource1.depth, imageSource1.nChannels);
            int srcAdd = imageSource1.imageData.ToInt32();
            int dstAdd = img.imageData.ToInt32();

            unsafe // to use pointer in c#
            {
                int srcIndex, dstIndex;
                for (int r = 0; r < img.height; r++)
                    for (int c = 0; c < img.width; c++)
                    {
                        srcIndex = dstIndex = (img.width * r * img.nChannels) + (c * img.nChannels); // to go from pixel to another 
                        //there 3 layers in pixel select color we ant and set other to 0
                        *(byte*)(dstAdd + dstIndex + 0) = 0;
                        *(byte*)(dstAdd + dstIndex + 1) = 0;
                        *(byte*)(dstAdd + dstIndex + 2) = *(byte*)(srcAdd + srcIndex + 2);
                    }

            }


            CvSize size = new CvSize(pictureBox3.Width, pictureBox3.Height);
            IplImage resized_image = cvlib.CvCreateImage(size, img.depth, img.nChannels);
            cvlib.CvResize(ref img, ref resized_image, cvlib.CV_INTER_LINEAR);
            pictureBox3.BackgroundImage = (Image)resized_image;
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img = cvlib.CvCreateImage(new CvSize(imageSource1.width, imageSource1.height), imageSource1.depth, imageSource1.nChannels);
            int srcAdd = imageSource1.imageData.ToInt32();
            int dstAdd = img.imageData.ToInt32();
            unsafe
            {
                int srcIndex, dstIndex;
                for (int r = 0; r < img.height; r++)
                    for (int c = 0; c < img.width; c++)
                    {
                        srcIndex = dstIndex = (img.width * r * img.nChannels) + (c * img.nChannels);
                        *(byte*)(dstAdd + dstIndex + 0) = 0;
                        *(byte*)(dstAdd + dstIndex + 1) = *(byte*)(srcAdd + srcIndex + 1);
                        *(byte*)(dstAdd + dstIndex + 2) = 0;
                    }
            }


            CvSize size = new CvSize(pictureBox3.Width, pictureBox3.Height);
            IplImage resized_image = cvlib.CvCreateImage(size, img.depth, img.nChannels);
            cvlib.CvResize(ref img, ref resized_image, cvlib.CV_INTER_LINEAR);
            pictureBox3.BackgroundImage = (Image)resized_image;


        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img = cvlib.CvCreateImage(new CvSize(imageSource1.width, imageSource1.height), imageSource1.depth, imageSource1.nChannels);
            int srcAdd = imageSource1.imageData.ToInt32();
            int dstAdd = img.imageData.ToInt32();

            unsafe
            {

                int srcIndex, dstIndex;
                for (int r = 0; r < img.height; r++)
                    for (int c = 0; c < img.width; c++)
                    {
                        srcIndex = dstIndex = (img.width * r * img.nChannels) + (c * img.nChannels);
                        *(byte*)(dstAdd + dstIndex + 0) = *(byte*)(srcAdd + srcIndex + 0);
                        *(byte*)(dstAdd + dstIndex + 1) = 0;
                        *(byte*)(dstAdd + dstIndex + 2) = 0;

                    }
            }

            CvSize size = new CvSize(pictureBox3.Width, pictureBox3.Height);
            IplImage resized_image = cvlib.CvCreateImage(size, img.depth, img.nChannels);
            cvlib.CvResize(ref img, ref resized_image, cvlib.CV_INTER_LINEAR);
            pictureBox3.BackgroundImage = (Image)resized_image;
        }
        //----------------------------------------open secound photo -------------------------------------------------------
        private void openAnotherImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = " ";
            openFileDialog1.Filter = "JPEG|*JPG|Bitmap|*.bmp|All|*.*-11";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    imageSource2= cvlib.CvLoadImage(openFileDialog1.FileName, cvlib.CV_LOAD_IMAGE_COLOR);
                    CvSize size = new CvSize(pictureBox2.Width, pictureBox2.Height);
                    imageSource_resized2 = cvlib.CvCreateImage(size, imageSource2.depth, imageSource2.nChannels);
                    cvlib.CvResize(ref imageSource2, ref imageSource_resized2, cvlib.CV_INTER_LINEAR);
         
                    pictureBox2.BackgroundImage = (Image)imageSource_resized2;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
        //---------------------------Hiostgram-----------------------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series["Red"].Points.Clear();
            chart1.Series["Green"].Points.Clear();
            chart1.Series["Blue"].Points.Clear();
            //colored img which contains 3 colors 
            //make 3 hiostogram to each one 

            bmp = (Bitmap)imageSource1; //initializ the image through casting it from the source image 
            int width = bmp.Width;
            int height = bmp.Height;

            //make there fixed arrays          
            int[] ni_Red = new int[256];
            int[] ni_Green = new int[256];
            int[] ni_Blue = new int[256];
            //to get every pixel in the photo and count which color contains (R & G & B)
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color pixelColor = bmp.GetPixel(i, j);//which pixel 
                    //count the repetition of every level
                    ni_Red[pixelColor.R]++;//set index as a number of levels[0-255] and set the counter as nuber of pixels
                    ni_Green[pixelColor.G]++;
                    ni_Blue[pixelColor.B]++;

                }
            }

            // to Draw hiostgram 
            //every level by alone
            for (int i = 0; i < 256; i++)
            {
                chart1.Series["Red"].Points.AddY(ni_Red[i]);
                chart1.Series["Green"].Points.AddY(ni_Green[i]);
                chart1.Series["Blue"].Points.AddY(ni_Blue[i]);
            }



        }
        //-------------------------------------equalized Image---------------------------------------------------
        private void equalizedImage_Click(object sender, EventArgs e)
        {

            chart2.Series["Red"].Points.Clear();
            chart2.Series["Green"].Points.Clear();
            chart2.Series["Blue"].Points.Clear();

            bmp2 = (Bitmap)imageSource_resized1;
            int width = bmp2.Width;
            int height = bmp2.Height;


            //******************* Calculate N(i) **************//

            int[] ni_Red = new int[256];
            int[] ni_Green = new int[256];
            int[] ni_Blue = new int[256];





            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color pixelColor = bmp2.GetPixel(i, j);

                    ni_Red[pixelColor.R]++;
                    ni_Green[pixelColor.G]++;
                    ni_Blue[pixelColor.B]++;
                }
            }

            //******************* Calculate Probability(Ni) **************//
            decimal[] prob_ni_Red = new decimal[256]; //new arr which contains new data coming from p(ni)
            decimal[] prob_ni_Green = new decimal[256];
            decimal[] prob_ni_Blue = new decimal[256];

            for (int i = 0; i < 256; i++)
            {
                prob_ni_Red[i] = (decimal)ni_Red[i] / (decimal)(width * height);//new data of P(ni) / W * H
                prob_ni_Green[i] = (decimal)ni_Green[i] / (decimal)(width * height);
                prob_ni_Blue[i] = (decimal)ni_Blue[i] / (decimal)(width * height);
            }

            //******************* Calculate CDF **************//

            decimal[] cdf_Red = new decimal[256];
            decimal[] cdf_Green = new decimal[256];
            decimal[] cdf_Blue = new decimal[256];

            cdf_Red[0] = prob_ni_Red[0];
            cdf_Green[0] = prob_ni_Green[0];
            cdf_Blue[0] = prob_ni_Blue[0];

            for (int i = 1; i < 256; i++)
            {
                cdf_Red[i] = prob_ni_Red[i] + cdf_Red[i - 1];
                cdf_Green[i] = prob_ni_Green[i] + cdf_Green[i - 1];
                cdf_Blue[i] = prob_ni_Blue[i] + cdf_Blue[i - 1];
            }


            //******************* Calculate CDF(L-1) **************//


            int red, green, blue;
            int constant = 255;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color pixelColor = bmp2.GetPixel(i, j);

                    red = (int)Math.Round(cdf_Red[pixelColor.R] * constant);
                    green = (int)Math.Round(cdf_Red[pixelColor.G] * constant);
                    blue = (int)Math.Round(cdf_Red[pixelColor.B] * constant);
                    //take new photo and get equelized hiostgram from it 
                    Color newColor = Color.FromArgb(red, green, blue);
                    bmp2.SetPixel(i, j, newColor);

                }
            }

            pictureBox6.Image = (Image)bmp2;
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
        //------------------------------ equleized hiostgram ----------------------
        private void button1_Click_1(object sender, EventArgs e)
        {
            chart2.Series["Red"].Points.Clear();
            chart2.Series["Green"].Points.Clear();
            chart2.Series["Blue"].Points.Clear();



            bmp2 = (Bitmap)imageSource_resized1;
            int width = bmp2.Width;
            int height = bmp2.Height;


            int[] ni_Red = new int[256];
            int[] ni_Green = new int[256];
            int[] ni_Blue = new int[256];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color pixelColor = bmp2.GetPixel(i, j);

                    ni_Red[pixelColor.R]++;
                    ni_Green[pixelColor.G]++;
                    ni_Blue[pixelColor.B]++;

                }
            }


            for (int i = 0; i < 256; i++)
            {
                chart2.Series["Red"].Points.AddY(ni_Red[i]);
                chart2.Series["Green"].Points.AddY(ni_Green[i]);
                chart2.Series["Blue"].Points.AddY(ni_Blue[i]);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        //----------------------------------------Add too photos-----------------------------------
        private void addTwoImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            CvSize size = new CvSize(pictureBox3.Width, pictureBox3.Height);
            img3 = cvlib.CvCreateImage(size, imageSource1.depth, imageSource1.nChannels);



            int srcX = imageSource_resized1.imageData.ToInt32();
            int srcY = imageSource_resized2.imageData.ToInt32();
            int dstAddress = img3.imageData.ToInt32();
            unsafe
            {
                for (int r = 0; r < imageSource_resized1.height; r++)
                {
                    for (int c = 0; c < imageSource_resized1.width; c++)
                    {

                        int srcIndexX, srcIndexY, disIndex;
                        srcIndexX = (imageSource_resized1.width * r * imageSource_resized1.nChannels) + (imageSource_resized1.nChannels * c);
                        srcIndexY = (imageSource_resized2.width * r * imageSource_resized2.nChannels) + (imageSource_resized2.nChannels * c);
                        disIndex = (img3.width * r * img3.nChannels) + (img3.nChannels * c);

                        byte* redX = (byte*)(srcX + srcIndexX + 2);
                        byte* greenX = (byte*)(srcX + srcIndexX + 1);
                        byte* blueX = (byte*)(srcX + srcIndexX + 0);

                        byte* redY = (byte*)(srcY + srcIndexY + 2);
                        byte* greenY = (byte*)(srcY + srcIndexY + 1);
                        byte* blueY = (byte*)(srcY + srcIndexY + 0);

                        byte red = (byte)Math.Min(255, (*redX + *redY));
                        byte green = (byte)Math.Min(255, (*greenX + *greenY));
                        byte blue = (byte)Math.Min(255, (*blueX + *blueY));

                        *(byte*)(dstAddress + disIndex + 2) = red;
                        *(byte*)(dstAddress + disIndex + 1) = green;
                        *(byte*)(dstAddress + disIndex + 0) = blue;
                    }

                }
            }
            pictureBox3.BackgroundImage = (Image) img3;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //---------------------------------------------Blured Image------------------------------------------------
        private void blureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap blurred = new Bitmap(imageSource_resized1.width, imageSource_resized1.height);
            Rectangle rectangle = new Rectangle(new Point(0, 0), new Size(imageSource_resized1.width, imageSource_resized1.height));
            int blurSize=2;
            // make an exact copy of the bitmap provided
            using (Graphics graphics = Graphics.FromImage(blurred))
                graphics.DrawImage((Image)imageSource_resized1, new Rectangle(0, 0, imageSource_resized1.width, imageSource_resized1.height),
                    new Rectangle(0, 0, imageSource_resized1.width, imageSource_resized1.height), GraphicsUnit.Pixel);

            // look at every pixel in the blur rectangle
            for (Int32 xx = rectangle.X; xx < rectangle.X + rectangle.Width; xx++)
            {
                for (Int32 yy = rectangle.Y; yy < rectangle.Y + rectangle.Height; yy++)
                {
                    Int32 avgR = 0, avgG = 0, avgB = 0;
                    Int32 blurPixelCount = 0;

                    // average the color of the red, green and blue for each pixel
                    // blur size while making sure you don't go outside the image bounds
                    for (Int32 x = xx; (x < xx + blurSize && x < imageSource_resized1.width); x++)
                    {
                        for (Int32 y = yy; (y < yy + blurSize && y < imageSource_resized1.height); y++)
                        
                          {  Color pixel = blurred.GetPixel(x, y);

                            avgR += pixel.R;
                            avgG += pixel.G;
                            avgB += pixel.B;

                            blurPixelCount++;
                        }
                    }

                    avgR = avgR / blurPixelCount;
                    avgG = avgG / blurPixelCount;
                    avgB = avgB / blurPixelCount;

                    // now that we know the average for the blur size, set each pixel to that color
                    for (Int32 x = xx; x < xx + blurSize && x < imageSource_resized1.width && x < rectangle.Width; x++)
                        for (Int32 y = yy; y < yy + blurSize && y < imageSource_resized1.height && y < rectangle.Height; y++)
                            blurred.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                }
            }

            pictureBox5.Image = blurred;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

    
        }
    }

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//------------------- Developed by : omar nasr elsayed \ 119000024 \ class 13
