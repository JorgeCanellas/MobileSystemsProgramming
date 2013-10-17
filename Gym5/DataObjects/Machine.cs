using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym5.DataObjects
{
    [Table]
    public class Machine : IComparable<Machine> //: INotifyPropertyChanged, INotifyPropertyChanging
    {
        // Define ID: private field, public property, and database column.
        private int id;
        private string name;
        private int calories;
        private MachineType type;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    //NotifyPropertyChanging("ToDoItemId");
                    id = value;
                    //NotifyPropertyChanged("ToDoItemId");
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
                    //NotifyPropertyChanging("ItemName");
                    name = value;
                    //NotifyPropertyChanged("ItemName");
                }
            }
        }

        [Column]
        public int Calories
        {
            get { return calories; }
            set
            {
                if (calories != value)
                {
                    //NotifyPropertyChanging("ItemName");
                    calories = value;
                    //NotifyPropertyChanged("ItemName");
                }
            }
        }

        [Column]
        public MachineType Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    //NotifyPropertyChanging("ItemName");
                    type = value;
                    //NotifyPropertyChanged("ItemName");
                }
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //public event PropertyChangingEventHandler PropertyChanging;

        public int CompareTo(Machine other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
