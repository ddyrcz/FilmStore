using System.Collections.Generic;
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
        private bool _popularFilmsDownloadingStarted;
        private List<Image> _recentFilmPosters = new List<Image>();
        private bool _recentFilmsDownloadingStarted;

        public MainWindow()
        {
            InitializeComponent();
            GetPopularFilms();
        }

        private async void DownloadPopularFilms()
        {
            IHttpGet adapter = new HttpAdapter();

            for (int page = 1; page <= _pageCount; page++)
            {
                Films popularFilms = await adapter.Get<Films>(string.Format("3/movie/popular?api_key={0}&page={1}", _apiKey, page));

                // In MVVM pattern would not be a await key
                await IncludePosters(popularFilms, _popularFilmPosters, popularImages);
            }
        }

        private async void DownloadRecentFilms()
        {
            IHttpGet adapter = new HttpAdapter();

            for (int page = 1; page <= _pageCount; page++)
            {
                Films recentFilms = await adapter.Get<Films>(string.Format("3/movie/now_playing?api_key={0}&page={1}", _apiKey, page));

                // In MVVM pattern would not be a await key
                await IncludePosters(recentFilms, _recentFilmPosters, recentImages);
            }
        }

        private void GetPopularFilms()
        {
            if (!PopularFilmsDownloadingStarted())
            {
                DownloadPopularFilms();
            }
        }

        private void GetRecentFilms()
        {
            if (!RecentFilmsDownloadingStarted())
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

        private async Task IncludePosters(Films films, List<Image> posterList, StackPanel imagesWrapper)
        {
            if (films == null || films.results == null) return;

            IUIObjectPrepareWithParameter<Image, Film> imagePreparer = new ImagePreparer();

            foreach (Film film in films.results)
            {
                Image img = await imagePreparer.Prepare(film);
                posterList.Add(img);
                img.MouseDown += Img_MouseDown;
                imagesWrapper.Children.Add(img);
            }
        }

        private bool PopularFilmsDownloadingStarted()
        {
            if (!_popularFilmsDownloadingStarted)
            {
                _popularFilmsDownloadingStarted = true;
                return false;
            }

            return true;
        }

        private bool RecentFilmsDownloadingStarted()
        {
            if (!_recentFilmsDownloadingStarted)
            {
                _recentFilmsDownloadingStarted = true;
                return false;
            }

            return true;
        }

        private void recentFilmsTab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            popularImages.Visibility = Visibility.Collapsed;
            recentImages.Visibility = Visibility.Visible;
            topFilmUnderLine.Visibility = Visibility.Collapsed;
            recentFilmUnderLine.Visibility = Visibility.Visible;
            GetRecentFilms();
        }

        private void topFilmsTab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            recentImages.Visibility = Visibility.Collapsed;
            popularImages.Visibility = Visibility.Visible;
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