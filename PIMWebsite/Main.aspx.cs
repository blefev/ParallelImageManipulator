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

                MemoryStream ms = new MemoryStream();
                Bitmap ImgModified = IM.ToBitmap();
                ImgModified.Save(ms, ImageFormat.Gif);
                var base64Data = Convert.ToBase64String(ms.ToArray());
                imgCtrl.Src = "data:image/gif;base64," + base64Data;
                imgCtrl.Visible = true;
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
                MemoryStream ms = new MemoryStream();
                Bitmap ImgModified = IM.ToBitmap();
                ImgModified.Save(ms, ImageFormat.Gif);
                var base64Data = Convert.ToBase64String(ms.ToArray());
                imgCtrl.Src = "data:image/gif;base64," + base64Data;
                imgCtrl.Visible = true;

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
                MemoryStream ms = new MemoryStream();
                Bitmap ImgModified = IM.ToBitmap();
                ImgModified.Save(ms, ImageFormat.Gif);
                var base64Data = Convert.ToBase64String(ms.ToArray());
                imgCtrl.Src = "data:image/gif;base64," + base64Data;
                imgCtrl.Visible = true;

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
                MemoryStream ms = new MemoryStream();
                Bitmap ImgModified = IM.ToBitmap();
                ImgModified.Save(ms, ImageFormat.Gif);
                var base64Data = Convert.ToBase64String(ms.ToArray());
                imgCtrl.Src = "data:image/gif;base64," + base64Data;
                imgCtrl.Visible = true;

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


        protected void btnGrayscale_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                Initialize();
                IM.Grayscale();
                MemoryStream ms = new MemoryStream();
                Bitmap ImgModified = IM.ToBitmap();
                ImgModified.Save(ms, ImageFormat.Gif);
                var base64Data = Convert.ToBase64String(ms.ToArray());
                imgCtrl.Src = "data:image/gif;base64," + base64Data;
                imgCtrl.Visible = true;

            }
            else
            {
                lblError.Text = "Please select a file.";
                lblError.Visible = true;
            }
        }

        protected void btnBlur_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                Initialize();
                IM.Blur();
                MemoryStream ms = new MemoryStream();
                Bitmap ImgModified = IM.ToBitmap();
                ImgModified.Save(ms, ImageFormat.Gif);
                var base64Data = Convert.ToBase64String(ms.ToArray());
                imgCtrl.Src = "data:image/gif;base64," + base64Data;
                imgCtrl.Visible = true;

            }
            else
            {
                lblError.Text = "Please select a file.";
                lblError.Visible = true;

            }
        }

        protected void btnBrightness_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                Initialize();
                IM.Brightness(Convert.ToInt32(txtBrightness.Text));

                MemoryStream ms = new MemoryStream();
                Bitmap ImgModified = IM.ToBitmap();
                ImgModified.Save(ms, ImageFormat.Gif);
                var base64Data = Convert.ToBase64String(ms.ToArray());
                imgCtrl.Src = "data:image/gif;base64," + base64Data;
                imgCtrl.Visible = true;

            }
            else
            {
                lblError.Text = "Please select a file.";
                lblError.Visible = true;

            }
        }
    }
}