using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Annuaire.Services;
using Annuaire.ViewModel;
using AnnuaireModel;

namespace Annuaire.Views;

public class NavigationViewModel
{
    public ICommand NavigateToListServiceCommand { get; set; }
    public ICommand NavigateToListSiteCommand { get; set; }
    public ICommand NavigateToMenuCommand { get; set; }
    public ICommand NavigateToListEmployeeCommand { get; set; }
    public ICommand NavigateToAddSiteCommand { get; set; }
    public ICommand NavigateToAddEmployeeCommand { get; set; }
    public ICommand NavigateToAddUserCommand { get; set; }
    public ICommand NavigateToUpdateEmployeeCommand { get; set; }
    public ICommand NavigateToListUserCommand { get; set; }
    

    public NavigationViewModel()
    {
        NavigateToListEmployeeCommand = new RelayCommand(_ => NavigateToListEmployee());
        NavigateToListSiteCommand = new RelayCommand(_ => NavigateToListSite());
        NavigateToMenuCommand = new RelayCommand(_ => NavigateToMenu());
        NavigateToListServiceCommand = new RelayCommand(_ => NavigateToListService());
        NavigateToAddSiteCommand = new RelayCommand(_ => NavigateToAddSite());
        NavigateToAddEmployeeCommand = new RelayCommand(_ => NavigateToAddEmployee());
        NavigateToAddUserCommand = new RelayCommand(_ => NavigateToAddUser());
        NavigateToListUserCommand = new RelayCommand(_ => NavigateToListUser());


    }
    private void NavigateToListEmployee()
    {
        NavigationService.Navigate(new ListeEmployeePage());
    }

    public void NavigateToListSite()
    {
        NavigationService.Navigate(new ListSitePage());
    }

    public void NavigateToMenu()
    {
        NavigationService.Navigate(new MainPage());
    }
    private void NavigateToListService()
    {
        
        NavigationService.Navigate(new ListServicePage());
    }

    private void NavigateToAddSite()
    {
        NavigationService.Navigate(new AddSitePage());
    }

    public void NavigateToAddEmployee()
    {
        NavigationService.Navigate(new AddEmployeePage());
    }

    public void NavigateToAddUser()
    {
        NavigationService.Navigate(new AddUser());
    }

    public void NavigateToUpdateEmployee(Employee employee, EmployeeService employeeService)
    {
        NavigationService.Navigate(new UpdateEmployeePage(employee,employeeService));
    }

    public void NavigateToListUser()
    {
        NavigationService.Navigate(new ListUserPage());
    }
}