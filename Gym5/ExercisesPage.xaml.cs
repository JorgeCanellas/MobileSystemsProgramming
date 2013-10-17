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
using System.Collections.ObjectModel;
using Gym5.DataObjects;
using Microsoft.Phone.Shell;

namespace Gym5
{
    public partial class ExercisesPage : PhoneApplicationPage
    {
        public static bool edit = false;

        private ObservableCollection<Exercise> exercises;

        private bool kindOfExercise = true; //SQL QUERY  CARDIO=TRUE, LIFTING=FALSE        

        //ObservableCollection<Exercise> listOfExercises;

        public ExercisesPage()
        {
            InitializeComponent();
            //listOfExercises = new ObservableCollection<Exercise>();
            //listBox.DataContext = listOfExercises;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);            
            if (Data.Instance.currentRoutine == null) return;
            exercises = DataAccess.GetRoutineExercises(Data.Instance.currentRoutine.Id);
            listBox.ItemsSource = exercises;

            PageTitle.Text = Data.Instance.currentRoutine.Name;

            //edit = false;
            //foreach (Exercise exercise in exercises)
            //{
            //    exercise.NotifyPropertyChanged("EditRoutine");
            //}
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                string parameter = listBox.SelectedIndex.ToString();
                NavigationService.Navigate(new Uri(string.Format("/PivotTraining.xaml?parameter={0}", parameter), UriKind.Relative));                 
            }
            //if (kindOfExercise)
            //{
            //    NavigationService.Navigate(new Uri("/CardioTrainingPage.xaml", UriKind.Relative));
            //}
            //else
            //{
            //    NavigationService.Navigate(new Uri("/LiftTrainingPage.xaml", UriKind.Relative));
            //}
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AllExercisesPage.xaml", UriKind.Relative));
        }

        private void ApplicationBarIconButton_Click_3(object sender, EventArgs e)
        {
            // Remove exercises
            ApplicationBarIconButton btn = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
            // edit
            edit = !edit;
            if (edit)
                btn.IconUri = new Uri("/Images/check.png", UriKind.Relative);
            else
                btn.IconUri = new Uri("/Images/appbar.delete.rest.png", UriKind.Relative);
            

            foreach (Exercise exercise in exercises)
            {
                exercise.NotifyPropertyChanged("EditRoutine");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Remove exercise from routine
            int exerciseId = (int)((Button)sender).Tag;
            DataAccess.RemoveRoutineExercise(exerciseId, Data.Instance.currentRoutine.Id);
            exercises = DataAccess.GetRoutineExercises(Data.Instance.currentRoutine.Id);
            listBox.ItemsSource = exercises;
        }
    }

}