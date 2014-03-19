using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacroTracker
{
    public partial class NewFood : Form
    {
        public Food toAdd;
        public List<Food> mybank;
        private ComboBox displayFood;

        public NewFood(List<Food> bank, ComboBox todaysFood)
        {
            displayFood = todaysFood;
            InitializeComponent();
            mybank = bank;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String newName = textBox1.Text;
            int newCalories = (int)numericUpDown1.Value;
            int newProtein = (int)numericUpDown2.Value;
            int newCarbs = (int)numericUpDown3.Value;
            int newFat = (int)numericUpDown4.Value;
            String newServingSize = textBox2.Text;

            Food toAdd = new Food(newName, newCalories, newProtein, newCarbs, newFat, newServingSize);
            mybank.Add(toAdd);


            displayFood.Items.Add(toAdd);
            
            
            this.Close();
        }

        public Food theFood()
        {
            return toAdd;
        }

    }
}
