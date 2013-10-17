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

namespace Gym5
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            // load previous values to the text boxes
            Profile profile = DataAccess.GetProfile();
            if (!String.IsNullOrEmpty(profile.Name))
            {
                NameBox.Text = profile.Name;
            }

            //RestTimeBox.Text = profile.RestTime.ToString();

            Weight weight = DataAccess.GetLastWeightEntry();
            if (weight != null)
            {
                WeightBox.Text = weight.WeightValue.ToString();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // reset calories
            DataAccess.ResetCalories();
            MessageBox.Show("Calories reseted.");
            NavigationService.GoBack();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // apply all settings

            // update name
            // update rest time
            string name = NameBox.Text;
            int restTime = 0;
            float weight;

            if (name.Length == 0)
            {
                MessageBox.Show("Invalid name.");
                return;
            }

            //if (!Int32.TryParse(RestTimeBox.Text, out restTime))
            //{
            //    MessageBox.Show("Invalid rest time.");
            //    return;
            //}

            if (!float.TryParse(WeightBox.Text, out weight))
            {
                MessageBox.Show("Invalid weight.");
                return;
            }

            DataAccess.EditProfile(name, restTime);
            DataAccess.AddWeightEntry(DateTime.Now, weight);
            MessageBox.Show("Settings applied.");
            NavigationService.GoBack();
        }
    }
}