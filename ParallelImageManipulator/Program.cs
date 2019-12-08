using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace ParallelImageManipulator
{
    class Program
    {
        static void Main(string[] args)
        {
            SetParallelismToCoreCount();

            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));

            /* TODO: use commandline arguments */
            Bitmap img = new Bitmap($"{path}\\..\\Tests\\Resources\\Square.png");
            ImageManipulator im = new ImageManipulator(img);
            im.Rotate(2, true);
            Bitmap ret = im.ToBitmap();
            ret.Save($"{path}\\Output\\output.jpg");
        }

        static private void SetParallelismToCoreCount()
        {
            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = System.Environment.ProcessorCount;
        }
    }
}
