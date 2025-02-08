using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Navigation;
using Annuaire.Services;
using AnnuaireModel;

namespace Annuaire.Views;

public class ListEmployeeViewModel : INotifyPropertyChanged
{
    private readonly EmployeeService _employeeService;
    private ObservableCollection<Employee> _employees;
    private Employee _selectedEmployee;


    public ListEmployeeViewModel(EmployeeService employeeService)
    {
        _employeeService = employeeService;
        Employees = new ObservableCollection<Employee>();
        LoadEmployees();
        NavigateToFicheEmployeeCommand =
            new RelayCommand(_ => NavigateToFicheEmployee(), _ => SelectedEmployee != null);
        NavigateToAddEmployeeCommand = 
            new RelayCommand(_ => NavigateToAddEmployee());
    }
    
    public event Action OnNavigateToFicheEmployee;
    public event Action OnNavigateToAddEmployeeCommand;
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public ObservableCollection<Employee> Employees
    {
        get => _employees;
        set
        {
            _employees = value;
            OnPropertyChanged();
        }
    }
    
    
    public ICommand NavigateToFicheEmployeeCommand { get; }
    public ICommand LoadEmployeesCommand { get; }
    public ICommand NavigateToAddEmployeeCommand { get; }

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
            NavigationServiceSingleton.Navigate(new FicheEmployee(_selectedEmployee, _employeeService));
        }
    }
    private async Task NavigateToAddEmployee()
    {
        NavigationServiceSingleton.Navigate(new AddEmployeePage());
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    
}

public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Predicate<object> _canExecute;

    public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
        
    }
    
    public event EventHandler? CanExecuteChanged;


    public bool CanExecute(object? parameter)=> _canExecute == null || _canExecute(parameter);
    public void Execute(object? parameter)=> _execute(parameter);
}
