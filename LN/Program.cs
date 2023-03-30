using System;
using System.Windows.Forms;

namespace CANAPRO
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new /*F_Principal()*/UI.F_Acceso());
        }
    }
}
