using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Annuaire.Views;

public class MainViewModel 
{
    public MainViewModel()
    {
        NavigateToListEmployee = App.NavigationVM.NavigateToListEmployeeCommand;
        //NavigateToAddEmployeeCommand = App.NavigationVM.NavigateToAddEmployeeCommand;
        NavigateToListServiceCommand = App.NavigationVM.NavigateToListServiceCommand;
        //NavigateToAddServiceCommand = App.NavigationVM.NavigateToAddServiceCommand;
        NavigateToListSiteCommand = App.NavigationVM.NavigateToListSiteCommand;
        //NavigateToAddSiteCommand = App.NavigationVM.NavigateToAddSiteCommand;

    }
    public ICommand NavigateToListEmployee { get; }
    public ICommand NavigateToAddEmployeeCommand { get; }
    public ICommand NavigateToListServiceCommand { get; }
    public ICommand NavigateToAddServiceCommand { get; }
    public ICommand NavigateToListSiteCommand { get; }
    public ICommand NavigateToAddSiteCommand { get; }
    

}