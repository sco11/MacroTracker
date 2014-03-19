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
    public partial class newEstimate : Form
    {
        private Primary x;

        public newEstimate(Primary x)
        {
            this.x = x;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String newName = textBox1.Text;
            int newCalories = (int)numericUpDown1.Value;
            int newProtein = (int)numericUpDown2.Value;
            int newCarbs = (int)numericUpDown3.Value;
            int newFat = (int)numericUpDown4.Value;


            Food toAdd = new Food(newName, newCalories, newProtein, newCarbs, newFat, "");

            ListViewItem item = new ListViewItem();
            item.Text = "(1x) " + newName;
            item.Tag = toAdd;

            x.listView1.Items.Add(item);


            x.selectedDay.addMeal(toAdd, 1);

            x.textBox1.Text = x.selectedDay.calculateCalories().ToString();
            x.textBox4.Text = x.selectedDay.calculateProtein().ToString();
            x.textBox6.Text = x.selectedDay.calculateCarbs().ToString();
            x.textBox8.Text = x.selectedDay.calculateFats().ToString();


            x.progressBar3.Increment((int)(toAdd.calories));
            x.progressBar5.Increment((int)(toAdd.protein));
            x.progressBar6.Increment((int)(toAdd.carbs));
            x.progressBar7.Increment((int)(toAdd.fats));


            x.updateGraph();

            this.Close();
        }
    }
}
