using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym5.DataObjects
{
    [Table]
    public class Profile
    {
        private int id;
        private int restTime;
        private string name;

        private int burntCalories;
        private DateTime createdDate;
        private int currentRoutineId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                }
            }
        }

        [Column]
        public int RestTime
        {
            get { return restTime; }
            set
            {
                if (restTime != value)
                {
                    restTime = value;
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
                    name = value;
                }
            }
        }

        [Column]
        public int BurntCalories
        {
            get { return burntCalories; }
            set
            {
                if (burntCalories != value)
                {
                    burntCalories = value;
                }
            }
        }

        [Column]
        public DateTime CreatedTime
        {
            get { return createdDate; }
            set
            {
                if (createdDate != value)
                {
                    createdDate = value;
                }
            }
        }

        [Column(CanBeNull=true)]
        public int CurrentRoutineId
        {
            get { return currentRoutineId; }
            set
            {
                if (currentRoutineId != value)
                {
                    currentRoutineId = value;
                }
            }
        }
    }
}
