using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace GalleryApp.UserControls
{
    /// <summary>
    /// Interaction logic for ImportAndSelectAll.xaml
    /// </summary>
    public partial class ImportAndSelectAll : UserControl, INotifyPropertyChanged
    {
        public ImportAndSelectAll()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event RoutedEventHandler ButtonClick;

        public event PropertyChangedEventHandler? PropertyChanged;

        private string title;

        private int myFontSize;

        private string imageSource;

        public string ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                OnPropertyChanged();
            }
        }

        public int MyFontSize
        {
            get { return myFontSize; }
            set
            {
                myFontSize = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged();
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
    }
}
