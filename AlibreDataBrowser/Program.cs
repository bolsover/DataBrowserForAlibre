using System;
using System.Windows.Forms;

namespace Bolsover.DataBrowser;

internal static class Program
{
    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        // force initialisation of connector here to prevent later problems
        new AlibreConnector();
        Application.Run(new DataBrowserForm());
    }
}