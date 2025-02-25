using System.ComponentModel;
using System.Runtime.CompilerServices;
using Annuaire.Services;
using AnnuaireModel;

namespace Annuaire.Views;

public class UpdateEmployeeViewModel : INotifyPropertyChanged
{
    private readonly EmployeeService _employeeService;
    private Employee _employee;
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}