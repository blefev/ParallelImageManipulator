using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ParallelImageManipulator
{
    class ImageManipulator
    {
        private Color[,] pixels;
        private int Height;
        private int Width;

        public ImageManipulator(Bitmap startImage)
        {
            Height = startImage.Height;
            Width = startImage.Width;
            pixels = ImageToBytes(startImage);
        }

        private Color[,] ImageToBytes(Bitmap img)
        {
            Color[,] matrix = new Color[Height, Width];

            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    matrix[i, j] = img.GetPixel(i, j);
                }
            }
            return matrix;
        }

        public Bitmap ToBitmap()
        {

            Bitmap bitmap = new Bitmap(Width, Height);

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    bitmap.SetPixel(i, j, pixels[i, j]);
                }
            }
            return bitmap;
        }

        public void Grayscale()
        {
            Parallel.For(0, Width, i =>
            {
                for (int j = 0; j < Height; j++)
                {
                    Color px = pixels[i, j];
                    byte grayVal = (byte)((px.R + px.G + px.B) / 3);
                    pixels[i, j] = Color.FromArgb(px.A, grayVal, grayVal, grayVal);

                }
            });
        }
    }
}
