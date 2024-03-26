using System.Configuration;
using System.Data;
using System.Windows;

namespace Ukol_DatabaseWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // doinstalovat System.Data.SQLite
            base.OnStartup(e);
            DatabaseManager.Initialize();
        }
    }
}