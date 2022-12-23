using System;
using System.Windows.Forms;

namespace PracticMapsProject
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try 
            { 
                Application.Run(new main_Form());
            }
            catch
            {
                // TODO игнорировать
            }
        }
    }
}
