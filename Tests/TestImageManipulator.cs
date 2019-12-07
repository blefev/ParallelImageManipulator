/*
 *  Black box tests. Things to test:
 *  
 *  Each method
 *  - Different image sizes (square, rectangular, w > h, h > w)
 *  - Different formats: jpg, png, bmp (may need to factor in compression??)
 * 
 * Need test images. Make known-good images for use in tests.
 * 
 */
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.IO;
using ImageMagick;
using System.Drawing.Imaging;

namespace Tests
{
    
    [TestClass]
    public class TestImageManipulator
    {
        private static string BaseDir = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
        private Bitmap  SquareBmp    = Properties.Resources.SquareBmp,
                        SquarePng    = Properties.Resources.SquarePng,
                        SquareJpg    = Properties.Resources.SquareJpg,
                        RectangleBmp = Properties.Resources.RectangleBmp,
                        RectanglePng = Properties.Resources.RectanglePng,
                        RectangleJpg = Properties.Resources.RectangleJpg;

        // Requires ImageMagick installed on your machine: https://imagemagick.org/script/download.php
        private Bitmap RunImageMagick(Bitmap bmp, string command)
        {
            MagickImage img = new MagickImage(bmp);

            Bitmap returnImg = img.ToBitmap();
            return returnImg;
        }

        // Code modified from
        // https://codereview.stackexchange.com/questions/39980/determining-if-2-images-are-the-same
        private bool BmpsAreEqual(Bitmap bmp1, Bitmap bmp2)
        {
            if (!bmp1.Size.Equals(bmp2.Size))
            {
                return false;
            }
            for (int x = 0; x < bmp1.Width; ++x)
            {
                for (int y = 0; y < bmp1.Height; ++y)
                {
                    if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        
        private Bitmap ReadTestBitmap(string name)
        {
            Bitmap bmp = new Bitmap($"{BaseDir}\\Resources\\${name}");
            return bmp;
        }

        [TestMethod]
        public void BmpsAreEqualWorks()
        {
            Assert.IsTrue(BmpsAreEqual(SquareBmp, SquareBmp));
        }

        [TestMethod]
        public void ImageToBytes()
        {

        }

        [TestMethod]
        public void ToBitmap()
        {

        }

        [TestMethod]
        public void FlipSquareVertical()
        {

        }

        [TestMethod]
        public void FlipSquareHorizontal()
        {

        }

        [TestMethod]
        public void FlipRectangleVertical()
        {

        }

        [TestMethod]
        public void FlipRectangleHorizontal()
        {

        }


        [TestMethod]
        public void RotateSquareClockwise()
        {

        }

        [TestMethod]
        public void RotateSquareCounterClockwise()
        {

        }

        [TestMethod]
        public void RotateRectangleClockwise()
        {

        }

        [TestMethod]
        public void RotateRectangleCounterClockwise()
        {

        }

        [TestMethod]
        public void Negate()
        {

        }

        [TestMethod]
        public void FilterRed()
        {

        }

        [TestMethod]
        public void FilterBlue()
        {

        }

        [TestMethod]
        public void FilterGreen()
        {

        }
    }
}
