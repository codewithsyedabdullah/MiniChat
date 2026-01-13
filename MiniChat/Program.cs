using System;
using System.Windows.Forms;

namespace MiniChat
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MiniChat.Forms.Form1());
        }
    }
}
