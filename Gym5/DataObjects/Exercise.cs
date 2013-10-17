using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Gym5.DataObjects
{
    [Table]
    public class Exercise : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private int id;
        private int idMachine;
        private int sets;
        private int reps;
        private int weight;
        private int resistance;
        private int time;
        private string icon;
        private bool removed;

        public Exercise()
        {
        }

        public string ExerciseName { get { return Machine.Name; } }
        public string Sets_Time { get { return (Machine.Type == MachineType.Lifting ? sets.ToString() : time.ToString()); } }
        public string Reps_Level { get { return (Machine.Type == MachineType.Lifting ? reps.ToString() : resistance.ToString()); } }

        public string Details
        {
            get
            {
                if (machine.Type == MachineType.Cardio)
                {
                    return "Time: " + Time + ", Resistance: " + Resistance;
                }
                else
                {
                    return "Sets: " + Sets + ", Reps: " + Reps + ", Weight: " + Weight;
                }
            }
        }

        private Machine machine = null;
        public Machine Machine
        {
            get
            {
                if (machine == null) {  machine = DataAccess.GetMachine(idMachine); }
                return machine;
            }
            set
            {
                if (machine != value)
                {
                    machine = value;
                    NotifyPropertyChanged("Machine");
                }
            }
        }

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }

        [Column]
        public int IdMachine
        {
            get { return idMachine; }
            set
            {
                if (idMachine != value)
                {
                    idMachine = value;
                    NotifyPropertyChanged("IdMachine");
                }
            }
        }

        [Column(CanBeNull = true)]
        public int Sets
        {
            get { return sets; }
            set
            {
                if (sets != value)
                {
                    sets = value;
                    NotifyPropertyChanged("Sets");
                }
            }
        }

        [Column(CanBeNull = true)]
        public int Reps
        {
            get { return reps; }
            set
            {
                if (reps != value)
                {
                    reps = value;
                    NotifyPropertyChanged("Reps");
                }
            }
        }

        [Column(CanBeNull = true)]
        public int Weight
        {
            get { return weight; }
            set
            {
                if (weight != value)
                {
                    weight = value;
                    NotifyPropertyChanged("Weight");
                }
            }
        }

        [Column(CanBeNull = true)]
        public int Resistance
        {
            get { return resistance; }
            set
            {
                if (resistance != value)
                {
                    resistance = value;
                    NotifyPropertyChanged("Resistance");
                }
            }
        }

        [Column(CanBeNull = true)]
        public int Time
        {
            get { return time; }
            set
            {
                if (time != value)
                {
                    time = value;
                    NotifyPropertyChanged("Time");
                }
            }
        }

        [Column]
        public string Icon
        {
            get { return icon; }
            set
            {
                if (icon != value)
                {
                    NotifyPropertyChanging("Icon");
                    icon = value;
                    NotifyPropertyChanged("Icon");
                }
            }
        }

        [Column]
        public bool Removed
        {
            get { return removed; }
            set
            {
                if (removed != value)
                {
                    NotifyPropertyChanging("Removed");
                    removed = value;
                    NotifyPropertyChanged("Removed");
                }
            }
        }

        public Visibility EditAll
        {
            get
            {
                if (AllExercisesPage.edit) { return Visibility.Visible; }
                return Visibility.Collapsed;
            }
        }

        public Visibility EditRoutine
        {
            get
            {
                if (ExercisesPage.edit) { return Visibility.Visible; }
                return Visibility.Collapsed;
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanging(string propertyName)
        {
            if (this.PropertyChanging != null)
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
