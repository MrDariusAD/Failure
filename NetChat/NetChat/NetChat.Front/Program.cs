using System;
using System.Windows.Forms;

namespace NetChat.Front {
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Logger.Start();
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWindow());
            }catch(Exception e)
            {
                Logger.Error(e.ToString());
                MessageBox.Show(@"This error will also be logged in the Logger.Log - " + Logger.path  + "\n\n" + e.ToString(), @"Error NetChat", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
