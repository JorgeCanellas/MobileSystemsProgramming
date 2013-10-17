using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym5.DataObjects
{
    [Table]
    public class RoutineExercise
    {
        private int routineId;
        private int exerciseId;
        private int id;

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
        public int RoutineId
        {
            get { return routineId; }
            set
            {
                if (routineId != value)
                {
                    routineId = value;
                }
            }
        }

        [Column]
        public int ExerciseId
        {
            get { return exerciseId; }
            set
            {
                if (exerciseId != value)
                {
                    exerciseId = value;
                }
            }
        }
    }
}
