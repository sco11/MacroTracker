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
    public partial class deleteFood : Form
    {

        List<Food> foodbank;

        public deleteFood(List<Food> foodbank)
        {
            InitializeComponent();
            this.foodbank = foodbank;

            foreach (var x in foodbank)
            {
                comboBox1.Items.Add(x);
            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Delete button
        private void button1_Click(object sender, EventArgs e)
        {
            foodbank.Remove((Food)comboBox1.SelectedItem);


            this.Close();
        }
    }
}
