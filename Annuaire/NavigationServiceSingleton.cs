using System.Windows.Controls;

namespace Annuaire;

public class NavigationServiceSingleton
{
    // private static NavigationServiceSingleton _instance;
    // public static NavigationServiceSingleton Instance => _instance ??= new NavigationServiceSingleton();
    public static Frame MainFrame { get; set; }
    // private NavigationServiceSingleton(){}
    public static void GoBack()
    {
        if (MainFrame?.CanGoBack == true)
        {
            MainFrame.GoBack();
        }
    }
    public static void Navigate(Page page)
    {
        MainFrame?.Navigate(page);
    }
    
}