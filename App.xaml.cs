using System.Configuration;
using System.Data;
using System.Windows;

namespace WPFTutorial
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Contains("--verify-assets"))
            {
                foreach (var name in Enum.GetValues<Items.SoundEnums>())
                    if (name != Items.SoundEnums.none) _ = new Items.Sound(name);
                Shutdown(0);
                return;
            }
            base.OnStartup(e);
        }
    }

}
