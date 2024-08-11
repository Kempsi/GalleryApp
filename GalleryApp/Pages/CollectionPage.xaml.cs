using GalleryApp.Entitys;
using GalleryApp.UserControls;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GalleryApp.Pages
{
    /// <summary>
    /// Interaction logic for CollectionPage.xaml
    /// </summary>
    public partial class CollectionPage : Page
    {
        public ObservableCollection<MyImage> Images { get; set; }
        public ObservableCollection<MyImage> SelectedImages { get; set; }

        public CollectionPage()
        {
            InitializeComponent();

            Images = new ObservableCollection<MyImage>();
            SelectedImages = new ObservableCollection<MyImage>();

            DataContext = this;
            LoadImages(300,300);

        }



        // Kun kun valitsee, niin se lisätään SelectedImages - kokoelmaan
        private void OnImageSelected(object sender, ClickableImage.ImageSelectedEventArgs e)
        {
            var selectedImage = Images.FirstOrDefault(img => img.Image == e.Image);
            if (selectedImage != null && !SelectedImages.Contains(selectedImage))
            {
                SelectedImages.Add(selectedImage);
            }
        }

        // Kun kun valitsee ja se on jo SelectedImages - kokoelmassa, niin se poistetaan sieltä
        private void OnImageDeselected(object sender, ClickableImage.ImageDeselectedEventArgs e)
        {
            var deselectedImage = Images.FirstOrDefault(img => img.Image == e.Image);
            if (deselectedImage != null)
            {
                SelectedImages.Remove(deselectedImage);
            }
        }

        // Poistaa valitut kuvat kokoelmasta ja tiedostokansiosta
        public void DeleteSelectedImages()
        {
            foreach (var image in SelectedImages.ToList())
            {
                if (!string.IsNullOrEmpty(image.FilePath) && File.Exists(image.FilePath))
                {
                    File.Delete(image.FilePath);
                }

                Images.Remove(image);
            }

            SelectedImages.Clear();
        }


        public void SelectAll()
        {

        }



        // Lataa kuvat kansiosta
        public void LoadImages(int width, int height)
        {
            string imagesFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            if (Directory.Exists(imagesFolder))
            {
                foreach (string file in Directory.GetFiles(imagesFolder))
                {
                    BitmapImage bitmap = GetImageFromFile(file);

                    AddImage(bitmap, width, height, file);
                }
            }
        }

        // Lisää kuvan tiedostopolun perusteella paikalliseen kokoelmaan
        private void AddImage(BitmapImage bitmap, int width, int height, string filePath)
        {
            Images.Add(new MyImage
            {
                Image = bitmap,
                Width = width,
                Height = height,
                FilePath = filePath

            });

        }



        // Hakee kuvan tiedostosta ja palauttaa BitmapImagena
        private static BitmapImage GetImageFromFile(string file)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(file);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            return bitmap;
        }

    }
}
