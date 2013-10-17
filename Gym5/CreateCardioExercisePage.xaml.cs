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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Gym5
{
    public partial class CreateExercisePage : PhoneApplicationPage
    {
        private Machine currentMachine = null;
        private string icon;

        public CreateExercisePage()
        {
            InitializeComponent();

            InputScope inputScope = new InputScope()
            {
                Names = { new InputScopeName() { NameValue = InputScopeNameValue.Number } }
            };

            time.InputScope = inputScope;
            resistance.InputScope = inputScope;
            sets.InputScope = inputScope;
            reps.InputScope = inputScope;
            weight.InputScope = inputScope;

            foreach (UIElement element in IconsPanel.Children)
            {
                Button button = (Button)element;
                Image image = (Image)button.Content;
                BitmapImage bitmapImage = new BitmapImage(new Uri(button.Tag.ToString(), UriKind.Relative));
                image.Source = bitmapImage;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ObservableCollection<Machine> machines = DataAccess.GetMachines();
            //LongListSelector1.DataContext = machines;
            listBoxMachines.ItemsSource = machines;
            currentMachine = machines.First();

            SelectIcon((Button)IconsPanel.Children.First());

            listBoxMachines.SelectedIndex = 0;
        }

        private void listBoxRoutines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // select machine
            currentMachine = (Machine)listBoxMachines.SelectedItem;
            if (currentMachine == null) { return; }
            if (currentMachine.Type == MachineType.Cardio)
            {
                cardio.Visibility = Visibility.Visible;
                lift.Visibility = Visibility.Collapsed;
                //cardioName.Text = currentMachine.Name;
            }
            else
            {
                cardio.Visibility = Visibility.Collapsed;
                lift.Visibility = Visibility.Visible;
                //liftName.Text = currentMachine.Name;
            }
        }

        private void CreateExerciseButton(object sender, RoutedEventArgs e)
        {
            int time;
            int resistance;
            int sets;
            int reps;
            int weight;

            if (currentMachine.Type == MachineType.Cardio)
            {
                if (!Int32.TryParse(this.time.Text, out time))
                {
                    MessageBox.Show("Invalid time.");
                    return;
                }
                if (!Int32.TryParse(this.resistance.Text, out resistance))
                {
                    MessageBox.Show("Invalid resistance.");
                    return;
                }
                DataAccess.AddCardioExercise(time, resistance, currentMachine.Id, icon);
            }
            else
            {
                if (!Int32.TryParse(this.sets.Text, out sets))
                {
                    MessageBox.Show("Invalid sets.");
                    return;
                }
                if (!Int32.TryParse(this.reps.Text, out reps))
                {
                    MessageBox.Show("Invalid reps.");
                    return;
                }
                if (!Int32.TryParse(this.weight.Text, out weight))
                {
                    MessageBox.Show("Invalid weight.");
                    return;
                }
                DataAccess.AddLiftingExercise(sets, reps, weight, currentMachine.Id, icon);
            }

            //NavigationService.Navigate(new Uri("/AllExercisesPage.xaml", UriKind.Relative));
            NavigationService.GoBack();
        }

        private void SelectIcon(Button button)
        {
            foreach (UIElement element in IconsPanel.Children)
            {
                Button button2 = (Button)element;
                button2.Margin = new Thickness(0, 0, 12, 0);
                button2.BorderBrush = new SolidColorBrush(Colors.Black);
                Image image2 = (Image)button2.Content;
                image2.Width = 125;
                image2.Height = 115;
            }

            button.Margin = new Thickness(0, 0, 2, 0);
            button.BorderBrush = new SolidColorBrush(Colors.White);
            Image image = (Image)button.Content;
            image.Width = 135;
            image.Height = 125;

            icon = button.Tag.ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // select icon
            SelectIcon((Button)sender);
        }
    }
}