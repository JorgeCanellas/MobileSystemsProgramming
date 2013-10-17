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

namespace Gym5
{
    public partial class LiftTrainingPage : PhoneApplicationPage
    {
        public LiftTrainingPage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            //String routine = NavigationContext.QueryString["routineName"];
            //PageTitle.Text = routine;
            //PageTitle.Text = Data.Instance.currentRoutine.Name;

            // TODO
            //String exName = NavigationContext.QueryString["exerciseName"];
            //liftExTextBlock.Text = exName;

            //String liftSets = NavigationContext.QueryString["liftSets"];
            //setsTextBlock.Text = liftSets;

            //String liftReps = NavigationContext.QueryString["liftReps"];
            //repsTextBlock.Text = liftReps;
        }
    }
}