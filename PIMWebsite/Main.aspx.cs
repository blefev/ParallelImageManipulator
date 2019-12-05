using System;
using System.Collections.Generic;
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

        }

        protected void btnRotate_Click(object sender, EventArgs e)
        {
            // False if 90-factor clockwise (right), True if 90-factor counter-clockwise (left)
            ImageManipulator.Rotate(Convert.ToInt32(txtRotateTimes.Text), Convert.ToBoolean(radbtnRotateDirection.SelectedValue));
        }

        protected void btnFlip_Click(object sender, EventArgs e)
        {
            //true if verical, false if horizontal
            ImageManipulator.Flip(Convert.ToBoolean(radbtnFlip.SelectedValue));
        }
    }
}