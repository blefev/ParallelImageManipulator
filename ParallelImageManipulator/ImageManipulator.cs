using System;
using System.Linq;
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
                    // create a new pixel from the grayvals substituted for RGB vals
                    byte grayVal = (byte)((px.R + px.G + px.B) / 3);
                    // set the new pixel
                    pixels[i, j] = Color.FromArgb(px.A, grayVal, grayVal, grayVal);

                }
            });
        }
        public void Filter(string color)
        {
            color = color.ToUpper();
            if (!(new string[] { "R", "G", "B" }.Contains(color)))
            {
                throw new Exception("Invalid color filter specified in ImageManipulator.Filter()");
            }

            Parallel.For(0, Width, i =>
            {
                for (int j = 0; j < Height; j++)
                {
                    byte newR = 0, newG = 0, newB = 0;
                    Color px = pixels[i, j];

                    switch (color)
                    {
                        case "R":
                            newR = px.R;
                            newG = (byte)(px.G - 255);
                            newB = (byte)(px.B - 255);
                            break;
                        case "G":
                            newR = (byte)(px.B - 255);
                            newG = px.G;
                            newB = (byte)(px.B - 255);
                            break;
                        case "B":
                            newR = (byte)(px.B - 255);
                            newG = (byte)(px.G - 255);
                            newB = px.B;
                            break;
                    }
                    // Keep range between 0 and 255
                    newR = Math.Min((byte)255, Math.Max(newR, (byte)0));
                    newG = Math.Min((byte)255, Math.Max(newG, (byte)0));
                    newB = Math.Min((byte)255, Math.Max(newB, (byte)0));

                    pixels[i, j] = Color.FromArgb(px.A, newR, newG, newB);
                }
            });
        }
    }
}
