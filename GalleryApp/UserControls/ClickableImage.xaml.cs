using GalleryApp.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GalleryApp.UserControls
{
    public partial class ClickableImage : UserControl
    {
        public static readonly DependencyProperty ImageProperty =
                    DependencyProperty.Register("Image", typeof(BitmapImage), typeof(ClickableImage), new PropertyMetadata(null));
        public bool IsSelected { get; private set; }

        public BitmapImage Image
        {
            get { return (BitmapImage)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(double), typeof(ClickableImage), new PropertyMetadata(300.0));

        public double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.Register("Height", typeof(double), typeof(ClickableImage), new PropertyMetadata(300.0));

        public double Height
        {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }


        public event EventHandler<ImageSelectedEventArgs> ImageSelected;
        public event EventHandler<ImageDeselectedEventArgs> ImageDeselected;

        protected virtual void OnImageSelected()
        {
            ImageSelected?.Invoke(this, new ImageSelectedEventArgs { Image = this.Image });
        }

        protected virtual void OnImageDeselected()
        {
            ImageDeselected?.Invoke(this, new ImageDeselectedEventArgs { Image = this.Image });
        }

        public class ImageSelectedEventArgs : EventArgs
        {
            public BitmapImage Image { get; set; }
        }

        public class ImageDeselectedEventArgs : EventArgs
        {
            public BitmapImage Image { get; set; }
        }



        public ClickableImage()
        {
            InitializeComponent();

        }


        public void Select()
        {
            var button = (Button)Content;
            var border = (Border)button.Template.FindName("border", button);
            border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0B6CCF"));
            IsSelected = true;
        }


        public void Deselect()
        {
            var button = (Button)Content;
            var border = (Border)button.Template.FindName("border", button);
            border.BorderBrush = Brushes.Transparent;
            IsSelected = false;
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (IsSelected)
            {
                Deselect();
                OnImageDeselected();
            }

            else
            {
                Select();
                OnImageSelected();
            }

        }

        private void Button_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var window = new DisplayImageWindow(this.Image);
            window.ShowDialog();
        }
    }
}
