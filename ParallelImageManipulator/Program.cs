using System.Drawing;
using System.IO;

namespace ParallelImageManipulator
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));

            Bitmap img = new Bitmap($"{path}\\Resources\\rgb.bmp");
            ImageManipulator im = new ImageManipulator(img);
            im.Flip(true);
            im.Rotate(90, true);
            Bitmap ret = im.ToBitmap();
            ret.Save($"{path}\\Output\\output.bmp");
        }
    }
}
