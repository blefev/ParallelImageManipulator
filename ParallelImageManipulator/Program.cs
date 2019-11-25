using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ParallelImageManipulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Bitmap img = new Bitmap("D:\\School\\CSC-410 Parallel Computing\\ParallelImageManipulator\\ParallelImageManipulator\\Resources\\fractal.bmp");
            ImageManipulator im = new ImageManipulator(img);
            im.Grayscale();
            Bitmap ret = im.ToBitmap();
            ret.Save("D:\\School\\CSC-410 Parallel Computing\\ParallelImageManipulator\\ParallelImageManipulator\\output.bmp");
        }
    }
}
