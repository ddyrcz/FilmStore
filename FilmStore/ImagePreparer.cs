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
    class ImagePreparer : IUIObjectPrepareWithParameter<Image, BitmapImage>
    {
        public Image Prepare(BitmapImage obj)
        {
            var img = new Image();

            img.Width = 250;
            img.Height = 250;
            img.Margin = new Thickness(0, 15, 0, 15);
            img.Source = obj;

            return img;
        }
    }
}
