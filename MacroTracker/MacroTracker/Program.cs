using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacroTracker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

           // List<Food> myBank = new ArrayList();
            

            Application.Run(new Primary());
        }
    }
}
