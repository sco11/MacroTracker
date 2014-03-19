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
    public partial class editFood : Form
    {
        private Primary prim;

        public editFood(Primary prim)
        {
            this.prim = prim;

            InitializeComponent();

            foreach (Food food in prim.bank)
            {
                comboBox1.Items.Add(food);
            }

        }

        //Submit
        private void button1_Click(object sender, EventArgs e)
        {
            Food edited = (Food)comboBox1.SelectedItem;
            edited.name = textBox1.Text;
            edited.calories = (int)numericUpDown1.Value;
            edited.protein =  (int)numericUpDown2.Value;
            edited.carbs =    (int)numericUpDown3.Value;
            edited.fats =     (int)numericUpDown4.Value;
            edited.servingSize = textBox2.Text;

            prim.comboBox1.Items.Clear();
            foreach(var x in prim.bank)
            {
                prim.comboBox1.Items.Add(x);
            }



            prim.updateProgressBars();

            this.Close();
        }


        //Prefills data forms
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Food selected = (Food)comboBox1.SelectedItem;

            textBox1.Text = selected.name;
            numericUpDown1.Value = selected.calories;
            numericUpDown2.Value = selected.protein;
            numericUpDown3.Value = selected.carbs;
            numericUpDown4.Value = selected.fats;
            textBox2.Text = selected.servingSize;
        }
    }
}
