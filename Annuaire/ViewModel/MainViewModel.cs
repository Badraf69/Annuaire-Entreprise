using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AnnuaireModel;

namespace Annuaire.Views;

public class MainViewModel : BaseViewModel
{
    public LoginViewModel Login { get; set; }
    public ICommand LoginCommand { get; set; }
    public ICommand LogoutCommand { get; set; }
    //Navigation
    public MainViewModel()
    {
        NavigateToListEmployeeCommand = App.NavigationVM.NavigateToListEmployeeCommand;
        NavigateToAddEmployeeCommand = App.NavigationVM.NavigateToAddEmployeeCommand;
        NavigateToListServiceCommand = App.NavigationVM.NavigateToListServiceCommand;
        NavigateToListSiteCommand = App.NavigationVM.NavigateToListSiteCommand;
        NavigateToAddSiteCommand = App.NavigationVM.NavigateToAddSiteCommand;
        NavigateToAddUserCommand = App.NavigationVM.NavigateToAddUserCommand;
        NavigateToListUserCommand = App.NavigationVM.NavigateToListUserCommand;
        
        Login = new LoginViewModel();
        LoginCommand = new RelayCommand(_=>LoginUser());
        LogoutCommand = new RelayCommand(_=>LogoutUser());
    }

    //Relay command
    public ICommand NavigateToListEmployeeCommand { get; }
    public ICommand NavigateToAddEmployeeCommand { get; }
    public ICommand NavigateToListServiceCommand { get; }
    public ICommand NavigateToListSiteCommand { get; }
    public ICommand NavigateToAddSiteCommand { get; }
    public ICommand NavigateToAddUserCommand { get; }
    public ICommand NavigateToListUserCommand { get; }


    
    //Fonction pour la gestion de connection et déconnection des utilisateurs
    private void LoginUser()
    {
        Login.IsLoginLoggedIn = true;
    }

    private void LogoutUser()
    {
        Login.IsLoginLoggedIn = false;
    }

}