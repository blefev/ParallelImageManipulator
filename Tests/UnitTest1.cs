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

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {



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
