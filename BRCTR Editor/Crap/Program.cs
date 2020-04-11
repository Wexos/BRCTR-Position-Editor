using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BRCTR_Editor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length >= 1)
            {
                Form1 f1 = new Form1();
                f1.PreRead(args[0]);
                Application.Run(f1);
            }
            else
            {
                Application.Run(new Form1());
            }
        }
    }
}
