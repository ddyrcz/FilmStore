using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FilmStore
{
    class Films
    {
        public List<Film> results{ get; set; }        
    }

    class Film
    {
        public string title { get; set; }
        public string overview { get; set; }
        public double vote_average { get; set; }
        public string poster_path { get; set; }        
        public DateTime release_date { get; set; }
        public string release_year { get { return "(" + release_date.Year + ")"; }  }
    }
}
