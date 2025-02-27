using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Annuaire.Services;
using Annuaire.ViewModel;
using AnnuaireModel;

namespace Annuaire.Views;

public class ListServiceViewModel : INotifyPropertyChanged
{

    private readonly ServiceService _serviceService;
    private ObservableCollection<Service> _services;
    private  Service _service;
    private Service _selectedService;

    public ListServiceViewModel(ServiceService serviceService, Service service)
    {
        _serviceService = serviceService;
        _service = service;
        
        LoadService();
        Services = new ObservableCollection<Service>();
        NavigateToListEmployeeCommand = App.NavigationVM.NavigateToListEmployeeCommand;
        NavigateToListSiteCommand = App.NavigationVM.NavigateToListSiteCommand;
        NavigateToMenuCommand = App.NavigationVM.NavigateToMenuCommand;
        NavigateToAddServiceCommand = new RelayCommand(_ => OpenAddServiceWindow());
        DeleteServiceCommand = new AsyncRelayCommand(
            async () => await DeleteService(),
            () => SelectedService != null
        );

    }

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

    public Service Service
    {
        get => _service;
        set
        {
            _service = value;
            OnPropertyChanged();
            
        }
    }

    public Service SelectedService
    {
        get => _selectedService;
        set
        {
            _selectedService = value;
            OnPropertyChanged();
            ((AsyncRelayCommand)DeleteServiceCommand).RaiseCanExecuteChanged();
        }
    }
    
    public ICommand NavigateToListEmployeeCommand { get; }
    public ICommand NavigateToListSiteCommand { get; }
    public ICommand NavigateToMenuCommand { get; }
    public ICommand NavigateToAddServiceCommand { get; set; }
    public ICommand DeleteServiceCommand { get; set; }
    
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
            _service.ServiceName = serviceName;
            var addService =  _serviceService.AddServiceAsync(_service);
            if (addService != null)
            {
                MessageBox.Show("Service ajouté avec Succès.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new ListServicePage());

            }
        }
    }
    private async Task DeleteService()
    {
        if (SelectedService == null) return;
        var result = MessageBox.Show($"Voulez-vous vraiment supprimer '{SelectedService.ServiceName}?", 
            "Confirmation", 
            MessageBoxButton.YesNo, 
            MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            int isDeleted = await _serviceService.DeleteServiceAsync(SelectedService.Id);
            if (new []{200,201,204}.Contains(isDeleted))
            {
                MessageBox.Show("Service supprimé avec succès", "Success", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information);
                Services.Remove(SelectedService);
                SelectedService = null;
            }
            else
            {
                if (isDeleted==501)
                {
                    MessageBox.Show($"Suppression impossible au moins 1 employe occupe ce service", "Erreur", MessageBoxButton.OK);

                }
                else
                {
                    MessageBox.Show("Erreur lors de la suppression d'un service", "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

    }
}
