using System;
using System.Windows.Forms;

namespace Rando
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Rando());
        }
    }
}
