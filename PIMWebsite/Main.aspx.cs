using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ParallelImageManipulator;

namespace PIMWebsite
{
    public partial class Main : System.Web.UI.Page
    {
        Bitmap Img;
        ImageManipulator IM;

        protected void Initialize()
        {
            //string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            string filename = Path.GetFileName(FileUpload1.FileName);
            Img = new Bitmap(FileUpload1.PostedFile.InputStream);
            IM = new ImageManipulator(Img);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        protected void btnInvert_Click(object sender, EventArgs e)
        {

        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                Initialize();
                IM.Filter(ddlFilter.SelectedValue);
            }
            else
            {
                lblError.Text = "Please select a file.";
                lblError.Visible = true;

            }
        }

        protected void btnRotate_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                Initialize();
                // False if 90-factor clockwise (right), True if 90-factor counter-clockwise (left)
                IM.Rotate(Convert.ToInt32(txtRotateTimes.Text), Convert.ToBoolean(radbtnRotateDirection.SelectedValue));
            }
            else
            {
                lblError.Text = "Please select a file.";
                lblError.Visible = true;



            }
        }

        protected void btnFlip_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                Initialize();
                //true if verical, false if horizontal
                IM.Flip(Convert.ToBoolean(radbtnFlip.SelectedValue));
            }
            else
            {
                lblError.Text = "Please select a file.";
                lblError.Visible = true;

            }
        }


        protected void btnNegate_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                Initialize();
                IM.Negate();
            }
            else
            {
                lblError.Text = "Please select a file.";
                lblError.Visible = true;

            }
        }

        private Bitmap ConvertToBMP(Bitmap img)
        {
            img.Save("input.bmp", ImageFormat.Bmp);
            return img;
        }



        void OutputFile(ImageManipulator im, string path)
        {
            Bitmap ret = im.ToBitmap();
            ret.Save($"{path}\\Output\\output.bmp");

            lblError.Text = "Your modified file has been saved in " + $"{path}\\Output\\output.bmp";
        }

        protected void btnGrayscale_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                Initialize();
                IM.Grayscale();
            }
            else
            {
                lblError.Text = "Please select a file.";
                lblError.Visible = true;
            }
        }
    }
}