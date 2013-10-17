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
using Gym5.DataObjects;
using System.Windows.Threading;
using System.Windows.Media.Imaging;

namespace Gym5
{
    public partial class PivotTraining : PhoneApplicationPage
    {
        private ObservableCollection<Exercise> exercises;
        private Exercise exercise;
        private CardioTrainingPage cardioPage;
        private LiftTrainingPage liftPage;
        private PivotItem currentPivotItem;
        private int exerciseIndex = 0;
        private string secondsString;
        private DispatcherTimer setTimer = new DispatcherTimer();
        private TimeSpan setTimerRemaining = TimeSpan.FromSeconds(0d);
        private TimeSpan setTimerInterval = TimeSpan.FromSeconds(1d);
        private bool isSetTimerStopped = false;
        private bool isSetTimerRunning = false;
        private bool end = false;
        string newparameter;
        int i;

        public PivotTraining()
        {
            InitializeComponent();
            setTimer.Interval = setTimerInterval;
            setTimer.Tick += new EventHandler(setTimerTick);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            newparameter = this.NavigationContext.QueryString["parameter"];
        }

        private void MainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshCurrentPivotItem();
        }

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            exercises = DataAccess.GetRoutineExercises(Data.Instance.currentRoutine.Id);

            foreach (Exercise ex in exercises)
            {
                PivotItem newPiv = new PivotItem();
                newPiv.Header = ex.ExerciseName;
                newPiv.Tag = Convert.ToString(i);
                i++;
                if (ex.Machine.Type == MachineType.Cardio)
                {
                    cardioPage = new CardioTrainingPage();
                    cardioPage.txt_Time.Text = ex.Time.ToString();
                    cardioPage.txt_level.Text = ex.Reps_Level.ToString();
                    BitmapImage bitmapImage = new BitmapImage(new Uri(ex.Icon, UriKind.Relative));
                    cardioPage.image1.Source = bitmapImage;
                    cardioPage.setTimerButton.Click += new RoutedEventHandler(setTimerButton_Click);
                    cardioPage.setTimerResetButton.Click += new RoutedEventHandler(setTimerResetButton_Click);
                    cardioPage.skipButton.Click += new RoutedEventHandler(skipButton_Click);  
                    cardioPage.image1.DataContext = ex;
                    newPiv.Content = cardioPage;
                }
                else
                {
                    liftPage = new LiftTrainingPage();
                    liftPage.txt_Reps.Text = ex.Reps.ToString();
                    liftPage.txt_Sets.Text = ex.Sets.ToString();
                    liftPage.txt_Weight.Text = ex.Weight.ToString();
                    BitmapImage bitmapImage = new BitmapImage(new Uri(ex.Icon, UriKind.Relative));
                    liftPage.image1.Source = bitmapImage;
                    liftPage.skipButton.Click += new RoutedEventHandler(skipButton_Click);                    
                    newPiv.Content = liftPage;
                }

                MainPivot.Items.Add(newPiv);
            }
            MainPivot.SelectedIndex = int.Parse(newparameter);
            startExercise();            
        }

        private void startExercise()
        {
            if (exerciseIndex == exercises.Count)
            {
                return;
            }
            //exercise = exercises[exerciseIndex++];                                    
        }

        private void setTimerButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshCurrentPivotItem();
            cardioPage.canvas1.Visibility = Visibility.Visible;
            if (end)
            {
                end = false;
                cardioPage.setTimerButton.Content = "Start";
                isSetTimerRunning = true;

                if (MainPivot.Items.Count == 1)
                {
                    MessageBox.Show("Congratulations! You have finished");
                    GoToMainPage();
                }
                else
                {
                    PivotItem removedPiv = new PivotItem();
                    removedPiv = MainPivot.SelectedItem as PivotItem;
                    MainPivot.Items.Remove(removedPiv);
                }

                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;                
                startExercise();
            }
            else if (isSetTimerRunning)
            {
                cardioPage.setTimerButton.Content = "Start";
                cardioPage.setTimerButton.FontWeight = FontWeights.Normal;
                setTimer.Stop();
                isSetTimerStopped = true;
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;
            }
            else
            {
                setTimer.Start();
                cardioPage.setTimerButton.Content = "Pause";
                cardioPage.setTimerButton.FontWeight = FontWeights.ExtraBold;
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }
            isSetTimerRunning = !isSetTimerRunning;
        }

        private void setTimerTick(object sender, EventArgs e)
        {
            setTimerRemaining = setTimerRemaining.Subtract(setTimerInterval);
            cardioPage.setTimerValue.Text = setTimerRemaining.ToString(@"mm\:ss");

            if (setTimerRemaining.Ticks <= 0)
            {
                cardioPage.setTimerButton.Content = "Start";
                setTimer.Stop();
                setTimerRemaining = TimeSpan.Zero;
                cardioPage.setTimerValue.Text = setTimerRemaining.ToString(@"mm\:ss");
                isSetTimerRunning = false;
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;
                cardioPage.setTimerButton.FontWeight = FontWeights.Normal;
                cardioPage.setTimerButton.Content = "Next exercise!";

                // Update the amount of burnt calories
                Profile profile = DataAccess.GetProfile();
                Machine machine = DataAccess.GetMachine(exercise.IdMachine);
                int calori =  machine.Calories * exercise.Time;
                int totalCalories = profile.BurntCalories + calori;
                DataAccess.AddCalories(totalCalories);

                DataAccess.AddCardioExerciseDone(exercise.Id, DateTime.Today, exercise.Time);
                end = true;
                if (exerciseIndex == exercises.Count)
                {
                    MessageBox.Show("Congratulations!! Cardio is finished");
                    NavigationService.GoBack();
                    return;
                }
            }
        }

        private void setTimerResetButton_Click(object sender, RoutedEventArgs e)
        {            
            if (isSetTimerRunning)
                setTimer.Stop();

            int seconds;
            if (int.TryParse(secondsString, out seconds))
            {
                setTimerRemaining = TimeSpan.FromSeconds(seconds);
                cardioPage.setTimerValue.Text = setTimerRemaining.ToString(@"mm\:ss");
            }

            if (isSetTimerRunning)
                setTimer.Start();
        }

        private void skipButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainPivot.Items.Count == 1)
            {
                MessageBox.Show("Congratulations! You have finished");
                GoToMainPage();
            }
            else
            {
                PivotItem removedPiv = new PivotItem();
                removedPiv = MainPivot.SelectedItem as PivotItem;
                MainPivot.Items.Remove(removedPiv);
            }
        }

        private void GoToMainPage()
        {
            NavigationService.RemoveBackEntry(); // Jump over exercises page
            NavigationService.GoBack();
        }

        private void MainPivot_LoadingPivotItem(object sender, PivotItemEventArgs e)
        {
            RefreshCurrentPivotItem();
            secondsString = exercise.Time.ToString();
            if (exercise.Machine.Type == MachineType.Cardio)
            {
                cardioPage = currentPivotItem.Content as CardioTrainingPage;
                setTimerRemaining = TimeSpan.FromSeconds(exercise.Time);
                cardioPage.setTimerValue.Text = setTimerRemaining.ToString(@"mm\:ss");
                //MessageBox.Show(exercise.ExerciseName);
            }
        }

        private void RefreshCurrentPivotItem()
        {
            currentPivotItem = MainPivot.SelectedItem as PivotItem;
            //exercise = exercises[MainPivot.SelectedIndex];   
            exercise = exercises[int.Parse(Convert.ToString(currentPivotItem.Tag))];
            if (exercise.Machine.Type == MachineType.Cardio)
            {
                cardioPage = currentPivotItem.Content as CardioTrainingPage;
            }
            else
            {
                liftPage = currentPivotItem.Content as LiftTrainingPage;
            }
        }
    }
}
