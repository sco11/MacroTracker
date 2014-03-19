using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTracker
{
    [Serializable]
    public class Food
    {
        public string name;
        public int calories;
        public int protein;
        public int carbs;
        public int fats;
        public string servingSize;


        private Food()
        {
            //Make compiler happy?
        }

        public Food(string name, int calories, int protein, int carbs, int fats, string servingSize)
        {
            this.name = name;
            this.calories = calories;
            this.protein = protein;
            this.carbs = carbs;
            this.fats = fats;
            this.servingSize = servingSize;
 

        }

        public override string ToString()
        {
            //return base.ToString();
            return name + " (" + servingSize + ")";
        }

    }
}
