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
    public partial class Page2 : PhoneApplicationPage
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //MachineType type = (ListPikerType.SelectedItem.ToString().Equals(MachineType.Cardio) ? MachineType.Cardio : MachineType.Lifting);

            //DataAccess.AddMachine(type, TextBoxMachineName.Text);

        }
    }
}