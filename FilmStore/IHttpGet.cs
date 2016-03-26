using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmStore
{
    interface IHttpGet<T> where T : class
    {
        Task<T> Get(Uri uri);
    }
}
