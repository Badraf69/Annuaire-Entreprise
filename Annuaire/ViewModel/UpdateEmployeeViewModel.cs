using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Annuaire.Services;
using AnnuaireModel;

namespace Annuaire.Views;

public class UpdateEmployeeViewModel : BaseViewModel
{
    private readonly EmployeeService _employeeService;
    private Employee _employee;

    public UpdateEmployeeViewModel(EmployeeService employeeService, Employee employee)
    {
        _employeeService = employeeService;
        Employee = employee;
        UpdateEmployeeCommand = new RelayCommand(async (_)=> await UpdateEmployee());
       
    }
    public ICommand UpdateEmployeeCommand { get; }
    public Employee Employee
    {
        get => _employee;
        set
        {
            _employee = value;
            OnPropertyChanged();
        }
    }

    private async  Task UpdateEmployee()
    {
        try
        {
            var updatedEmployee = await _employeeService.UpdateEmployee(Employee);
            if (updatedEmployee != null)
            {
                Employee = updatedEmployee;
                NavigationService.Navigate(new ListeEmployeePage());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

}