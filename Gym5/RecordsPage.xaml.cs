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
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using System.Windows.Controls.DataVisualization.Charting;

namespace Gym5
{
    public partial class RecordsPage : PhoneApplicationPage
    {
        private List<ChartItem1> itemsWeekCardio;
        private List<ChartItem1> itemsWeekLift;
        private List<ChartItem1> itemsMonthCardio;
        private List<ChartItem1> itemsMonthLift;

        public RecordsPage()
        {
            Data.Initialize();

            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string newparameter = this.NavigationContext.QueryString["parameter"];
            //if (newparameter.Equals("day"))
            //{
            //    recordsPivot.SelectedIndex = 0;
            //}
            if (newparameter.Equals("week"))
            {
                recordsPivot.SelectedIndex = 0;
            }
            else if (newparameter.Equals("month"))
            {
                recordsPivot.SelectedIndex = 1;
            }
            //else if (newparameter.Equals("routine"))
            //{
            //    recordsPivot.SelectedIndex = 3;
            //}

            SetData(6, out itemsWeekCardio, out itemsWeekLift, MyLineSeriesChart);
            SetData(30, out itemsMonthCardio, out itemsMonthLift, MyLineSeriesChart3);

            //itemsWeekCardio.Add(new ChartItem1() { Count = 2, Date = DateTime.Today });
            //itemsWeekCardio.Add(new ChartItem1() { Count = 3, Date = DateTime.Today.AddDays(-3) });
            //itemsWeekCardio.Add(new ChartItem1() { Count = 1, Date = DateTime.Today.AddDays(-2) });
            //itemsWeekLift.Add(new ChartItem1() { Count = 1, Date = DateTime.Today });
            //itemsWeekLift.Add(new ChartItem1() { Count = 6, Date = DateTime.Today.AddDays(-1) });
            //itemsWeekLift.Add(new ChartItem1() { Count = 1, Date = DateTime.Today.AddDays(-4) });

            //itemsMonthCardio.Add(new ChartItem1() { Count = 2, Date = DateTime.Today });
            //itemsMonthCardio.Add(new ChartItem1() { Count = 3, Date = DateTime.Today.AddDays(-3) });
            //itemsMonthCardio.Add(new ChartItem1() { Count = 1, Date = DateTime.Today.AddDays(-2) });
            //itemsMonthLift.Add(new ChartItem1() { Count = 1, Date = DateTime.Today });
            //itemsMonthLift.Add(new ChartItem1() { Count = 6, Date = DateTime.Today.AddDays(-1) });
            //itemsMonthLift.Add(new ChartItem1() { Count = 1, Date = DateTime.Today.AddDays(-14) });

            // Month
            var exercisesMonth = DataAccess.GetExercisesDone(DateTime.Now.Subtract(TimeSpan.FromDays(30)), DateTime.Now);
        }

        private void SetData(int days, out List<ChartItem1> cardioItems, out List<ChartItem1> liftItems, Chart chart)
        {
            DateTime startDate = DateTime.Today.AddDays(-days);
            DateTime endDate = DateTime.Today.AddDays(1);

            var exercises = DataAccess.GetExercisesDone(startDate, endDate);
            cardioItems = new List<ChartItem1>();
            liftItems = new List<ChartItem1>();

            foreach (ExerciseDone exerciseDone in exercises)
            {
                if (exerciseDone.Exercise.Machine.Type == MachineType.Cardio)
                {
                    DateTime date = exerciseDone.Date.Date;
                    AddExercise(cardioItems, date);
                }
                else
                {
                    DateTime date = exerciseDone.Date.Date;
                    AddExercise(liftItems, date);
                }
            }

            // Dummy values to make the charts display correctly
            AddExercise(cardioItems, startDate, 0);
            AddExercise(cardioItems, endDate, 0);
            AddExercise(liftItems, startDate, 0);
            AddExercise(liftItems, endDate, 0);

            chart.DataContext = new ChartData1() { Cardio = cardioItems, Lift = liftItems, Min = startDate, Max = endDate };
        }

        private void AddExercise(List<ChartItem1> list, DateTime date)
        {
            AddExercise(list, date, 1);
        }

        private void AddExercise(List<ChartItem1> list, DateTime date, int count)
        {
            GetItem(list, date).Count += count;
        }

        private ChartItem1 GetItem(List<ChartItem1> list, DateTime date)
        {
            foreach (ChartItem1 item in list)
            {
                if (item.Date == date) { return item; }
            }
            ChartItem1 newItem = new ChartItem1();
            newItem.Date = date;
            list.Add(newItem);
            return newItem;
        }
    }
    
    public class ChartItem1
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }

    public class ChartData1
    {
        public List<ChartItem1> Cardio { get; set; }
        public List<ChartItem1> Lift { get; set; }
        public DateTime Min { get; set; }
        public DateTime Max { get; set; }
    }
}