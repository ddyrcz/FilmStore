﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace FilmStore
{
    class HttpAdapter<T> : IHttpGet<T> where T : class
    {
        public async Task<T> Get(string request)
        {
            T result = default(T);
                                                      
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.themoviedb.org");
                    string jsonResult = await client.GetStringAsync(request);
                    result = new JavaScriptSerializer().Deserialize<T>(jsonResult);
                }
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