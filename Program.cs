using System;
using System.Windows.Forms;
using System.Diagnostics;

static class Program
{
    [STAThread]
    static void Main()
    {
        /*try
        {
            BetterAntiDump.Initialize();
            AntiDumpRun.Initialize();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
        catch
        {
            Process.GetCurrentProcess().Kill();
            return;
        }*/

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());
    }
}