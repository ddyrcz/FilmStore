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
            filmList.ItemsSource = _data;
        }

        private void GetPopularFilms()
        {
            //IHttpGet<>
        }
    }
}
