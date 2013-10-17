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

namespace Gym5
{
    public partial class TestPage2 : PhoneApplicationPage
    {
        public TestPage2()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Profile profile = DataAccess.GetProfile();
            Button1.Content = profile.Name;

            DataAccess.EditProfile(TextBox1.Text,60);

        }

        private void ButtonAddWeight_Click(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            DataAccess.AddWeightEntry(now, float.Parse(TextBoxWeight.Text));
        }

        private void ButtonNewRoutine_Click(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime end = DateTime.Now;
            end.AddMonths(1);
            DataAccess.AddRoutine(TextBoxRoutine.Text, now, end);
        }
    }
}