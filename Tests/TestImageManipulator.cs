﻿/*
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

namespace Tests
{
    
    [TestClass]
    public class TestImageManipulator
    {
        private static string BaseDir = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));

        // Requires ImageMagick installed on your machine: https://imagemagick.org/script/download.php
        private void RunImageMagick(string command)
        {

        }

        private bool BmpsAreEqual(Bitmap bmp1, Bitmap bmp2)
        {
            return false;
        }

        private Bitmap ReadTestBitmap(string name)
        {
            Bitmap bmp = new Bitmap($"{BaseDir}\\Resources\\${name}");
            return bmp;
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