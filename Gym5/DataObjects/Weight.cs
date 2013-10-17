using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym5.DataObjects
{
    [Table]
    public class Weight
    {
        private int id;
        private DateTime date;
        private float weightValue;

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
        public DateTime Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                }
            }
        }

        [Column]
        public float WeightValue
        {
            get { return weightValue; }
            set
            {
                if (weightValue != value)
                {
                    weightValue = value;
                }
            }
        }
    }
}
