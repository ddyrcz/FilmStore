using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmStore
{
    interface IImageDownload
    {
        Task<byte[]> Download(string uri);
    }
}
