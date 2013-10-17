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
using System.Collections.ObjectModel;

namespace Gym5
{
    public partial class Routines : PhoneApplicationPage
    {
        public Routines()
        {
            InitializeComponent();
        }

        private void NewRoutineButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/CreateRoutinePage.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ObservableCollection<Routine> routines = DataAccess.GetRoutines();
            listBoxRoutines.Items.Clear();
            foreach (Routine routine in routines)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = routine.Name;
                item.Tag = routine;
                listBoxRoutines.Items.Add(item);
            }
        }

        private void listBoxRoutines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
        //    if (listBox1.SelectedIndex == -1)
          //      return;

      
            // Navigate to the new page

            if (listBoxRoutines.SelectedItem != null)
            {
                Data.Instance.currentRoutine = (Routine)(listBoxRoutines.SelectedItem as ListBoxItem).Tag;

                NavigationService.Navigate(new Uri("/ExercisesPage.xaml", UriKind.Relative));
            }
   
        }
    }
}