using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Gym5
{
    public partial class Page3 : PhoneApplicationPage
    {
        public Page3()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            int reps = int.Parse(TextBoxReps.Text);
            int sets = int.Parse(TextBoxSets.Text);
            int weight = int.Parse(TextBoxWeight.Text);
            //DataAccess.AddExercise(sets, reps, weight,1, 1, DayOfWeek.Monday);

        }
    }
}