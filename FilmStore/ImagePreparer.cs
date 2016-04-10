using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FilmStore
{
    class ImagePreparer : IUIObjectPrepareWithParameter<Image, Film>
    {
        public async Task<Image> Prepare(Film film)
        {
            var img = new Image();

            IImageDownload downloader = new ImageDownloader();
            byte[] bytes = await downloader.Download("t/p/w500" + film.poster_path);

            img.Stretch = System.Windows.Media.Stretch.Fill;
            img.Width = 120;
            img.Height = 180;            
            img.Margin = new Thickness(3);
            img.Source = Converter.FromBytesToBitmapImage(bytes);
            img.Tag = film;

            return img;
        }
    }
}
