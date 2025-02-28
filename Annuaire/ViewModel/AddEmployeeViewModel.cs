using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Annuaire.Services;
using AnnuaireModel;

namespace Annuaire.Views;

public class AddEmployeeViewModel : BaseViewModel
{
    private  Employee _employee;
    private readonly EmployeeService _employeeService;
    private readonly ServiceService _serviceService;
    private readonly SiteService  _siteService;
    
    public ICommand AddEmployeeCommand { get; }
    public ICommand GoBackCommand { get; }

    public AddEmployeeViewModel()
    {
        _employeeService = new EmployeeService();
        _serviceService = new ServiceService();
        _siteService = new SiteService();
        Employee = new Employee();
        Services = new ObservableCollection<Service>();
        Sites = new ObservableCollection<Site>();
        AddEmployeeCommand = new RelayCommand(async (_) => await AddEmployee());
        ChargedData();
    }
    
    public ObservableCollection<Service> Services { get; set; }
    public ObservableCollection<Site> Sites { get; set; }

    public Employee Employee
    {
        get => _employee;
        set
        {
            _employee = value;
            OnPropertyChanged();
        }
    }

    public async Task AddEmployee()
    {
        if (string.IsNullOrWhiteSpace(Employee.FirstName) || string.IsNullOrWhiteSpace(Employee.LastName) || string.IsNullOrWhiteSpace(Employee.Email))
        {
            MessageBox.Show("Tous les champs obligatoire doivent être remplie.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        
        var addedEmployee = await _employeeService.AddEmployee(Employee);
        if (addedEmployee != null)
        {
            MessageBox.Show("Employé ajouté avec Succès.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new ListeEmployeePage());

        }
        
    }

    public async void ChargedData()
    {
        var serviceList = await _serviceService.GetServicesAsync();
        foreach (var service in serviceList)   
        {
            Services.Add(service);
        }
        var sitesList = await _siteService.GetSitesAsync();
        foreach (var site in sitesList)
        {
            Sites.Add(site);
        }
    }
    
}