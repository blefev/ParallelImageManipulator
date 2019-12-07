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
using System.Threading.Tasks;
using System.Diagnostics;

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
                                                            { "SquarePng.png" , SquarePng},
                                                            { "SquareBmp.bmp", SquareBmp },
                                                            {"RectangleBmp.bmp" , RectangleBmp},
                                                            {"RectanglePng.png" , RectanglePng} };


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

        

        [TestMethod]
        public void BmpsAreEqualWorks()
        {
            Assert.IsTrue(BmpsAreEqual(SquareBmp, SquareBmp), "Equal are equal");
            Assert.IsFalse(BmpsAreEqual(SquareBmp, RectangleBmp), "Unequal are unequal");
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

        [TestMethod]
        public void Grayscale()
        {
            foreach (KeyValuePair<string, Bitmap> entry in AllImages)
            {
                Bitmap bmp = entry.Value;

                ImageManipulator im = new ImageManipulator(bmp);
                MagickImage mi = new MagickImage(bmp);

                im.Grayscale();
                Bitmap answer = new Bitmap($"{BaseDir}\\Resources\\Answers\\Grayscale_" + entry.Key);

                bool passed = BmpsAreEqual(im.ToBitmap(), answer);

                if (!passed)
                {
                    im.ToBitmap().Save($"{BaseDir}\\TestOutput\\Grayscale" + entry.Key);
                }

                Assert.IsTrue(passed, $"{entry.Key} failed");
            }
        }

        [TestMethod]
        public void FlipVertical()
        {
            foreach (KeyValuePair<string, Bitmap> entry in AllImages)
            {
                Bitmap bmp = entry.Value;

                ImageManipulator im = new ImageManipulator(bmp);
                MagickImage mi = new MagickImage(bmp);

                im.Flip(true);
                mi.Flip();

                bool passed = BmpsAreEqual(im.ToBitmap(), mi.ToBitmap());

                if (!passed) {
                    im.ToBitmap().Save($"{BaseDir}\\TestOutput\\FlipVertical" + entry.Key + " ImageManipulator.png");
                    mi.ToBitmap().Save($"{BaseDir}\\TestOutput\\FlipVertical" + entry.Key + " MagickImage.png");
                }

                Assert.IsTrue(passed, $"{entry.Key} failed");
            }
        }

        [TestMethod]
        public void FlipHorizontal()
        {
            foreach (KeyValuePair<string, Bitmap> entry in AllImages)
            {
                Bitmap bmp = entry.Value;

                ImageManipulator im = new ImageManipulator(bmp);
                MagickImage mi = new MagickImage(bmp);

                im.Flip(false);
                mi.Flop();

                bool passed = BmpsAreEqual(im.ToBitmap(), mi.ToBitmap());

                if (!passed)
                {
                    im.ToBitmap().Save($"{BaseDir}\\TestOutput\\FlipHorizontal" + entry.Key + " ImageManipulator.png");
                    mi.ToBitmap().Save($"{BaseDir}\\TestOutput\\FlipHorizontal" + entry.Key + " MagickImage.png");
                }

                Assert.IsTrue(passed, $"{entry.Key} failed");
            }
        }

        [TestMethod]
        public void Rotate()
        {
            foreach (KeyValuePair<string, Bitmap> entry in AllImages)
            {
                Bitmap bmp = entry.Value;

                ImageManipulator im = new ImageManipulator(bmp);
                MagickImage mi = new MagickImage(bmp);

                for (int rotates = 1; rotates < 6; rotates++)
                {
                    foreach (bool clockwise in (new bool[] {true, false}))
                    {
                        im.Rotate(rotates, clockwise);
                        int degrees = rotates * 90;
                        if (!clockwise) degrees = -degrees;
                        im.Rotate(rotates, clockwise);
                        mi.Rotate(degrees);

                        bool passed = BmpsAreEqual(im.ToBitmap(), mi.ToBitmap());

                        if (!passed)
                        {
                            im.ToBitmap().Save($"{BaseDir}\\TestOutput\\Rotate" + entry.Key + " ImageManipulator.png");
                            mi.ToBitmap().Save($"{BaseDir}\\TestOutput\\Rotate" + entry.Key + " MagickImage.png");
                        }

                        Assert.IsTrue(passed, $"{entry.Key} failed rotation at {degrees} degrees");
                    }
                }
            }
        }

        [TestMethod]
        public void Negate()
        {
            foreach (KeyValuePair<string, Bitmap> entry in AllImages)
            {
                Bitmap bmp = entry.Value;

                ImageManipulator im = new ImageManipulator(bmp);
                MagickImage mi = new MagickImage(bmp);

                im.Negate();
                Bitmap answer = new Bitmap($"{BaseDir}\\Resources\\Answers\\Negate" + entry.Key);

                bool passed = BmpsAreEqual(im.ToBitmap(), answer);

                if (!passed)
                {
                    im.ToBitmap().Save($"{BaseDir}\\TestOutput\\Negate" + entry.Key + " ImageManipulator.png");
                    mi.ToBitmap().Save($"{BaseDir}\\TestOutput\\Negate" + entry.Key + " MagickImage.png");
                }

                Assert.IsTrue(passed, $"{entry.Key} failed");
            }
        }

        [TestMethod]
        public void Filter()
        {
            foreach (string color in new string[] { "R", "G", "B" })
            {
                foreach (KeyValuePair<string, Bitmap> entry in AllImages)
                {
                    Bitmap bmp = entry.Value;

                    ImageManipulator im = new ImageManipulator(bmp);
                    MagickImage mi = new MagickImage(bmp);

                    im.Filter(color);
                    im.ToBitmap().Save($"{BaseDir}\\Resources\\Answers\\Filter" +color + entry.Key);
                    /*
                    bool passed = BmpsAreEqual(im.ToBitmap(), mi.ToBitmap());

                    if (!passed)
                    {
                        im.ToBitmap().Save($"{BaseDir}\\TestOutput\\Filter" + entry.Key + " ImageManipulator.png");
                        mi.ToBitmap().Save($"{BaseDir}\\TestOutput\\Filter" + entry.Key + " MagickImage.png");
                    }

                    Assert.IsTrue(passed, $"{entry.Key} failed");
                    */
                }
            }
        }
    }
}
