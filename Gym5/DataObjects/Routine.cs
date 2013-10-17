using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Gym5.DataObjects
{
    [Table]
    public class Routine : INotifyPropertyChanged, INotifyPropertyChanging
    {
        // Define ID: private field, public property, and database column.
        private int id;
        private DateTime startDate;
        private DateTime endDate;
        private string name;
        private bool removed;
        private string icon;

        public Routine()
        {
        }

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    NotifyPropertyChanging("Id");
                    id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }

        [Column]
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {
                    NotifyPropertyChanging("StartDate");
                    startDate = value;
                    NotifyPropertyChanged("StartDate");
                }
            }
        }

        [Column(CanBeNull = true)]
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                {
                    NotifyPropertyChanging("EndDate");
                    endDate = value;
                    NotifyPropertyChanged("EndDate");
                }
            }
        }

        [Column]
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    NotifyPropertyChanging("Name");
                    name = value;
                    NotifyPropertyChanged("Name");
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

        public Visibility Edit
        {
            get
            {
                if (MainPage.edit) { return Visibility.Visible; }
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
