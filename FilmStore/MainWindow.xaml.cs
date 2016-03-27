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

        public MainWindow()
        {
            InitializeComponent();
            GetPopularFilms();
        }

        private async void GetPopularFilms()
        {
            IHttpGet adapter = new HttpAdapter();

            Films popularFilms = await adapter.Get<Films>(string.Format("3/movie/popular?api_key={0}", _apiKey));
            
            // To cut download time
            // popularFilms.results = popularFilms.results.Take(3).ToList();

            // In MVVM patern would not be a await key 
            await IncludePosters(popularFilms);
            
            // filmList.ItemsSource = popularFilms.results.OrderByDescending(x => x.vote_average);
        }

        private async Task IncludePosters(Films popularFilms)
        {
            if (popularFilms == null || popularFilms.results == null) return;

            IImageDownload downloader = new ImageDownloader();
            var imagePreparer = new ImagePreparer();

            foreach (Film film in popularFilms.results)
            {                                
                byte[] bytes = await downloader.Download("t/p/w500" + film.poster_path);
                Image img = imagePreparer.Prepare(Converter.FromBytesToBitmapImage(bytes));
                images.Children.Add(img);
            }            
        }
    }
}
