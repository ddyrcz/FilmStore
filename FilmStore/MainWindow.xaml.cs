using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FilmStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _apiKey = "469d646964e305889fe0cbc41689b037";
        private int _pageCount = 1;
        private List<Image> _popularFilmPosters = new List<Image>();
        private List<Image> _recentFilmPosters = new List<Image>();

        public MainWindow()
        {
            InitializeComponent();
            GetPopularFilms();
        }

        private void BindDownloadedPosters(List<Image> posters)
        {
            images.Children.Clear();
            posters.ForEach(x => images.Children.Add(x));
        }

        private async void DownloadPopularFilms()
        {
            IHttpGet adapter = new HttpAdapter();

            for (int page = 1; page <= _pageCount; page++)
            {
                Films popularFilms = await adapter.Get<Films>(string.Format("3/movie/popular?api_key={0}&page={1}", _apiKey, page));

                // In MVVM patern would not be a await key
                await IncludePosters(popularFilms, _popularFilmPosters);
            }
        }

        private async void DownloadRecentFilms()
        {
            IHttpGet adapter = new HttpAdapter();

            for (int page = 1; page <= _pageCount; page++)
            {
                Films recentFilms = await adapter.Get<Films>(string.Format("3/movie/now_playing?api_key={0}&page={1}", _apiKey, page));

                // In MVVM patern would not be a await key
                await IncludePosters(recentFilms, _recentFilmPosters);
            }
        }

        private bool FirstDownloadingPage(List<Image> posterList)
        {
            return posterList == null || !posterList.Any();
        }

        private void GetPopularFilms()
        {
            if (IsFilmDataDownloaded(_popularFilmPosters))
            {
                BindDownloadedPosters(_popularFilmPosters);
            }
            else
            {
                DownloadPopularFilms();
            }
        }

        private void GetRecentFilms()
        {
            if (IsFilmDataDownloaded(_recentFilmPosters))
            {
                BindDownloadedPosters(_recentFilmPosters);
            }
            else
            {
                DownloadRecentFilms();
            }
        }

        private void Img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            noDetails.Visibility = Visibility.Collapsed;
            details.Visibility = Visibility.Visible;

            if (sender is Image && ((Image)sender).Tag is Film)
            {
                Film film = ((Film)((Image)sender).Tag);
                image.Source = ((Image)sender).Source;
                title.Text = film.title_with_release_date;
                description.Text = film.overview;
                rate.Text = film.vote_average.ToString();
            }
        }

        private async Task IncludePosters(Films films, List<Image> posterList)
        {
            if (films == null || films.results == null) return;

            IUIObjectPrepareWithParameter<Image, Film> imagePreparer = new ImagePreparer();

            //if (FirstDownloadingPage(posterList))
            //{
            //    images.Children.Clear();
            //}

            foreach (Film film in films.results)
            {
                Image img = await imagePreparer.Prepare(film);
                posterList.Add(img);
                img.MouseDown += Img_MouseDown;
                images.Children.Add(img);
            }
        }

        private bool IsFilmDataDownloaded(List<Image> posterList)
        {
            return posterList != null && posterList.Any();
        }

        private void recentFilmsTab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            topFilmUnderLine.Visibility = Visibility.Collapsed;
            recentFilmUnderLine.Visibility = Visibility.Visible;
            GetRecentFilms();
        }

        private void topFilmsTab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            topFilmUnderLine.Visibility = Visibility.Visible;
            recentFilmUnderLine.Visibility = Visibility.Collapsed;
            GetPopularFilms();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e == null) return;

            // Close application
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}