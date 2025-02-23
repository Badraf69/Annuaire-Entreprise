using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Pkcs;
using System.Windows.Input;
using Annuaire.Services;
using Annuaire.ViewModel;
using AnnuaireModel;

namespace Annuaire.Views;

public class ListSiteViewModel :INotifyPropertyChanged
{
    private readonly SiteService _siteService;
    private ObservableCollection<Site> _sites;

    public ListSiteViewModel(SiteService siteService)
    {
        _siteService = siteService;
        Sites = new ObservableCollection<Site>();
        
        NavigateToListServiceCommand = App.NavigationVM.NavigateToListServiceCommand;
        NavigateToListEmployeeCommand = App.NavigationVM.NavigateToListEmployeeCommand;
        NavigateToMenuCommand = App.NavigationVM.NavigateToMenuCommand;
        NavigateToAddSiteCommand = App.NavigationVM.NavigateToAddSiteCommand;
        
        LoadSites();
    }
    
    // public event Action OnNavigateToListService;
    // public event Action OnNavigateToListEmployee;
    // public event Action OnNavigateToMenu;
    public event PropertyChangedEventHandler PropertyChanged;
    public ObservableCollection<Site> Sites
    {
        get => _sites;
        set
        {
            _sites = value;
            OnPropertyChanged();
        }
    }

    public ICommand NavigateToListServiceCommand { get; set; }
    public ICommand NavigateToListEmployeeCommand { get; set; }
    public ICommand NavigateToMenuCommand { get; set; }
    public ICommand NavigateToAddSiteCommand { get; set; }

    //Fonctions pour les données
    private async void LoadSites()
    {
        var sites = await _siteService.GetSitesAsync();
        Sites = new ObservableCollection<Site>(sites);
    }

    
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}