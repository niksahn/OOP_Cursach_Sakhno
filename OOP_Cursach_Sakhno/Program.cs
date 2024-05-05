using Microsoft.Extensions.DependencyInjection;
using OOP_Cursach_Sakhno.ui;

namespace OOP_Cursach_Sakhno
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            MineNavigator navigator = new MineNavigator();
            navigator.navigate(NavScreen.Info);
        }
    }
}