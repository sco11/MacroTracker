using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MacroTracker
{
    public partial class newMacros : Form
    {
        public MacroPlan user;
        private Form primForm;

        public newMacros(MacroPlan myPlan, Form primaryForm)
        {
            user = myPlan;
            primForm = primaryForm;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            user = new MacroPlan((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value, (int)numericUpDown4.Value);

            Primary f1 = (Primary)primForm;
            f1.textBox2.Text = user.calorieGoal.ToString();
            f1.textBox3.Text = user.proteinGoal.ToString();
            f1.textBox5.Text = user.carbGoal.ToString();
            f1.textBox7.Text = user.fatGoal.ToString();

            //Now save to xml file
            var x = new XmlSerializer(typeof(MacroPlan));
            //var fs = new FileStream("macroPlan.xml", FileMode.Open);
            var fs = new FileStream("macroPlan.xml", FileMode.Create);
            x.Serialize(fs, user);
            fs.Close();


            this.Close();

           
        }

        private void newMacros_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
        }
    }
}
