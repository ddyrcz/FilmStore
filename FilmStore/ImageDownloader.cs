using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FilmStore
{
    class ImageDownloader : IImageDownload
    {
        public async Task<byte[]> Download(string uri)
        {
            byte[] bytes = default(byte[]);

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://image.tmdb.org");
                    bytes = await client.GetByteArrayAsync(uri);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", ex.Message);
            }

            return bytes;
        }
    }
}
