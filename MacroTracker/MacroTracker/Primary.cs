using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using System.Xml.Serialization;

namespace MacroTracker
{
    public partial class Primary : Form
    {
        public List<Food> bank;
        public MacroPlan usersPlan;
        public List<Day> allDays;
        public Day selectedDay;

        public Primary()
        {
           
            
            InitializeComponent();
            loadFoodBank();
            loadMacroPlan();
            loadDay();
            loadChart(1);

        }

        private void loadDay()
        {
            this.allDays = new List<Day>();

            try
            {
                var x = new XmlSerializer(typeof(List<Day>));
                var fs = new FileStream("dayLog.xml", FileMode.Open);
                this.allDays = x.Deserialize(fs) as List<Day>;
                fs.Close();
               

            }
            catch (Exception e)
            {
                //couldn't load days
                //MessageBox.Show("No log found. Creating new one now");       
            }

            try
            {
                
                foreach (Day z in allDays)
                {
                    if (DateTime.Compare(DateTime.Today, z.date) == 0)
                    {
                        this.selectedDay = z;
                        break;
    
                    }
                }
                updateTextBoxes();
                updateGraph();
                updateProgressBars();
            }
            catch (Exception f)
            {
                this.selectedDay = new Day(DateTime.Today);
            }

            for (int i = 0; i < selectedDay.todaysFoodQuantities.Count; i++)
            {
                ListViewItem k = new ListViewItem();
                k.Tag = selectedDay.todaysFood;
                k.Text = "(" + selectedDay.todaysFoodQuantities[i].ToString() + "x) " + selectedDay.todaysFood[i].ToString();               
                listView1.Items.Add(k);
            }

            listView1.Columns.RemoveAt(0);

            textBox9.Text = selectedDay.weight.ToString();        
        }


        private void loadFoodBank()
        {
            bank = new List<Food>();

            try
            {
                var x = new XmlSerializer(typeof(List<Food>));
                var fs = new FileStream("foodBank.xml", FileMode.Open);
                bank = x.Deserialize(fs) as List<Food>;
                fs.Close();
                foreach (var z in bank)
                {
                    //comboBox1.Items.Add(z.ToString());
                    comboBox1.Items.Add(z);
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("Foodbank not loaded");
            }
 
           
        }

        private void loadMacroPlan()
        {
            usersPlan = new MacroPlan();

            try
            {
                var x = new XmlSerializer(typeof(MacroPlan));
                var fs = new FileStream("macroPlan.xml", FileMode.Open);
                usersPlan = x.Deserialize(fs) as MacroPlan;
                fs.Close();

                textBox2.Text = usersPlan.calorieGoal.ToString();
                textBox3.Text = usersPlan.proteinGoal.ToString();
                textBox5.Text = usersPlan.carbGoal.ToString();
                textBox7.Text = usersPlan.fatGoal.ToString();
            }
            catch (Exception e)
            {
                //No macro plan found, must make one
                newMacros formForMacros = new newMacros(usersPlan, this);
                formForMacros.Show();
            }

            progressBar3.Maximum = usersPlan.calorieGoal;
            progressBar5.Maximum = usersPlan.proteinGoal;
            progressBar6.Maximum = usersPlan.carbGoal;
            progressBar7.Maximum = usersPlan.fatGoal;


        }

        //Create new food button
        private void button4_Click(object sender, EventArgs e)
        {
            NewFood formForNewFood = new NewFood(bank, comboBox1);
            formForNewFood.Show();
            
         }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Edit Macro Plan (menu button)
        private void macroPlanToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            newMacros formForMacros = new newMacros(usersPlan, this);
            formForMacros.Show();


            progressBar3.Maximum = usersPlan.calorieGoal;
            progressBar5.Maximum = usersPlan.proteinGoal;
            progressBar6.Maximum = usersPlan.carbGoal;
            progressBar7.Maximum = usersPlan.fatGoal;
        }

