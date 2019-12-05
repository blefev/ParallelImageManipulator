using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PIMWebsite
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnInvert_Click(object sender, EventArgs e)
        {

        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            Initilize();
            ImageManipulator.Filter(ddlFilter.SelectedValue);
        }

        protected void btnRotate_Click(object sender, EventArgs e)
        {
            Initilize();
            // False if 90-factor clockwise (right), True if 90-factor counter-clockwise (left)
            ImageManipulator.Rotate(Convert.ToInt32(txtRotateTimes.Text), Convert.ToBoolean(radbtnRotateDirection.SelectedValue));
        }

        protected void btnFlip_Click(object sender, EventArgs e)
        {
            Initilize();
            //true if verical, false if horizontal
            ImageManipulator.Flip(Convert.ToBoolean(radbtnFlip.SelectedValue));
        }

        private Bitmap ConvertToBMP(Bitmap img)
        {
            img.Save("input.bmp", ImageFormat.Bmp);
            return img;
        }

        protected void Initilize()
        {
            //string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            string filename = Path.GetFileName(FileUpload1.FileName);
            FileUpload1.SaveAs(Server.MapPath("~/") + filename);
            Bitmap img = new Bitmap(Server.MapPath("~/") + filename);
            ImageManipulator im = new ImageManipulator(img);
            //im.Rotate(90, true);

        }

        void OutputFile(ImageManipulator im, string path)
        {
            Bitmap ret = im.ToBitmap();
            ret.Save($"{path}\\Output\\output.bmp");
        }
    }
}