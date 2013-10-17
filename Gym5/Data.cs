using Gym5.DataObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym5
{
    internal class Data
    {
        internal static Data Instance { get; private set; }
        internal static void Initialize() { if (Instance == null) { new Data(); } }

        const int version = 3; // Increment this if changing the database!!!

        internal Gym5DataContext db;
        internal ObservableCollection<Machine> MachineItems;
        internal ObservableCollection<Exercise> ExerciseItems;
        internal ObservableCollection<ExerciseDone> ExerciseDoneItems;
        internal ObservableCollection<Profile> ProfileItems;
        internal ObservableCollection<Routine> RoutineItems;
        internal ObservableCollection<Weight> WeightItems;
        internal ObservableCollection<RoutineExercise> RoutineExerciseItems;

        internal Routine currentRoutine = null;

        internal Data()
        {
            Instance = this;

            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            db = new Gym5DataContext("isostore:/Gym5.sdf");

         //   db.DeleteDatabase(); // TODO Remove this when the database is complete

            if (!db.DatabaseExists())
            {
                // Create the database if it does not yet exist.
                CreateDatabase();
            }
            else
            {
                try
                {
                    int oldVersion = DataAccess.GetVersion().Version;
                    if (oldVersion < version)
                    {
                        DataAccess.RemoveVersion();
                        db.DeleteDatabase();
                        CreateDatabase();
                    }
                }
                catch
                {
                    db.DeleteDatabase();
                    CreateDatabase();
                }
            }
        }

        private void CreateDatabase()
        {
            db.CreateDatabase();

            // Increment version if changing the database!!!
            DataAccess.SetVersion(version);
            DataAccess.AddMachine(MachineType.Cardio, "Bicycle", 8);
            DataAccess.AddMachine(MachineType.Cardio, "Treadmill", 11);
            DataAccess.AddMachine(MachineType.Lifting, "Leg Press", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Pull-Up", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Abs", 7);
            // Biceps: 5
            DataAccess.AddMachine(MachineType.Lifting, "Alternating Supinating Curl", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Alternating Dumbbell Curl", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Single Arm Seated Concentrated Curl Over Leg", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Alternating hammer curls", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Biceps pulley curls", 7);
            DataAccess.AddMachine(MachineType.Lifting, "High pulley cable curls ", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Straight bar curl", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Scott bench curls", 7);
            //Triceps:13
            DataAccess.AddMachine(MachineType.Lifting, "high pulley triceps extensions", 7);
            DataAccess.AddMachine(MachineType.Lifting, "alternate pulley triceps extensions", 7);
            DataAccess.AddMachine(MachineType.Lifting, "French press ", 7);
            DataAccess.AddMachine(MachineType.Lifting, "French press with dumbells", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Dips", 7);
            //shoulder 18
            DataAccess.AddMachine(MachineType.Lifting, "press behind the neck", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Frontal press", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Sitted press with dumbells", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Lateral elevations with dumbells", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Alternate front elevations with dumbells", 7);
            //Chest 23
            DataAccess.AddMachine(MachineType.Lifting, "Bench press", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Bench press with dumbells", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Inclinated bench press", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Inclinated bench press with dumbells", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Declinated bench press", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Declinated bench press with dumbells", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Pullover", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Pushups", 7);
            //Back 31
            DataAccess.AddMachine(MachineType.Lifting, "Chinups", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Polley to the chest", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Polley back neck", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Rowing", 7);
            //Legs 35
            DataAccess.AddMachine(MachineType.Lifting, "Squats", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Squats with bar", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Hack squat", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Leg curl", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Donkey calf raise", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Lunges", 7);
            //Abs 41
            DataAccess.AddMachine(MachineType.Lifting, "Crunch", 7);
            DataAccess.AddMachine(MachineType.Lifting, "Twist", 7);

            DataAccess.AddRoutine("Full Body", DateTime.Now, DateTime.Now.AddMonths(1));
            DataAccess.AddRoutine("Fat Burn", DateTime.Now, DateTime.Now.AddMonths(1));
            
            DataAccess.AddCardioExercise(10, 10, 1);
            DataAccess.AddCardioExercise(10, 10, 2);
            DataAccess.AddCardioExercise(20, 10, 1);
            DataAccess.AddCardioExercise(20, 10, 2);
            DataAccess.AddCardioExercise(30, 10, 1);
            DataAccess.AddCardioExercise(30, 10, 2);
            DataAccess.AddCardioExercise(50, 10, 1);
            DataAccess.AddCardioExercise(50, 10, 2);
            

            DataAccess.AddLiftingExercise(3, 12, 10, 25);
            DataAccess.AddLiftingExercise(3, 12, 6, 6);
            DataAccess.AddLiftingExercise(3, 12, 10, 14);
            DataAccess.AddLiftingExercise(3, 12, 10, 22);
            DataAccess.AddLiftingExercise(3, 12, 10, 31);
            DataAccess.AddLiftingExercise(3, 12, 10, 39);
            DataAccess.AddLiftingExercise(3, 25, 0, 42);

            DataAccess.AddRoutineExercise(1, 1);
            DataAccess.AddRoutineExercise(9, 1);
            DataAccess.AddRoutineExercise(10, 1);
            DataAccess.AddRoutineExercise(11, 1);
            DataAccess.AddRoutineExercise(12, 1);
            DataAccess.AddRoutineExercise(13, 1);
            DataAccess.AddRoutineExercise(14, 1);
            DataAccess.AddRoutineExercise(15, 1);
            DataAccess.AddRoutineExercise(2, 1);

            DataAccess.AddRoutineExercise(7, 2);
            DataAccess.AddRoutineExercise(8, 2);



        }
    }
}
