using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GalleryApp.UserControls
{
    public partial class CategoryButton : UserControl, INotifyPropertyChanged
    {
        private Image icon;
        private string imageSource;
        private string normalImageSource;
        private string activeImageSource;

        public CategoryButton()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += CategoryButton_Loaded;
        }

        private void CategoryButton_Loaded(object sender, RoutedEventArgs e)
        {
            var button = (Button)Content;
            icon = (Image)button.Template.FindName("imageIcon", button);
        }

        public event RoutedEventHandler ButtonClick;
        public event PropertyChangedEventHandler? PropertyChanged;

        public string ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                OnPropertyChanged();
            }
        }

        public string NormalImageSource
        {
            get { return normalImageSource; }
            set
            {
                normalImageSource = value;
                ImageSource = value;
            }
        }

        public string ActiveImageSource
        {
            get { return activeImageSource; }
            set
            {
                activeImageSource = value;
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick?.Invoke(this, e);
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {

            ImageSource = ActiveImageSource;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ImageSource = NormalImageSource;
        }
    }
}
