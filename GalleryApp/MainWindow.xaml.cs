using GalleryApp.UserControls;
using System.Windows;
using System.Windows.Controls;
using GalleryApp.Pages;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;
using GalleryApp.Entitys;

namespace GalleryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Category categoryCollection;
        private Category categoryFavourites;
        private Category categoryRecents;

        private List<LayoutButton> layoutButtons;
        private LayoutButton selectedLayoutButton;

        private List<SideButton> sideButtons;
        private SideButton selectedSideButton;

        private bool isSidePanelCollapsed = false;

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.NavigationService.Navigate(new CollectionPage());
            SetSelectedButtons();
            CreateButtonLists();
            GetEventHandlers();
            DelayAndSetButtonStates();

            

        }

        #region Layout- and sidebutton state - methods

        // Viivästyttää SetLayoutStates - metodin kutsua kunnes UI on päivitetty
        private void DelayAndSetButtonStates()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                SetLayoutStates(selectedLayoutButton);
                SetSideButtonStates(selectedSideButton);
            }), System.Windows.Threading.DispatcherPriority.Loaded);

        }

        // Asettaa valituiksi napeiksi ensimmäiset napit
        private void SetSelectedButtons()
        {
            selectedLayoutButton = layoutButton1;
            selectedSideButton = btn_Collection;
        }

        // Luo nappilistat
        private void CreateButtonLists()
        {
            layoutButtons = new List<LayoutButton>
            {
                layoutButton1,
                layoutButton2,
                layoutButton3
            };


            sideButtons = new List<SideButton>
            {
                btn_Collection,
                btn_Favourites,
                btn_Recents
            };
        }

        // Asettaa Layout - nappien tilan
        private void SetLayoutStates(LayoutButton selButton)
        {
            foreach (var button in layoutButtons)
            {
                if (button == selButton)
                {
                    button.SelectedRectOpacity = 100;
                }

                else
                {
                    button.SelectedRectOpacity = 0;
                }
            }
        }

        // Asettaa Side - nappien tilan
        private void SetSideButtonStates(SideButton selButton)
        {
            foreach (var button in sideButtons)
            {
                if (button == selButton)
                {
                    button.SelectedRectOpacity = 100;
                }

                else
                {
                    button.SelectedRectOpacity = 0;
                }
            }
        }

        // Liittää tapahtumien käsittelijät napeille
        private void GetEventHandlers()
        {
            foreach (var button in layoutButtons)
            {
                button.ButtonClick += OnLayoutButtonClick;
            }

            foreach (var button in sideButtons)
            {
                button.ButtonClick += OnSideButtonClick;
            }

            cb_CategoryButton.ButtonClick += OnCategoryButtonClick;
            cb_CollapsedCategoryButton.ButtonClick += OnCollapsedCategoryButtonClick;
        }

        private void OnCollapsedCategoryButtonClick(object sender, RoutedEventArgs e)
        {
            if (sp_CollapsedCategories.Visibility == Visibility.Collapsed)
            {
                sp_MainCategories.Visibility = Visibility.Collapsed;
                sp_CollapsedCategories.Visibility = Visibility.Visible;
                Grid.SetColumn(MainFrame, 0);
                Grid.SetColumnSpan(MainFrame, 2);
                MainFrame.HorizontalAlignment = HorizontalAlignment.Stretch;

                Grid.SetColumn(sp_LayoutButtons, 0);
                Grid.SetColumnSpan(sp_LayoutButtons, 2);
                sp_LayoutButtons.Margin = new Thickness(50, 0, 0, 0);
            }

            else if (sp_MainCategories.Visibility == Visibility.Collapsed)
            {
                sp_CollapsedCategories.Visibility = Visibility.Collapsed;
                sp_MainCategories.Visibility = Visibility.Visible;
                Grid.SetColumn(MainFrame, 1);
                Grid.SetColumnSpan(MainFrame, 1);
                MainFrame.HorizontalAlignment = HorizontalAlignment.Stretch;

                Grid.SetColumn(sp_LayoutButtons, 1);
                Grid.SetColumnSpan(sp_LayoutButtons, 1);
                sp_LayoutButtons.Margin = new Thickness(0, 0, 0, 0);

            }


        }

        private void OnCategoryButtonClick(object sender, RoutedEventArgs e)
        {
            if (sp_CollapsedCategories.Visibility == Visibility.Collapsed)
            {
                sp_MainCategories.Visibility = Visibility.Collapsed;
                sp_CollapsedCategories.Visibility = Visibility.Visible;
                Grid.SetColumn(MainFrame, 0);
                Grid.SetColumnSpan(MainFrame, 2);
                MainFrame.HorizontalAlignment = HorizontalAlignment.Stretch;

                Grid.SetColumn(sp_LayoutButtons, 0);
                Grid.SetColumnSpan(sp_LayoutButtons, 2);
                sp_LayoutButtons.Margin = new Thickness(50, 0, 0, 0);
            }

            else if (sp_MainCategories.Visibility == Visibility.Collapsed)
            {
                sp_CollapsedCategories.Visibility = Visibility.Collapsed;
                sp_MainCategories.Visibility = Visibility.Visible;
                Grid.SetColumn(MainFrame, 1);
                Grid.SetColumnSpan(MainFrame, 1);
                MainFrame.HorizontalAlignment = HorizontalAlignment.Stretch;

                Grid.SetColumn(sp_LayoutButtons, 1);
                Grid.SetColumnSpan(sp_LayoutButtons, 1);
                sp_LayoutButtons.Margin = new Thickness(0, 0, 0, 0);

            }



        }

        // Klikatessa Layout - nappia, asettaa sen ja muiden valitun tilan
        private void OnLayoutButtonClick(object sender, RoutedEventArgs e)
        {
            var clickedButton = sender as LayoutButton;

            if (clickedButton != null)
            {
                SetLayoutStates(clickedButton);
            }

        }

        // Klikatessa Side - nappia, asettaa sen ja muiden valitun tilan
        private void OnSideButtonClick(object sender, RoutedEventArgs e)
        {
            var clickedButton = sender as SideButton;

            if (clickedButton != null)
            {
                SetSideButtonStates(clickedButton);
            }

        }

        #endregion Layout- and sidebutton state - methods

        #region Window state controls
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            ToggleWindowState();

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void doubleClickArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                ToggleWindowState();
            }
        }

        private void ToggleWindowState()
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }

            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        #endregion Window state controls

        #region Navigation buttons
        private void btn_Collection_ButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new CollectionPage());
        }

        private void btn_Favourites_ButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new FavouritesPage());
        }

        private void btn_Recents_ButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new RecentsPage());
        }

        #endregion Navigation buttons


        //private void CreateCategorys()
        //{
        //    categoryCollection = new Category();
        //    categoryFavourites = new Category();
        //    categoryRecents = new Category();

        //    CollectionPage categoryCollectionPage = new CollectionPage();
        //    categoryCollection.Name = "Collection";
        //    categoryCollection.Images = categoryCollectionPage.Images;
        //}

        private void Import_ButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }

                foreach (string filename in openFileDialog.FileNames)
                {
                    string destFile = Path.Combine(imagesFolder, Path.GetFileName(filename));
                    File.Copy(filename, destFile, true);
                }

                if (MainFrame.Content is CollectionPage collectionPage)
                {
                    collectionPage.Images.Clear();
                    collectionPage.LoadImages(300,300);
                }
            }

        }

        private void Layout1_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content is CollectionPage collectionPage)
            {
                collectionPage.Images.Clear();
                collectionPage.LoadImages(300,300);
            }
        }

        private void Layout2_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content is CollectionPage collectionPage)
            {
                collectionPage.Images.Clear();
                collectionPage.LoadImages(450, 450);
            }
        }

        private void Layout3_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content is CollectionPage collectionPage)
            {
                collectionPage.Images.Clear();
                collectionPage.LoadImages(105, 105);
            }
        }

        private void DeleteSelected_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content is CollectionPage collectionPage)
            {
                if (collectionPage.SelectedImages.Count > 0)
                {
                    var answer = MessageBox.Show("Delete selected images?", "Delete images", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (answer == MessageBoxResult.Yes)
                    {
                        collectionPage.DeleteSelectedImages();
                    }
                }
     
            }

        }



        private void SelectAll_ButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}