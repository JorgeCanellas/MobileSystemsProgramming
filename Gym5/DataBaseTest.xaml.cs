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
using System.Text;
using System.Collections.ObjectModel;

namespace Gym5
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Checks the profile dataBase (just print all the data in the profile)
            Profile profile = DataAccess.GetProfile();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ID: "+profile.Id);
            sb.AppendLine("Name: "+profile.Name);
            sb.AppendLine("Rest Time: "+ profile.RestTime);
            
            // Define query to gather all of the to-do items.
            var weightItemsInDB = from Weight weight in Data.Instance.db.WeightItems select weight ;
            
            // Execute query and place results into a collection.
            Data.Instance.WeightItems = new ObservableCollection<Weight>(weightItemsInDB);

            sb.AppendLine("Weight Records");
            sb.AppendLine("==============");
            foreach (Weight weight in Data.Instance.WeightItems)
            {
                sb.AppendLine("ID: " + weight.Id + "| date: " + weight.Date + " | weight: " + weight.WeightValue);
            }

           // Last Weight Test
            sb.AppendLine("==============");
            sb.AppendLine("Last Weight Entry:");
            Weight lastWeight = DataAccess.GetLastWeightEntry();
            if (lastWeight == null)
            {
                //There are no weights
                sb.AppendLine("There are no records");
            }
            else
            {
                sb.AppendLine("ID: " + lastWeight.Id + "| date: " + lastWeight.Date + " | weight: " + lastWeight.WeightValue);
            }
            
            // GetWeightEntries
            

            DateTime begin = DateTime.Parse("02/22/2013 00:15:12");
            DateTime end = DateTime.Parse("02/22/2013 2:04:12");
            Data.Instance.WeightItems = DataAccess.GetWeightEntries(begin, end);
            sb.AppendLine("================");
            if (Data.Instance.WeightItems.Count() > 0)
            {
                foreach (Weight weight in Data.Instance.WeightItems)
                {
                    sb.AppendLine("ID: " + weight.Id + "| date: " + weight.Date + " | weight: " + weight.WeightValue);
                }
             
            }
            else
            {
                sb.AppendLine("There are no registers from" + begin.ToLongDateString() + " to " + end.ToLongDateString());
            }
            TextBlock.Text = sb.ToString();
        }

        private void ButtonExercises_Click(object sender, RoutedEventArgs e)
        {
            //Create new machine to ensure that there is at least 1 machine
            Machine machine = DataAccess.AddMachine(MachineType.Lifting, "Hypeeeer", 766);
            // Create new exercises ex1 and ex2 and add them to the dataBase
            Exercise ex1 = DataAccess.AddLiftingExercise(2, 12, 15, machine.Id);
            Exercise ex2 = DataAccess.AddLiftingExercise(3, 18, 10, machine.Id);
            // Create new routine routine1 and add to the dataBase
            Routine routine1 = DataAccess.AddRoutine("routine1", DateTime.Now, DateTime.Now.AddMonths(1));
            // Create new RoutineExercise that links ex1 and ex2 to routine1
            DataAccess.AddRoutineExercise(ex1.Id, routine1.Id);
            DataAccess.AddRoutineExercise(ex2.Id, routine1.Id);
            // Check if it works by printing exercises names that belong to routine1
            Data.Instance.ExerciseItems = DataAccess.GetRoutineExercises(routine1.Id);
            StringBuilder sb = new StringBuilder();
            foreach (Exercise exercise in Data.Instance.ExerciseItems)
            {
                sb.AppendLine("ID: " + exercise.Id + " reps:" + exercise.Reps);
            }
            TextBlock.Text = sb.ToString();
           

        }

        private void ButtonRoutines_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            Data.Instance.RoutineItems = DataAccess.GetRoutines();
            foreach (Routine routine in Data.Instance.RoutineItems)
            {
                sb.AppendLine("ID: " + routine.Id + "Name: " + routine.Name);
            }
            TextBlock.Text = sb.ToString();
        }

        private void ButtonMachines_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            Data.Instance.MachineItems = DataAccess.GetMachines();
            foreach (Machine machine in Data.Instance.MachineItems)
            {
                sb.AppendLine("ID: " + machine.Id + " Name: " + machine.Name + " Type: " + machine.Type);
            }
            TextBlock.Text = sb.ToString();
        }
    }
}