using System.ComponentModel;
using System.Runtime.CompilerServices;
using AnnuaireModel;

namespace Annuaire.Views;

public class AddEmployeeViewModel : INotifyPropertyChanged
{
    private  Employee _employee;
    
    public event PropertyChangedEventHandler PropertyChanged;

    public AddEmployeeViewModel()
    {
        
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
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}