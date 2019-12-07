﻿using System;
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
            Color[,] matrix = new Color[Width, Height];

            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    matrix[x, y] = img.GetPixel(x, y);
                }
            }
            return matrix;
        }

        public Bitmap ToBitmap()
        {
            Bitmap bitmap = new Bitmap(Width, Height);

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    bitmap.SetPixel(x, y, pixels[x, y]);
                }
            }
            return bitmap;
        }

        public void Grayscale()
        {
            Parallel.For(0, Width, x =>
            {
                for (int y = 0; y < Height; y++)
                {
                    Color px = pixels[x, y];
                    // create a new pixel from the grayvals substituted for RGB vals
                    byte grayVal = (byte)((px.R + px.G + px.B) / 3);
                    // set the new pixel
                    pixels[x, y] = Color.FromArgb(px.A, grayVal, grayVal, grayVal);

                }
            });
        }

        //true if verical, false if horizontal
        public void Flip(bool vertical)
        {
            if (vertical)
            {
                Parallel.For(0, Width, x =>
                {
                    for (int y = 0; y < Height / 2; y++)
                    {
                        Color temp = pixels[x, y];
                        pixels[x, y] = pixels[x, Height - y - 1];
                        pixels[x, Height - y - 1] = temp;
                    }
                });
            }
            else
            {
                Parallel.For(0, Width / 2, x =>
                {
                    for (int y = 0; y < Height; y++)
                    {
                        Color temp = pixels[x, y];
                        pixels[x, y] = pixels[Width - x - 1, y];
                        pixels[Width - x - 1, y] = temp;
                    }
                });
            }
        }

        // False if 90-factor clockwise (right), True if 90-factor counter-clockwise (left)
        public void Rotate(int times, bool clockwise = true)
        {
            // Rotate clockwise
            if (clockwise)
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
        
        public void Negate()
        {
            Parallel.For(0, Width, x =>
            {
                for (int y = 0; y < Height; y++)
                {

                    Color px = pixels[x, y];

                    int a = px.A;
                    int r = px.R;
                    int g = px.G;
                    int b = px.B;

                    r = 255 - r;
                    g = 255 - g;
                    b = 255 - b;

                    pixels[x, y] = Color.FromArgb(a, r, g, b);

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

            Parallel.For(0, Width, x =>
            {
                for (int y = 0; y < Height; y++)
                {
                    byte newR = 0, newG = 0, newB = 0;
                    Color px = pixels[x, y];

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

                    pixels[x, y] = Color.FromArgb(px.A, newR, newG, newB);
                }
            });
        }
    }
}
