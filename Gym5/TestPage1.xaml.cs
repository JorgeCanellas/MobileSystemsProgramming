using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Text;
using Gym5.DataObjects;

namespace Gym5
{
    public partial class TestPage1 : PhoneApplicationPage
    {
        public TestPage1()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Define query to gather all of the to-do items.
            var machineItemsInDB = from Machine machine in Data.Instance.db.MachineItems
                                select machine;

            // Execute query and place results into a collection.
            Data.Instance.MachineItems = new ObservableCollection<Machine>(machineItemsInDB);

            StringBuilder sb = new StringBuilder();
            foreach (Machine machine in Data.Instance.MachineItems)
            {
                sb.AppendLine("Id: " + machine.Id + ", name: " + machine.Name + ", type: " + machine.Type);
            }

            TextBlock1.Text = sb.ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // Create a new to-do item based on text box.
            Machine newMachine = new Machine { Name = "Machine" + new Random().Next(), Type = MachineType.Cardio };

            // Add the to-do item to the observable collection.
            Data.Instance.MachineItems.Add(newMachine);

            // Add the to-do item to the local database.
            Data.Instance.db.MachineItems.InsertOnSubmit(newMachine);

            //Save changes to the database
            Data.Instance.db.SubmitChanges();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Define query to gather all of the to-do items.
            var machineItemsInDB = from Machine machine in Data.Instance.db.MachineItems
                                   select machine;

            // Execute query and place results into a collection.
            Data.Instance.MachineItems = new ObservableCollection<Machine>(machineItemsInDB);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            //Save changes to the database
            Data.Instance.db.SubmitChanges();
        }
    }
}