        //Display foodbank button
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (var z in bank)
                {
                    MessageBox.Show(z.name);
                    Console.WriteLine(z.name);
                }
            }
            catch (Exception f)
            {
            
            }
        }

        //Save foodbank (save xml) button
        private void button7_Click(object sender, EventArgs e)
        {
            var x = new XmlSerializer(typeof(List<Food>));
            var fs = new FileStream("foodBank2.xml", FileMode.Open);
            x.Serialize(fs, bank);
            fs.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        //Add Food
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Must first select a food to add from the dropdown");
            }
            else
            {
                Food addMe = (Food)comboBox1.SelectedItem;

                double quantity = (double)numericUpDown1.Value;

                ListViewItem item = new ListViewItem();
                item.Text = "(" + quantity.ToString() + "x) " + addMe.ToString();
                item.Tag = addMe;
                listView1.Items.Add(item);

                selectedDay.addMeal(addMe, quantity);

                updateTextBoxes();
                updateProgressBars();
                updateGraph();
            }
        }

        //Save day button
        private void button8_Click(object sender, EventArgs e)
        {
            saveDay();
            /*
            bool theDayExists = false;
            foreach (Day z in allDays)
            {
                if (DateTime.Compare(selectedDay.date, z.date) == 0)
                {

                    theDayExists = true;
                }
            }
            if (theDayExists == false)
            {

                allDays.Add(selectedDay);
            }

            var x = new XmlSerializer(typeof(List<Day>));
            //var fs = new FileStream("dayLog.xml", FileMode.Open);
            var w = XmlWriter.Create(File.Create("daylog.xml"));
            //x.Serialize(fs, allDays);
            x.Serialize(w, allDays);
            //fs.Close(); */
        }

        private void saveDay()
        {
            bool theDayExists = false;
            foreach (Day z in allDays)
            {
                if (DateTime.Compare(selectedDay.date, z.date) == 0)
                {

                    theDayExists = true;
                }
            }
            if (theDayExists == false)
            {

                allDays.Add(selectedDay);
            }

            var x = new XmlSerializer(typeof(List<Day>));
            var fs = new FileStream("dayLog.xml", FileMode.Create);
            //var w = XmlWriter.Create(File.Create("daylog.xml"));
            x.Serialize(fs, allDays);
            //x.Serialize(w, allDays);
            fs.Close();
            //w.Close();

        }


        //Record weight button
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                selectedDay.weight = Double.Parse(textBox9.Text);
            }
            catch (Exception q)
            {
                MessageBox.Show("Must enter valid format xx.xx for weight");
            }
            updateGraph();
        }

        //Clicked a day on calender
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            saveDay();

            listView1.Items.Clear();

            bool success = false;
            try
            {

                foreach (Day z in allDays)
                {
                    if (DateTime.Compare(e.Start, z.date) == 0)
                    {
                        selectedDay = z;
                        success = true;
                        break;
                    }
                }

            }
            catch (Exception f)
            {
                selectedDay = new Day(e.Start);
               
            }
            if (success == false)
            {
                selectedDay = new Day(e.Start);
                
            }

            if (selectedDay.todaysFood.Count > 0)
            {
                for (int i = 0; i < selectedDay.todaysFood.Count; i++)
                {

                    ListViewItem addMe = new ListViewItem();
                    addMe.Text = "(" + selectedDay.todaysFoodQuantities[i].ToString() + "x) " + selectedDay.todaysFood[i].ToString();
                    addMe.Tag = selectedDay.todaysFood[i];

                    listView1.Items.Add(addMe);
                }
            }

  
            textBox9.Text = selectedDay.weight.ToString();

            updateTextBoxes();
            updateProgressBars();
            updateGraph();

        }

        public void updateGraph()
        {
            if (radioButton1.Checked)
            {
                loadChart(1);
            }
            else if (radioButton2.Checked)
            {
                loadChart(2);
            }
            else if (radioButton3.Checked)
            {
                loadChart(3);
            }
            else if (radioButton4.Checked)
            {
                loadChart(4);
            }
            else if (radioButton5.Checked)
            {
                loadChart(5);
            }
        }

        //Remove selected button
        private void button2_Click(object sender, EventArgs e)
        {

            for (int i = listView1.Items.Count - 1; i >= 0; i--)
            {
                if (listView1.Items[i].Checked)
                {
                    selectedDay.removeMeal(i, this);
                    listView1.Items[i].Remove();
                }
            }

            updateTextBoxes();
            updateGraph();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            loadChart(5);
        }

        private void loadChart(int radioButton)
        {
            string radioName;
            switch (radioButton)
            {
                case 1:

                    radioName = "Calories";
                    break;
                case 2:
                    radioName = "Protein";
                    break;
                case 3:
                    radioName = "Carbs";
                    break;
                case 4:
                    radioName = "Fats";
                    break;
                case 5:
                    radioName = "Weight";
                    break;
                default:
                    radioName = "Calories";
                    break;
            }

            chart1.Series.Clear();

            List<double> myDataPoints = new List<double>();
            List<Day> past2Weeks = new List<Day>();

            Series toDisplay = chart1.Series.Add(radioName);
            toDisplay.Points.Clear();
            toDisplay.ChartType = SeriesChartType.Line;

            //double x;

            for (int i = 6; i >= 0; i--)
            {
                foreach (Day z in allDays)
                {
                    if (DateTime.Compare(z.date.AddDays(i), selectedDay.date) == 0)
                    {
                                 
                        switch (radioButton)
                        {
                            case 1:

                                myDataPoints.Add(z.calculateCalories());
                                break;
                            case 2:
                                myDataPoints.Add(z.calculateProtein());
                                break;
                            case 3:
                                myDataPoints.Add(z.calculateCarbs());
                                break;
                            case 4:
                                myDataPoints.Add(z.calculateFats());
                                break;
                            case 5:
                                myDataPoints.Add(z.weight);
                                break;

                        }
                        past2Weeks.Add(z);
           
                    }
                }
            }

             foreach (var x in myDataPoints)
             {
                 toDisplay.Points.Add(x);
  
             }

             chart1.ChartAreas[0].AxisX.Interval = 1;

             for (int i = 0; i < myDataPoints.Count; i++)
             {
                 chart1.Series[radioName].Points[i].AxisLabel = past2Weeks[i].date.ToString("d");
             }
     
  
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            loadChart(2);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            loadChart(1);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            loadChart(3);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            loadChart(4);
        }

   


        public void updateTextBoxes()
        {
            textBox1.Text = selectedDay.calculateCalories().ToString();
            textBox4.Text = selectedDay.calculateProtein().ToString();
            textBox6.Text = selectedDay.calculateCarbs().ToString();
            textBox8.Text = selectedDay.calculateFats().ToString();
        }

        private void progressBar3_Click(object sender, EventArgs e)
        {

        }

        private void Primary_Load(object sender, EventArgs e)
        {
        }


        private void Primary_FormClosing(Object sender, FormClosingEventArgs e)
        {
            saveDay();

            var x = new XmlSerializer(typeof(List<Food>));
            var fs = new FileStream("foodBank.xml", FileMode.Create);
            x.Serialize(fs, bank);
            fs.Close();

        }

        private void foodBankToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            newEstimate estimate = new newEstimate(this);
            estimate.Show();

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about about = new about();
            about.Show();
        }

        private void deleteFoodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form deleteFood = new deleteFood(bank);
            deleteFood.Show();
        }

        private void editFoodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editFood edit = new editFood(this);
            edit.Show();
        }

        public void updateProgressBars()
        {
            progressBar3.Maximum = usersPlan.calorieGoal;
            progressBar5.Maximum = usersPlan.proteinGoal;
            progressBar6.Maximum = usersPlan.carbGoal;
            progressBar7.Maximum = usersPlan.fatGoal;

            try
            {
                progressBar3.Value = selectedDay.calculateCalories();

            }
            catch (Exception e)
            { progressBar3.Value = progressBar3.Maximum; }

            try
            {
                progressBar5.Value = selectedDay.calculateProtein();
            }
            catch (Exception e)
            { progressBar5.Value = progressBar5.Maximum; }

            try
            {
                progressBar6.Value = selectedDay.calculateCarbs();
            }
            catch (Exception e)
            { progressBar6.Value = progressBar6.Maximum; }

            try
            {
                progressBar7.Value = selectedDay.calculateFats();
            }
            catch(Exception e)
            { progressBar7.Value = progressBar7.Maximum; }
        }




    }

}
