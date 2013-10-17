using Gym5.DataObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym5
{
    public enum MachineType
    {
        Cardio = 0,
        Lifting = 1,
    }

    public class Gym5DataContext : DataContext
    {
        // Pass the connection string to the base class.
        public Gym5DataContext(string connectionString) : base(connectionString) { }

        // Specify a single table for the to-do items.
        public Table<Machine> MachineItems;
        public Table<Routine> RoutineItems;
        public Table<Weight> WeightItems;
        public Table<Exercise> ExerciseItems;
        public Table<ExerciseDone> ExerciseDoneItems;
        public Table<Profile> ProfileItems;
        public Table<RoutineExercise> RoutineExerciseItems;
        public Table<DatabaseVersion> VersionItems;
    }
}
