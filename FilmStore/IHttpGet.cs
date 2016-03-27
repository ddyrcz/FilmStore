using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmStore
{
    interface IHttpGet
    {
        Task<T> Get<T>(string request);
    }
}
