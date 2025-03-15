using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
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
    private  Service _selectedService;
    private ObservableCollection<Site> _sites;
    private readonly SiteService _siteService;
    private Site _selectedSite;
    private string _searchTerm;
    private List<Employee> _allEmployees;


    public ListEmployeeViewModel(EmployeeService employeeService)
    {
        _employeeService = employeeService;
        _serviceService = new ServiceService();
        _siteService = new SiteService();
        
        Employees = new ObservableCollection<Employee>();
        Services = new ObservableCollection<Service>();
        Sites = new ObservableCollection<Site>();
        LoadEmployees();
        LoadServices();
        LoadSites();
        ResetServiceFilterCommand = new RelayCommand(_=>ResetServiceFilter());
        ResetSiteCommand = new RelayCommand(_ => ResetSiteFilter());
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
    public ObservableCollection<Site> Sites
    {
        get => _sites;
        set
        {
            _sites = value;
            OnPropertyChanged();
        }
    }
    public Site SelectedSite
    {
        get => _selectedSite;
        set
        {
            _selectedSite = value;
            OnPropertyChanged();
            ApplyFilters();
        }
    }

    public Service SelectedService
    {
        get => _selectedService;
        set
        {
            _selectedService = value;
            OnPropertyChanged();
            ApplyFilters();
        }
    }
    public string SearchTerm
    {
        get => _searchTerm;
        set
        {
            _searchTerm = value;
            OnPropertyChanged();
            searchFilter();
        }
    }

    private void searchFilter()
    {
        if (string.IsNullOrEmpty(SearchTerm))
        {
            Employees = new ObservableCollection<Employee>(_allEmployees);
        }
        else
        {
            var filteredEmployees = _allEmployees.Where(e => e.FirstName.ToLower().Contains(SearchTerm.ToLower()) || 
                                                         e.LastName.ToLower().Contains(SearchTerm.ToLower())).ToList();
            Employees = new ObservableCollection<Employee>(filteredEmployees);
        }
        
    }

    private void ApplyFilters()
    {
        var filteredEmployees = _allEmployees.Where(e =>
                       (SelectedSite == null || SelectedSite.Id == -1 || e.Site?.Id == SelectedSite?.Id) &&
                       (SelectedService == null || SelectedService.Id == -1 || e.Service?.Id == SelectedService?.Id)).ToList();
                   
        Employees = new ObservableCollection<Employee>(filteredEmployees); 

    }

    private void ResetServiceFilter()
    {
        SelectedService = Services.FirstOrDefault(s=>s.Id == -1);
        ApplyFilters();
    }

    private void ResetSiteFilter()
    {
        SelectedSite = Sites.FirstOrDefault(s => s.Id == -1);
        ApplyFilters();
    }
    public ICommand NavigateToFicheEmployeeCommand { get; }
    public ICommand NavigateToAddEmployeeCommand { get; }
    public ICommand NavigateToServiceCommand { get; }
    public ICommand NavigateToSiteCommand { get; }
    public ICommand NavigateToMenuCommand { get; }
    public ICommand ResetServiceFilterCommand { get; }
    public ICommand ResetSiteCommand { get; }
    

    private async void LoadEmployees()
    {
        var employees = await _employeeService.GetEmployeesAsync();
        _allEmployees = employees.ToList();
        Employees = new ObservableCollection<Employee>(employees);
    }

    private async void LoadServices()
    {
        var services =  await _serviceService.GetServicesAsync();
        services.Insert(0, new Service { Id = -1, ServiceName  = "Tous les services" });
        Services = new ObservableCollection<Service>(services);
        
    }
    private async void LoadSites()
    {
        var sites =  await _siteService.GetSitesAsync();
        sites.Insert(0, new Site { Id = -1, Ville = "Tous les sites" });
        Sites = new ObservableCollection<Site>(sites);
        
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

