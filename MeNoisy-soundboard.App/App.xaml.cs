using MeNoisySoundboard.App.Logic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MeNoisySoundboard.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Exit += App_Exit;
            HotkeyHandler.SetHook();
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            HotkeyHandler.UnHook();
        }
    }
}
