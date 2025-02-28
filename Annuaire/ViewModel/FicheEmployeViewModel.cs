using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Annuaire.Services;
using AnnuaireModel;

namespace Annuaire.Views;

public class FicheEmployeViewModel : BaseViewModel
{
    private readonly EmployeeService _employeeService;
    private Employee _employee;
    

    public FicheEmployeViewModel(Employee employee, EmployeeService employeeService)
    {
        _employeeService = employeeService;
        Employee = employee;
        SupprimerCommand = new RelayCommand(async _ => await SupprimerEmploye(), _ => Employee != null);
        NavigateToListEmployeeCommand = App.NavigationVM.NavigateToListEmployeeCommand;
        
        
    }

    public Employee Employee
    {
        get => _employee;
        set
        {
            _employee = value;
            OnPropertyChanged();
        }
    }
    public ICommand SupprimerCommand { get; }
    public ICommand NavigateToListEmployeeCommand { get; }
    private async Task SupprimerEmploye()
    {
        if (_employeeService == null) return;
        var result = MessageBox.Show(
            "Voulez-vous vraiment supprimer l'employé ?",
            "Confirmation",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            bool isDeleted = await _employeeService.DeleteEmployeeAsync(Employee.Id);
            if (isDeleted)
            {
                MessageBox.Show("Employé supprimé avec succès", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new ListeEmployeePage());
                
            }
            else
            {
                MessageBox.Show("Erreur lors de la suppresion", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);  
            }
        }
    }
}