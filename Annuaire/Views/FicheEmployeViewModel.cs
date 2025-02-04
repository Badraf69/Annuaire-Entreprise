﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Annuaire.Services;
using AnnuaireModel;

namespace Annuaire.Views;

public class FicheEmployeViewModel : INotifyPropertyChanged
{
    private readonly EmployeeService _employeeService;
    private Employee _employee;
    
    public event PropertyChangedEventHandler? PropertyChanged;
    

    public FicheEmployeViewModel(Employee employee, EmployeeService employeeService)
    {
        _employeeService = employeeService;
        Employee = employee;
        SupprimerCommand = new RelayCommand(async _ => await SupprimerEmploye(), _ => Employee != null);
        
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
    private async Task SupprimerEmploye()
    {
        if (_employeeService == null) return;
        var result = MessageBox.Show(
            "Voulez-vous vraiment supprimer cert employé ?",
            "Confirmation",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);
        if (result == MessageBoxResult.Yes)
        {
            bool isDeleted = await _employeeService.DeleteEmployeeAsync(Employee.Id);
            if (isDeleted)
            {
                MessageBox.Show("Employé supprimé avec succès", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationServiceSingleton.MainFrame.GoBack();
            }
            else
            {
                MessageBox.Show("Erreur lors de la suppresion", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);  
            }
        }
    }
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}