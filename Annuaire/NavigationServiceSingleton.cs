using System.Windows.Controls;

namespace Annuaire;

public class NavigationServiceSingleton
{
    private static NavigationServiceSingleton _instance;
    public static NavigationServiceSingleton Instance => _instance ??= new NavigationServiceSingleton();
    public Frame MainFrame { get; set; }
    private NavigationServiceSingleton(){}

    public void Navigate(Page page)
    {
        MainFrame?.Navigate(page);
    }
}