using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym5.DataObjects
{
    [Table]
    public class ExerciseDone
    {
        private int id;
        private int idExercise;
        private DateTime date;
        private int sets;
        private int reps;
        private int time;

        private Exercise exercise = null;
        public Exercise Exercise
        {
            get
            {
                if (exercise == null) { exercise = DataAccess.GetExercise(idExercise); }
                return exercise;
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
                }
            }
        }

        [Column]
        public int IdExercise
        {
            get { return idExercise; }
            set
            {
                if (idExercise != value)
                {
                    idExercise = value;
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

        [Column(CanBeNull = true)]
        public int Sets
        {
            get { return sets; }
            set
            {
                if (sets != value)
                {
                    sets = value;
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
                }
            }
        }
    }
}
