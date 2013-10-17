using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Gym5.DataObjects;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace Gym5
{
    public partial class MainPage : PhoneApplicationPage
    {
        private ObservableCollection<Routine> routinesList;
        public static bool edit = false;

        // Constructor
        public MainPage()
        {
            Data.Initialize();

            InitializeComponent();

            ApplicationBar = (ApplicationBar)Resources["1"]; 
        }

        private void NewRoutineButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/CreateRoutinePage.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Profile profile = DataAccess.GetProfile();
            textBlock2.Text = "Hi " + profile.Name + "!";
            textBlock4.Text = "Calories burned: \n" + profile.BurntCalories + " Kcal";
            textBlock5.Text = "Since: " + profile.CreatedTime.ToShortDateString();

            routinesList = DataAccess.GetRoutines();
            listBoxRoutines.ItemsSource = routinesList;

            //edit = false;
            //foreach (Routine routine in routinesList)
            //{
            //    routine.NotifyPropertyChanged("Edit");
            //}
        }        

        private void listBoxRoutines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (listBoxRoutines.SelectedItem != null)
            {

                Data.Instance.currentRoutine = (Routine)(listBoxRoutines.SelectedItem);

                NavigationService.Navigate(new Uri("/ExercisesPage.xaml", UriKind.Relative));
            }

        }

        private void dayRecord_Click(object sender, RoutedEventArgs e)
        {            
            string parameter = "day";
            NavigationService.Navigate(new Uri(string.Format("/RecordsPage.xaml?parameter={0}", parameter), UriKind.Relative)); 
        }


        private void weekRecord_Click(object sender, RoutedEventArgs e)
        {
            string parameter = "week";
            NavigationService.Navigate(new Uri(string.Format("/RecordsPage.xaml?parameter={0}", parameter), UriKind.Relative)); 
        }

        private void monthRecord_Click(object sender, RoutedEventArgs e)
        {
            string parameter = "month";
            NavigationService.Navigate(new Uri(string.Format("/RecordsPage.xaml?parameter={0}", parameter), UriKind.Relative)); 
        }

        private void routineRecord_Click(object sender, RoutedEventArgs e)
        {
            string parameter = "routine";
            NavigationService.Navigate(new Uri(string.Format("/RecordsPage.xaml?parameter={0}", parameter), UriKind.Relative));
        }

        private void mainPanorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string panonResource;

            switch (mainPanorama.SelectedIndex)
            {
                case 0:
                    panonResource = "1";
                    break;

                case 1:
                    panonResource = "2";
                    break;

                case 2:
                    panonResource = "3";
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            ApplicationBar = (ApplicationBar)Resources[panonResource]; 
        }

        private void ApplicationBarMenuItem_Click_1(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            ApplicationBarIconButton btn = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
            // edit
            edit = !edit;
            if (edit)
                btn.IconUri = new Uri("/Images/check.png", UriKind.Relative);
            else
                btn.IconUri = new Uri("/Images/appbar.delete.rest.png", UriKind.Relative);
            
            
            foreach (Routine routine in routinesList)
            {
                routine.NotifyPropertyChanged("Edit");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).Tag;
            foreach (Routine routine in routinesList)
            {
                if (routine.Id == id)
                {
                    routine.Removed = true;
                    Data.Instance.db.SubmitChanges();
                    break;
                }
            }

            routinesList = DataAccess.GetRoutines();
            listBoxRoutines.ItemsSource = routinesList;
        }
    }
}