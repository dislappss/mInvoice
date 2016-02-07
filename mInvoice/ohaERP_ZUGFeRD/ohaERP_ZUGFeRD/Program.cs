using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ZUGFeRD_Test
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(true);
            System.Windows.Forms.Application.Run(new ZUGFeRD_Test.main_form());

            //Application app = new Application();
            //app.run();
        }
    }
}
