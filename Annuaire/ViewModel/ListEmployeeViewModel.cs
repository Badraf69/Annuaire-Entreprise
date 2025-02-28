using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Navigation;
using Annuaire.Services;
using Annuaire.ViewModel;
using AnnuaireModel;

namespace Annuaire.Views;

public class ListEmployeeViewModel : BaseViewModel
{
    private readonly EmployeeService _employeeService;
    private ObservableCollection<Employee> _employees;
    private Employee _selectedEmployee;
    private ObservableCollection<Service> _services;
    private readonly ServiceService _serviceService;


    public ListEmployeeViewModel(EmployeeService employeeService)
    {
        _employeeService = employeeService;
        _serviceService = new ServiceService();
        Employees = new ObservableCollection<Employee>();
        Services = new ObservableCollection<Service>();
        LoadEmployees();
        NavigateToFicheEmployeeCommand =
            new RelayCommand(_ => NavigateToFicheEmployee(), _ => SelectedEmployee != null);
        NavigateToAddEmployeeCommand = 
            new RelayCommand(_ => NavigateToAddEmployee());
        NavigateToServiceCommand = App.NavigationVM.NavigateToListServiceCommand;
        NavigateToSiteCommand = App.NavigationVM.NavigateToListSiteCommand;
        NavigateToMenuCommand = App.NavigationVM.NavigateToMenuCommand;
        
    }
    
    public ObservableCollection<Employee> Employees
    {
        get => _employees;
        set
        {
            _employees = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Service> Services
    {
        get => _services;
        set
        {
            _services = value;
            OnPropertyChanged();
        }
    }
    
    public ICommand NavigateToFicheEmployeeCommand { get; }
    public ICommand NavigateToAddEmployeeCommand { get; }
    public ICommand NavigateToServiceCommand { get; }
    public ICommand NavigateToSiteCommand { get; }
    public ICommand NavigateToMenuCommand { get; }

    private async void LoadEmployees()
    {
        var employees = await _employeeService.GetEmployeesAsync();
        Employees = new ObservableCollection<Employee>(employees);
    }
    public Employee SelectedEmployee
    {
        get => _selectedEmployee;
        set
        {
            _selectedEmployee = value;
            Console.WriteLine($"Selected employee : {_selectedEmployee}");
            OnPropertyChanged();
            
        }
    }

    private async Task NavigateToFicheEmployee()
    {
        if (SelectedEmployee != null)
        {
            NavigationService.Navigate(new FicheEmployee(_selectedEmployee, _employeeService));
        }
    }
    private async Task NavigateToAddEmployee()
    {
        NavigationService.Navigate(new AddEmployeePage());
    }
    private async void Service()
    {
        var services = await _serviceService.GetServicesAsync();
    }
    
}

