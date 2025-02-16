using System.Configuration;
using System.Data;
using System.Windows;
using Annuaire.Views;

namespace Annuaire;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    
    public static NavigationViewModel NavigationVM { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        NavigationVM = new NavigationViewModel();
    }
}