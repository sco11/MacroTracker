using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTracker
{
    [Serializable]
    public class MacroPlan
    {
        public int calorieGoal;
        public int proteinGoal;
        public int carbGoal;
        public int fatGoal;

        public MacroPlan()
        {
            //Compiler make happy for serialization
        }

        public MacroPlan(int calorieGoal, int proteinGoal, int carbGoal, int fatGoal)
        {
            this.calorieGoal = calorieGoal;
            this.proteinGoal = proteinGoal;
            this.carbGoal = carbGoal;
            this.fatGoal = fatGoal;
        }



    }
}
