using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Annuaire.Services;
using AnnuaireModel;

namespace Annuaire.Views;

public class ListServiceViewModel : INotifyPropertyChanged
{

    private readonly ServiceService _serviceService;
    private ObservableCollection<Service> _services;

    public ListServiceViewModel(ServiceService serviceService)
    {
        _serviceService = serviceService;
        LoadService();
        Services = new ObservableCollection<Service>();
        NavigateToListEmployeeCommand = App.NavigationVM.NavigateToListEmployeeCommand;
        NavigateToListSiteCommand = App.NavigationVM.NavigateToListSiteCommand;
        NavigateToMenuCommand = App.NavigationVM.NavigateToMenuCommand;
        NavigateToAddServiceCommand = new RelayCommand(_ => OpenAddServiceWindow());


    }
    // public event Action OnNavigateToListEmployee;
    // public event Action OnNavigateToListSite;
    // public event Action OnNavigateToMenu;
    public event PropertyChangedEventHandler PropertyChanged;
    
    public ObservableCollection<Service> Services
    {
        get => _services;
        set
        {
            _services = value;
            OnPropertyChanged();
        }
    }
    
    public ICommand NavigateToListEmployeeCommand { get; }
    public ICommand NavigateToListSiteCommand { get; }
    public ICommand NavigateToMenuCommand { get; }
    public ICommand NavigateToAddServiceCommand { get; set; }
    
    public async void LoadService()
    {
        var services = await _serviceService.GetServicesAsync();
        Services = new ObservableCollection<Service>(services);
    }
    
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void OpenAddServiceWindow()
    {
        var window = new AddServiceWindow();
        bool? result = window.ShowDialog();
        if (result == true)
        {
            string serviceName = window.ServiceName;
        }
    }
}
