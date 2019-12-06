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

            Bitmap img = new Bitmap($"{path}\\Resources\\face.jpg");
            ImageManipulator im = new ImageManipulator(img);
            im.Rotate(1, true);
            Bitmap ret = im.ToBitmap();
            ret.Save($"{path}\\Output\\output.jpg");
        }

        private Bitmap ConvertToBMP(Bitmap img)
        {
            img.Save("input.bmp", ImageFormat.Bmp);

            return img;
        }
    }
}
