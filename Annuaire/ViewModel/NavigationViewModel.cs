using System.Windows.Input;
using Annuaire.ViewModel;

namespace Annuaire.Views;

public class NavigationViewModel
{
    public ICommand NavigateToListServiceCommand { get; set; }
    public ICommand NavigateToListSiteCommand { get; set; }
    public ICommand NavigateToMenuCommand { get; set; }
    public ICommand NavigateToListEmployeeCommand { get; set; }

    public NavigationViewModel()
    {
        NavigateToListEmployeeCommand = new RelayCommand(_ => NavigateToListEmployee());
        NavigateToListSiteCommand = new RelayCommand(_ => NavigateToListSite());
        NavigateToMenuCommand = new RelayCommand(_ => NavigateToMenu());
        NavigateToListServiceCommand = new RelayCommand(_ => NavigateToListService());

    }
    private void NavigateToListEmployee()
    {
        NavigationServiceSingleton.Navigate(new ListeEmployeePage());
    }

    public void NavigateToListSite()
    {
        NavigationServiceSingleton.Navigate(new ListSitePage());
    }

    public void NavigateToMenu()
    {
        NavigationServiceSingleton.Navigate(new MainPage());
    }
    private void NavigateToListService()
    {
        
        NavigationServiceSingleton.Navigate(new ListServicePage());
    }
}