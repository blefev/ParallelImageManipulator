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
        private Color[ , ] pixelMatrix;
        private int Height;
        private int Width;

        public ImageManipulator(Bitmap startImage)
        {
            Height = startImage.Height;
            Width = startImage.Width;
            pixelMatrix = ImageToBytes(startImage);
        }

        private Color[ , ] ImageToBytes(Bitmap img)
        {
                Color[,] matrix = new Color[Height, Width];

                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        matrix[i , j] = img.GetPixel(i, j);
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
                        bitmap.SetPixel(i, j, pixelMatrix[i , j]);
                    }
                }
                return bitmap;
        }
    }
}
