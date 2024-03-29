﻿using System.Drawing;
using System.IO;

namespace ParallelImageManipulator
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));

            /* TODO: use commandline arguments */
            Bitmap img = new Bitmap($"{path}\\..\\Tests\\Resources\\Square.png");
            ImageManipulator im = new ImageManipulator(img);
            im.Blur(9);
            Bitmap ret = im.ToBitmap();
            ret.Save($"{path}\\Output\\output.jpg");
        }
    }
}
