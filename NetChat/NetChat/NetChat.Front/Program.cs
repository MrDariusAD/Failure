using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetChat.Front
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWindow());
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString(), "Error NetChat", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
