using Gym5.DataObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Gym5
{
    public static class DataAccess
    {
        public static void SetVersion(int version)
        {
            DatabaseVersion databaseVersion = new DatabaseVersion() { Version = version };
            Data.Instance.db.VersionItems.InsertOnSubmit(databaseVersion);
            Data.Instance.db.SubmitChanges();
        }
        public static DatabaseVersion GetVersion()
        {
            var versions = from DatabaseVersion version in Data.Instance.db.VersionItems select version;
            var versionItems = new ObservableCollection<DatabaseVersion>(versions);
            return versionItems.First();
        }
        public static void RemoveVersion()
        {
            Data.Instance.db.VersionItems.DeleteOnSubmit(GetVersion());
            Data.Instance.db.SubmitChanges();
        }

        /// <summary>
        /// Returns the existing profile or creates a new one if there's no existing profile and returns it
        /// </summary>
        /// <returns></returns>
        public static Profile GetProfile()
        {
            // Check if profile already exists. If not, add new
            var profiles = from Profile profile in Data.Instance.db.ProfileItems select profile;
            Data.Instance.ProfileItems = new ObservableCollection<Profile>(profiles);
            if (Data.Instance.ProfileItems.Count > 0) { return Data.Instance.ProfileItems.First(); } // Profile is already in the database

            // Create a new profile
            Profile newProfile = new Profile { Name = "Ana", RestTime = 1, CreatedTime = DateTime.Now, BurntCalories = 2500 };
            Data.Instance.ProfileItems.Add(newProfile);
            Data.Instance.db.ProfileItems.InsertOnSubmit(newProfile);
            Data.Instance.db.SubmitChanges();
            return newProfile;
        }
        public static void EditProfile(string name, int rest_time)
        {
            var profile = GetProfile();
            profile.Name = name;
            profile.RestTime = rest_time;
            Data.Instance.db.SubmitChanges();
        }
        public static void EditName(string name)
        {
            var profile = GetProfile();
            profile.Name = name;
            Data.Instance.db.SubmitChanges();
        }
        public static void EditRestTime(int rest_time)
        {
            var profile = GetProfile();
            profile.RestTime = rest_time;
            Data.Instance.db.SubmitChanges();
        }
        public static void AddCalories(int calories)
        {
            var profile = GetProfile();
            profile.BurntCalories = calories;
            Data.Instance.db.SubmitChanges();
        }
        public static void ResetCalories()
        {
            var profile = GetProfile();
            profile.BurntCalories = 0;
            Data.Instance.db.SubmitChanges();
        }
        public static void AddWeightEntry(DateTime date, float value)
        {
            // Create a new weight
            Weight newWeight = new Weight { Date = date, WeightValue = value };
            Data.Instance.db.WeightItems.InsertOnSubmit(newWeight);
            Data.Instance.db.SubmitChanges();
        }
        public static Weight GetLastWeightEntry()
        {
            // Gets the last Weight entry in the DataBase
            // Prepare the query statement
            var weights = from Weight weight in Data.Instance.db.WeightItems select weight;
            Data.Instance.WeightItems = new ObservableCollection<Weight>(weights);
            if (Data.Instance.WeightItems.Count > 0)
            {
                // There are weight records, get the last one and return it
                return Data.Instance.WeightItems.Last();
            }
            else
            {
                return null;
            }
        }
        public static ObservableCollection<Weight> GetWeightEntries(DateTime startDate, DateTime endDate)
        {
            // gets the weight entries between the given dates
            var weights = from Weight weight in Data.Instance.db.WeightItems where weight.Date.CompareTo(startDate) >= 0 && weight.Date.CompareTo(endDate) <= 0 select weight;
            Data.Instance.WeightItems = new ObservableCollection<Weight>(weights);
            return Data.Instance.WeightItems;
        }

        public static Routine AddRoutine(string name, DateTime startDate, DateTime endDate)
        {
            return AddRoutine(name, startDate, endDate, "/Images/Running_icons.png");
        }
        public static Routine AddRoutine(string name, DateTime startDate, DateTime endDate, string icon)
        {
            //  Add a Routine
            Routine newRoutine = new Routine { Name = name, StartDate = startDate, EndDate = endDate, Removed = false, Icon = icon };
            Data.Instance.db.RoutineItems.InsertOnSubmit(newRoutine);
            Data.Instance.db.SubmitChanges();
            return newRoutine;
        } 
        public static ObservableCollection<Routine> GetRoutines()
        {
            // get all the routines
            var routinesInDB = from Routine routine in Data.Instance.db.RoutineItems where !routine.Removed select routine;
            Data.Instance.RoutineItems = new ObservableCollection<Routine>(routinesInDB);
            return Data.Instance.RoutineItems;
        }
        public static Routine GetRoutine(int id)
        {
            var routinesInDB = from Routine routine in Data.Instance.db.RoutineItems where routine.Id == id select routine;
            var routines = new ObservableCollection<Routine>(routinesInDB);
            return routines.First();
        }
        public static void RemoveRoutine(int id)
        {
            try
            {
                var routine = GetRoutine(id);
                routine.Removed = true;
                
                Data.Instance.db.SubmitChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error.");
            }
        }
  

        public static Machine AddMachine(MachineType type, String name, int calories)
        {
            //  Add a new Machine
            Machine newMachine = new Machine { Name = name, Type = type, Calories = calories};
            Data.Instance.db.MachineItems.InsertOnSubmit(newMachine);
            Data.Instance.db.SubmitChanges();
            return newMachine;
        }
        public static ObservableCollection<Machine> GetMachines()
        {
            // returns all the Machines in the DataBase
            var machinesInDB = from Machine machine in Data.Instance.db.MachineItems select machine;
            Data.Instance.MachineItems = new ObservableCollection<Machine>(machinesInDB);
            Data.Instance.MachineItems =  new ObservableCollection<Machine>(Data.Instance.MachineItems.OrderBy(machine => machine));
            return Data.Instance.MachineItems;
            
        }
        public static Machine GetMachine(int machineId)
        {
            var machinesInDB = from Machine machine in Data.Instance.db.MachineItems where machine.Id == machineId select machine;
            return new ObservableCollection<Machine>(machinesInDB).First();
        }

        public static Exercise AddLiftingExercise(int sets, int reps, int weight, int id_machine)
        {
            return AddLiftingExercise(sets, reps, weight, id_machine, "/Images/gym2.jpg");
        }
        public static Exercise AddLiftingExercise(int sets, int reps, int weight, int id_machine, string icon)
        {
            // TODO Adds a new lifting exercise in the DataBase
            
            // TODO Manage the machine id
            // TODO Check values of machine_id

            Exercise newExercise = new Exercise {IdMachine = id_machine, Reps = reps, Sets = sets, Weight = weight, Icon = icon, Removed = false };
            Data.Instance.db.ExerciseItems.InsertOnSubmit(newExercise);
            Data.Instance.db.SubmitChanges();
            return newExercise;
        }
        public static Exercise AddCardioExercise(int time, int resistance, int id_machine)
        {
            return AddCardioExercise(time, resistance, id_machine, "/Images/Running_icons.png");
        }
        public static Exercise AddCardioExercise(int time, int resistance, int id_machine, string icon)
        {
            // TODO Adds a new cardio exercise in the DataBase
            
            // TODO Manage the machine id
            // TODO Check values of machine_id

            Exercise newExercise = new Exercise { IdMachine = id_machine, Resistance = resistance, Time = time, Icon = icon, Removed = false };
            Data.Instance.db.ExerciseItems.InsertOnSubmit(newExercise);
            Data.Instance.db.SubmitChanges();
            return newExercise;
        }
        public static void AddRoutineExercise(int id_exercise, int id_routine)
        {
            var exercisesRepeated = from RoutineExercise routineExercise in Data.Instance.db.RoutineExerciseItems where routineExercise.ExerciseId == id_exercise && routineExercise.RoutineId == id_routine select routineExercise;
            Data.Instance.RoutineExerciseItems = new ObservableCollection<RoutineExercise>(exercisesRepeated);
            if (Data.Instance.RoutineExerciseItems.Count == 0)
            {
                RoutineExercise newRoutineExercise = new RoutineExercise { ExerciseId = id_exercise, RoutineId = id_routine};
                Data.Instance.db.RoutineExerciseItems.InsertOnSubmit(newRoutineExercise);
                Data.Instance.db.SubmitChanges();
            }
        }
        public static RoutineExercise GetRoutineExercise(int id_exercise, int id_routine)
        {
            var exercisesInDB = from RoutineExercise routineExercise in Data.Instance.db.RoutineExerciseItems
                                where routineExercise.RoutineId == id_routine && routineExercise.ExerciseId == id_exercise
                                select routineExercise;
            return new ObservableCollection<RoutineExercise>(exercisesInDB).First();
        }
        public static void RemoveRoutineExercise(int id_exercise, int id_routine)
        {
            RoutineExercise routineExercise = GetRoutineExercise(id_exercise, id_routine);
            Data.Instance.db.RoutineExerciseItems.DeleteOnSubmit(routineExercise);
            Data.Instance.db.SubmitChanges();
        }
        public static Exercise GetExercise(int exerciseId)
        {
            var exercisesInDB = from Exercise exercise in Data.Instance.db.ExerciseItems where exercise.Id == exerciseId select exercise;
            return new ObservableCollection<Exercise>(exercisesInDB).First();
        }
        public static ObservableCollection<Exercise> GetExercises()
        {
            // TODO Returns all exercises in the DataBase 
            var exercisesInDB = from Exercise exercise in Data.Instance.db.ExerciseItems where !exercise.Removed select exercise;
            return new ObservableCollection<Exercise>(exercisesInDB);
        }
        public static ObservableCollection<Exercise> GetRoutineExercises(int routineID)
        {
            var exercisesInDB = from RoutineExercise routineExercise in Data.Instance.db.RoutineExerciseItems
                                from Exercise exercise in Data.Instance.db.ExerciseItems
                                where routineExercise.RoutineId == routineID && routineExercise.ExerciseId == exercise.Id
                                select exercise;
            return new ObservableCollection<Exercise>(exercisesInDB);
        }
        public static void RemoveExercise(int exerciseId)
        {
            // TODO
        }

        public static ExerciseDone AddLiftingExerciseDone(int idExercise, DateTime date, int sets, int reps)
        {
            ExerciseDone newExerciseDone = new ExerciseDone { IdExercise = idExercise, Date = date.Date, Sets = sets, Reps = reps };
            Data.Instance.db.ExerciseDoneItems.InsertOnSubmit(newExerciseDone);
            Data.Instance.db.SubmitChanges();
            return newExerciseDone;
        }
        public static ExerciseDone AddCardioExerciseDone(int idExercise, DateTime date, int time)
        {
            ExerciseDone newExerciseDone = new ExerciseDone { IdExercise = idExercise, Date = date.Date, Time = time};
            Data.Instance.db.ExerciseDoneItems.InsertOnSubmit(newExerciseDone);
            Data.Instance.db.SubmitChanges();
            return newExerciseDone;
        }


        public static ObservableCollection<ExerciseDone> GetExercisesDone(DateTime startDate, DateTime endDate)
        {
            var exercisesDoneInDB = from ExerciseDone exerciseDone in Data.Instance.db.ExerciseDoneItems where exerciseDone.Date.CompareTo(startDate) >= 0 && exerciseDone.Date.CompareTo(endDate) <= 0 select exerciseDone;
            return new ObservableCollection<ExerciseDone>(exercisesDoneInDB);
        }
    }
}
