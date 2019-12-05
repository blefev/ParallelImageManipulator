using System;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace ParallelImageManipulator
{
    class ImageManipulator
    {
        private Color[,] pixels;
        private int Width;
        private int Height;

        public ImageManipulator(Bitmap startImage)
        {
            Width = startImage.Width;
            Height = startImage.Height;
            pixels = ImageToBytes(startImage);
        }

        private Color[,] ImageToBytes(Bitmap img)
        {
            Color[,] matrix = new Color[Height, Width];

            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    matrix[y, x] = img.GetPixel(x, y);
                }
            }
            return matrix;
        }

        public Bitmap ToBitmap()
        {
            Bitmap bitmap = new Bitmap(Height, Width);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    bitmap.SetPixel(i, j, pixels[i, j]);
                }
            }
            return bitmap;
        }

        public void Grayscale()
        {
            Parallel.For(0, Height, i =>
            {
                for (int j = 0; j < Width; j++)
                {
                    Color px = pixels[i, j];
                    // create a new pixel from the grayvals substituted for RGB vals
                    byte grayVal = (byte)((px.R + px.G + px.B) / 3);
                    // set the new pixel
                    pixels[i, j] = Color.FromArgb(px.A, grayVal, grayVal, grayVal);

                }
            });
        }

        //true if verical, false if horizontal
        public void Flip(bool vertical)
        {
            if (vertical)
            {
                Parallel.For(0, Height / 2, i =>
                {
                    for (int j = 0; j < Width; j++)
                    {
                        Color temp = pixels[i, j];
                        pixels[i, j] = pixels[Width - i - 1, j];
                        pixels[Width - i - 1, j] = temp;
                    }
                });
            }
            else
            {
                Parallel.For(0, Width, i =>
                {
                    for (int j = 0; j < Height / 2; j++)
                    {
                        Color temp = pixels[i, j];
                        pixels[i, j] = pixels[i, Height - j - 1];
                        pixels[i, Height - j - 1] = temp;
                    }
                });
            }
        }

        // False if 90-factor clockwise (right), True if 90-factor counter-clockwise (left)
        public void Rotate(int times, bool direction)
        {
            // Rotate clockwise
            if (direction)
            {
                for (int x = 0; x < times; x += 90)
                {
                    Parallel.For(0, Width, i =>
                    {
                        for (int j = i; j < Height - i - 1; j++)
                        {
                            Color temp = pixels[i, j];
                            pixels[i, j] = pixels[Height - 1 - j, i];
                            pixels[Height - 1 - j, i] = pixels[Height - 1 - i, Height - 1 - j];
                            pixels[Height - 1 - i, Height - 1 - j] = pixels[j, Height - 1 - i];
                            pixels[j, Height - 1 - i] = temp;
                        }
                    });
                }
            }
            // Rotate counterclockwise
            else
            {
                for (int x = 0; x < times; x += 90)
                {
                    Parallel.For(0, Width, i =>
                    {
                        for (int j = i; j < Height - i - 1; j++)
                        {
                            Color temp = pixels[i, j];
                            pixels[i, j] = pixels[j, Height - 1 - i];
                            pixels[j, Height - 1 - i] = pixels[Height - 1 - i, Height - 1 - j];
                            pixels[Height - 1 - i, Height - 1 - j] = pixels[Height - 1 - j, i];
                            pixels[Height - 1 - j, i] = temp;
                        }
                    });
                }
            }
        }

        public void Filter(string color)
        {
            color = color.ToUpper();
            if (!(new string[] { "R", "G", "B" }.Contains(color)))
            {
                throw new Exception("Invalid color filter specified in ImageManipulator.Filter()");
            }

            Parallel.For(0, Height, i =>
            {
                for (int j = 0; j < Width; j++)
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
