using System.Windows.Controls;

namespace Annuaire;

public class NavigationService
{
    private static NavigationService _instance;
    private static Frame MainFrame { get; set; }

    private  NavigationService () {}

    public static NavigationService Instance
    {
        get
        {
            if (_instance == null)
                _instance = new NavigationService();
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