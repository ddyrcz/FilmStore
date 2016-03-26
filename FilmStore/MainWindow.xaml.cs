using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FilmStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _apiKey = "469d646964e305889fe0cbc41689b037";

        List<string> _data = new List<string>
        {
            "data",
            "data",
            "data",
            "data",
            "data",
        };        

        public MainWindow()
        {
            InitializeComponent();
            GetPopularFilms();
            
        }

        private async void GetPopularFilms()
        {
            IHttpGet<Films> adapter = new HttpAdapter<Films>();

            Films popularFilms = await adapter.Get(string.Format("3/movie/popular?api_key={0}", _apiKey));
            // w MVVM mozna nie dawac awaita. Zdjecia sie beda pobierac w tle
             IncludePosters(popularFilms);

            filmList.ItemsSource = popularFilms.results.OrderByDescending(x => x.vote_average);
        }

        private async void IncludePosters(Films popularFilms)
        {
            if (popularFilms == null || popularFilms.results == null) return;

            IImageDownload downloader = new ImageDownloader();

            foreach (Film film in popularFilms.results)
            {
                byte[] bytes = await downloader.Download("t/p/w500" + film.poster_path);
                film.Poster = Converter.FromBytesToBitmapImage(bytes);
            }                        
        }
    }
}
