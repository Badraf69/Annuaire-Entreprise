using System.Windows.Controls;

namespace Annuaire;

public class NavigationServiceSingleton
{
    private static NavigationServiceSingleton _instance;
    private static Frame MainFrame { get; set; }

    private  NavigationServiceSingleton () {}

    public static NavigationServiceSingleton Instance
    {
        get
        {
            if (_instance == null)
                _instance = new NavigationServiceSingleton();
            return _instance;
        }
    }

    public void Initialize(Frame frame)
    {
        MainFrame = frame;
    }
    public void GoBack()
    {
        if (MainFrame != null && MainFrame.CanGoBack)
        {
            MainFrame.GoBack();
        }
    }
    public static void Navigate(Page page)
    {
        MainFrame?.Navigate(page);
    }

    public void GoToFirstPage(Page page)
    {
        MainFrame?.Navigate(page);
    }
    
}