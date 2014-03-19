using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacroTracker
{
    [Serializable]
    public class Day
    {
        public double weight;
        public List<Food> todaysFood;
        public List<double> todaysFoodQuantities;
        //public int calculateCalories();
        //public int calculateProtein();
        //public int calculateCarbs();
        //public int calculateFats();
        public DateTime date;
        //public Dictionary<Food, double> foodList;

        public int calculateCalories()
        {
            double cal = 0;
            for (int i = 0; i < todaysFood.Count; i++)
            {
                cal += todaysFood[i].calories * todaysFoodQuantities[i];
            }

            return (int)cal;
        }

        public int calculateCarbs()
        {
            double carbs = 0;
            for (int i = 0; i < todaysFood.Count; i++)
            {
                carbs += todaysFood[i].carbs * todaysFoodQuantities[i];
            }

            return (int)carbs;
        }

        public int calculateProtein()
        {
            double protein = 0;
            for (int i = 0; i < todaysFood.Count; i++)
            {
                protein += todaysFood[i].protein * todaysFoodQuantities[i];
            }

            return (int)protein;
        }

        public int calculateFats()
        {
            double fats = 0;
            for (int i = 0; i < todaysFood.Count; i++)
            {
                fats += todaysFood[i].fats * todaysFoodQuantities[i];
            }

            return (int)fats;
        }

        private Day()
        {
            //make happy compiler
        }

        public Day(DateTime date)
        {
            todaysFood = new List<Food>();
            todaysFoodQuantities = new List<double>();
            //foodList = new Dictionary<Food, double>();
            this.date = date;
        }

        public void addMeal(Food toAdd, double quantity)
        {
            todaysFood.Add(toAdd);
            todaysFoodQuantities.Add(quantity);
            //foodList.Add(toAdd, quantity);

            //calculateCalories() += (int)(toAdd.calories *  quantity);
            //calculateProtein() +=  (int)(toAdd.protein *   quantity);
            //calculateCarbs() +=    (int)(toAdd.carbs *     quantity);
            //calculateFats() +=     (int)(toAdd.fats *      quantity);

        }

        public void removeMeal(int indexToRemove, Form primaryForm)
        {
            Primary primForm = (Primary)primaryForm;

            primForm.progressBar3.Maximum = primForm.usersPlan.calorieGoal;
            primForm.progressBar5.Maximum = primForm.usersPlan.proteinGoal;
            primForm.progressBar6.Maximum = primForm.usersPlan.carbGoal;
            primForm.progressBar7.Maximum = primForm.usersPlan.fatGoal;

            todaysFood.RemoveAt(indexToRemove);
            todaysFoodQuantities.RemoveAt(indexToRemove);


            //calculateCalories() -= todaysFood[indexToRemove].calories * (int)todaysFoodQuantities[indexToRemove];
            if (calculateCalories() <= primForm.progressBar3.Maximum)
            {
                primForm.progressBar3.Value = calculateCalories();
            }


            //calculateProtein() -= todaysFood[indexToRemove].protein * (int)todaysFoodQuantities[indexToRemove];
            if (calculateProtein() <= primForm.progressBar5.Maximum)
            {
                primForm.progressBar5.Value = calculateProtein();
            }


            //calculateCarbs() -= todaysFood[indexToRemove].carbs * (int)todaysFoodQuantities[indexToRemove];
            if (calculateCarbs() <= primForm.progressBar6.Maximum)
            {
                primForm.progressBar6.Value = calculateCarbs();
            }

           
            //calculateFats() -= todaysFood[indexToRemove].fats * (int)todaysFoodQuantities[indexToRemove];
            if (calculateFats() <= primForm.progressBar7.Maximum)
            {
                primForm.progressBar7.Value = calculateFats();
            }

    



        }

    }

}
