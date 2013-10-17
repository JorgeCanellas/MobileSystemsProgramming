using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Gym5
{
    public partial class CreateRoutinePage : PhoneApplicationPage
    {
        private string icon;

        public CreateRoutinePage()
        {
            InitializeComponent();

            foreach (UIElement element in IconsPanel.Children)
            {
                Button button = (Button)element;
                Image image = (Image)button.Content;
                BitmapImage bitmapImage = new BitmapImage(new Uri(button.Tag.ToString(), UriKind.Relative));
                image.Source = bitmapImage;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            SelectIcon((Button)IconsPanel.Children.First());
        }

        private void SelectIcon(Button button)
        {
            foreach (UIElement element in IconsPanel.Children)
            {
                Button button2 = (Button)element;
                button2.Margin = new Thickness(0, 0, 12, 0);
                button2.BorderBrush = new SolidColorBrush(Colors.Black);
                Image image2 = (Image)button2.Content;
                image2.Width = 125;
                image2.Height = 115;
            }
            
            button.Margin = new Thickness(0, 0, 2, 0);
            button.BorderBrush = new SolidColorBrush(Colors.White);
            Image image = (Image)button.Content;
            image.Width = 135;
            image.Height = 125;

            icon = button.Tag.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(RoutineName.Text))
            {
                MessageBox.Show("Invalid name.");
                return;
            }

            Data.Instance.currentRoutine = DataAccess.AddRoutine(RoutineName.Text, DateTime.Today, DateTime.Today.AddMonths(1), icon);
            NavigationService.Navigate(new Uri("/ExercisesPage.xaml", UriKind.Relative));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // select icon
            SelectIcon((Button)sender);
        }
    }
}