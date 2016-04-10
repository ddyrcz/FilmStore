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
            
            // In MVVM patern would not be a await key 
            await IncludePosters(popularFilms);                        
        }

        private async void GetRecentFilms()
        {
            IHttpGet adapter = new HttpAdapter();

            Films recentFilms = await adapter.Get<Films>(string.Format("3/movie/now_playing?api_key={0}", _apiKey));
            
            // In MVVM patern would not be a await key 
            await IncludePosters(recentFilms);
        }

        private async Task IncludePosters(Films popularFilms)
        {
            if (popularFilms == null || popularFilms.results == null) return;
            
            IUIObjectPrepareWithParameter<Image, Film> imagePreparer = new ImagePreparer();

            images.Children.Clear();

            foreach (Film film in popularFilms.results)
            {                                                
                Image img = await imagePreparer.Prepare(film);                
                img.MouseDown += Img_MouseDown;
                images.Children.Add(img);
            }            
        }

        private void Img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(sender is Image && ((Image)sender).Tag is Film)
            {
                Film film = ((Film)((Image)sender).Tag);                
                image.Source = ((Image)sender).Source;
                title.Text = film.title_with_release_date;
                description.Text = film.overview;
                rate.Text = film.vote_average.ToString();
            }
        }

        private void topFilmsTab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            topFilmUnderLine.Visibility = Visibility.Visible;
            recentFilmUnderLine.Visibility = Visibility.Collapsed;
            GetPopularFilms();
        }

        private void recentFilmsTab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            topFilmUnderLine.Visibility = Visibility.Collapsed;
            recentFilmUnderLine.Visibility = Visibility.Visible;
            GetRecentFilms();
        }
    }
}
