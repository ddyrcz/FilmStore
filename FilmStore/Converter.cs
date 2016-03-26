using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FilmStore
{
    static class Converter
    {
        public static BitmapImage FromBytesToBitmapImage(byte[] bytes)
        {
            var result = new BitmapImage();

            try
            {
                result.BeginInit();
                result.StreamSource = new System.IO.MemoryStream(bytes);
                result.EndInit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", ex.Message);
            }

            return result;

        }
    }
}
