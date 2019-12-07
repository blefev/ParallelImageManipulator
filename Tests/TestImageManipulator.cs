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
using ParallelImageManipulator;
using System.Collections.Generic;
using System.Reflection;

namespace Tests
{
    
    [TestClass]
    public class TestImageManipulator
    {
        private static string BaseDir = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
        private static Bitmap  SquareBmp    = Properties.Resources.SquareBmp,
                        SquarePng    = Properties.Resources.SquarePng,
                        SquareJpg    = Properties.Resources.SquareJpg,
                        RectangleBmp = Properties.Resources.RectangleBmp,
                        RectanglePng = Properties.Resources.RectanglePng,
                        RectangleJpg = Properties.Resources.RectangleJpg;

        private Dictionary<String, Bitmap> AllImages = new Dictionary<String, Bitmap>(){
                                                            { "SquarePng" , SquarePng},
                                                           // {"SquareJpg" , SquareJpg},
                                                            {"RectangleBmp" , RectangleBmp},
                                                            {"RectanglePng" , RectanglePng} };
                                                          // {"RectangleJpg" , RectangleJpg }};


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
            Assert.IsTrue(BmpsAreEqual(SquareBmp, SquareBmp), "Equal are equal");
            Assert.IsFalse(BmpsAreEqual(SquareBmp, RectangleBmp), "Unequal are unequal");
        }

        [TestMethod]
        public void ImageToBytes()
        {

        }

        [TestMethod]
        public void ToBitmap()
        {
            foreach (KeyValuePair<string, Bitmap> entry in AllImages)
            {
                Bitmap bmp = entry.Value;
                ImageManipulator im = new ImageManipulator(bmp);

                Assert.IsTrue(BmpsAreEqual(im.ToBitmap(), bmp));
            }
            
        }

        private void TestAllImagesWithDelegate(Delegate transformer)
        {

        }

        [TestMethod]
        public void FlipSquareVertical()
        {
            foreach (KeyValuePair<string, Bitmap> entry in AllImages)
            {
                Bitmap bmp = entry.Value;

                ImageManipulator im = new ImageManipulator(bmp);
                MagickImage mi = new MagickImage(bmp);

                im.Flip(true);
                mi.Flip();

                im.ToBitmap().Save($"{BaseDir}\\TestOutput\\FlipSquareVertical" + entry.Key + "im.jpg");
                mi.ToBitmap().Save($"{BaseDir}\\TestOutput\\FlipSquareVertical" + entry.Key + "mi.jpg");

                Assert.IsTrue(BmpsAreEqual(im.ToBitmap(), mi.ToBitmap()), $"{entry.Key} failed");
            }
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
