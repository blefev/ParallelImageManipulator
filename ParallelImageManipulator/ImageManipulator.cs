using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

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

        // Simple fixed box blur with kernel of 1/9
        public void Blur(int neighborDist = 1)
        {
            Color[,] tmpPixels = new Color[Height, Width];
            // Doing this in two parts:
            // 1: Calculate 1/N of each pixel for moving average
            // 2: Use these values for blur values of pixels
            Parallel.For(0, Width, x =>
            {
                for (int y = 0; y < Height; y++)
                {
                    // calculate how many will be in neighborhood
                    // if neighborDist = 1, we go to each neighbor 1 unit away
                    // 1 * 2 + 1 = 3. 3 ^ 2 = 9. we will have a 3x3 kernel with 9 
                    // total neighbors. the fraction is used for the
                    // moving average
                    double fraction = 1.0 / (int)Math.Pow((neighborDist * 2 + 1), 2);
                    Color px = pixels[x, y];
                    int A = (int)(fraction * px.A);
                    int R = (int)(fraction * px.R);
                    int G = (int)(fraction * px.G);
                    int B = (int)(fraction * px.B);

                    Color newPx = Color.FromArgb(A, R, G, B);
                    tmpPixels[x, y] = newPx;
                }
            });
            Parallel.For(0, Width, x =>
            {
                for (int y = 0; y < Height; y++)
                {
                    Color px = tmpPixels[x, y];

                    int A = 0, R = 0, G = 0, B = 0;
                    // Iterate through neighbors
                    for (int i = -neighborDist; i++ <= neighborDist;)
                    {
                        if (x + i < 0 || x + i + 1 > Width) continue;
                        for (int j = -neighborDist; j++ <= neighborDist;)
                        {
                            if (y + j < 0 || y + j + 1 > Height) continue;
                            A += tmpPixels[x + i, y + j].A;
                            R += tmpPixels[x + i, y + j].R;
                            G += tmpPixels[x + i, y + j].G;
                            B += tmpPixels[x + i, y + j].B;
                        }
                    }
                    // replace pixels
                    pixels[x, y] = Color.FromArgb(A, R, G, B);
                }
            });
        }

        // Rec. 709 grayscaale
        public void Grayscale()
        {
            Parallel.For(0, Width, x =>
            {
                for (int y = 0; y < Height; y++)
                {
                    Color px = pixels[x, y];
                    // create a new pixel from the grayvals substituted for RGB vals
                    byte grayVal = (byte)(0.2126 * px.R + 0.7152 * px.G + 0.0722 * px.B);
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
            if (!clockwise) times += 2; // counterclockwise is same as clockwise + 2 turns
            times = times % 4; // max rotations is 4

            // if rotation times == 4, do nothing
            if (times == 4 || times == 0) return;


            int newWidth;
            int newHeight;

            // Stores rotated image
            if (times != 2)
            {
                newWidth = Height;
                newHeight = Width;
            }
            else
            {
                newWidth = Width;
                newHeight = Height;
            }

            Color[,] rotated = new Color[newWidth, newHeight];

            if (times == 1)
            {
                Parallel.For(0, Height, i =>
                {
                    for (int j = 0; j < Width; j++)
                    {
                        rotated[i, j] = pixels[j, Height - i - 1];
                    }
                });
            }
            // Rotate counter-clockwise
            else if (times == 2)
            {
                Parallel.For(0, Width, x =>
                {
                    for (int y = 0; y < Height; y++)
                    {
                        rotated[newWidth - 1 - x, newHeight - 1 - y] = pixels[x, y];
                    }
                });
            }
            // Rotate clockwise
            else if (times == 3)
            {
                Parallel.For(0, Height, i =>
                {
                    for (int j = 0; j < Width; j++)
                    {
                        rotated[i, j] = pixels[Width - j - 1, i];
                    }
                });
            }
            Height = newHeight;
            Width = newWidth;
            pixels = rotated;
        }

        public void Brightness(int brightness)
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

                    r = Truncate(r + brightness);
                    g = Truncate(g + brightness);
                    b = Truncate(b + brightness);

                    pixels[x, y] = Color.FromArgb(a, r, g, b);

                }
            });
        }

        // For Brightness()
        public int Truncate(int value)
        {
            if (value < 0)
            {
                value = 0;
            }

            if (value > 255)
            {
                value = 255;
            }
            return value;
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
                    int newR = 0, newG = 0, newB = 0;
                    Color px = pixels[x, y];

                    switch (color)
                    {
                        case "R":
                            newR = (int)px.R;
                            newG = (px.G - 255);
                            newB = (px.B - 255);
                            break;
                        case "G":
                            newR = (px.B - 255);
                            newG = (int)px.G;
                            newB = (px.B - 255);
                            break;
                        case "B":
                            newR = (px.B - 255);
                            newG = (px.G - 255);
                            newB = (int)px.B;
                            break;
                    }
                    // Keep range between 0 and 255
                    newR = Math.Min(255, Math.Max(newR, 0));
                    newG = Math.Min(255, Math.Max(newG, 0));
                    newB = Math.Min(255, Math.Max(newB, 0));

                    pixels[x, y] = Color.FromArgb(px.A, newR, newG, newB);
                }
            });
        }
    }
}
