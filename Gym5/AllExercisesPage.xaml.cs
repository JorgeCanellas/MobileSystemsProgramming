using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Gym5.DataObjects;
using System.Collections.ObjectModel;

namespace Gym5
{
    public partial class AllExercisesPage : PhoneApplicationPage
    {
        public static bool edit = false;

        private ObservableCollection<Exercise> exercises;

        public AllExercisesPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            exercises = DataAccess.GetExercises();
            listBoxExercises.ItemsSource = exercises;

            edit = false;
            foreach (Exercise exercise in exercises)
            {
                exercise.NotifyPropertyChanged("EditAll");
            }
        }

        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {
            // Both cardio AND lift exercises are created on the same page
            NavigationService.Navigate(new Uri("/CreateCardioExercisePage.xaml", UriKind.Relative));
        }

        private void listBoxRoutines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxExercises.SelectedItem != null)
            {
                Exercise exercise = (Exercise)listBoxExercises.SelectedItem;
                DataAccess.AddRoutineExercise(exercise.Id, Data.Instance.currentRoutine.Id);
                //NavigationService.Navigate(new Uri("/ExercisesPage.xaml", UriKind.Relative));
                NavigationService.GoBack();
            }
        }

        private void ApplicationBarIconButton_Click_3(object sender, EventArgs e)
        {
            // Remove mode
            ApplicationBarIconButton btn = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
            // edit
            edit = !edit;
            if (edit)
                btn.IconUri = new Uri("/Images/check.png", UriKind.Relative);
            else
                btn.IconUri = new Uri("/Images/appbar.delete.rest.png", UriKind.Relative);
            
            foreach (Exercise exercise in exercises)
            {
                exercise.NotifyPropertyChanged("EditAll");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Remove exercise
            int id = (int)((Button)sender).Tag;
            foreach (Exercise exercise in exercises)
            {
                if (exercise.Id == id)
                {
                    exercise.Removed = true;
                    Data.Instance.db.SubmitChanges();
                    break;
                }
            }

            exercises = DataAccess.GetExercises();
            listBoxExercises.ItemsSource = exercises;
        }
    }
}