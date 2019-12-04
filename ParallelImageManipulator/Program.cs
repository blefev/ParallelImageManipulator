using System.Drawing;
using System.Drawing.Imaging;
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
            im.Rotate(90, true);
            Bitmap ret = im.ToBitmap();
            ret.Save($"{path}\\Output\\output.bmp");
        }

        private Bitmap ConvertToBMP(Bitmap img)
        {
            img.Save("input.bmp", ImageFormat.Bmp);

            return img;
        }
    }
}
