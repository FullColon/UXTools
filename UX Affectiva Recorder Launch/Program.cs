using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using UX_Affectiva_Research_Tool;

namespace UX_Affectiva_Recorder_Launch
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
            try
            {
                Application.Run(new RecordGui());
            }
            catch (COMException e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
