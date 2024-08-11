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
using System.Windows.Shapes;

namespace GalleryApp.Windows
{
    /// <summary>
    /// Interaction logic for DisplayImageWindow.xaml
    /// </summary>
    public partial class DisplayImageWindow : Window
    {
        public DisplayImageWindow(BitmapImage image)
        {
            InitializeComponent();
            LargeImage.Source = image;
        }
    }
}
