using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Pkcs;
using System.Windows;
using System.Windows.Input;
using Annuaire.Services;
using Annuaire.ViewModel;
using AnnuaireModel;

namespace Annuaire.Views;

public class ListSiteViewModel :INotifyPropertyChanged
{
    private readonly SiteService _siteService;
    private ObservableCollection<Site> _sites;
    private Site _site;
    private Site _selectedSite;

    public ListSiteViewModel(SiteService siteService )
    {
        _siteService = siteService;
        Sites = new ObservableCollection<Site>();
        
        NavigateToListServiceCommand = App.NavigationVM.NavigateToListServiceCommand;
        NavigateToListEmployeeCommand = App.NavigationVM.NavigateToListEmployeeCommand;
        NavigateToMenuCommand = App.NavigationVM.NavigateToMenuCommand;
        NavigateToAddSiteCommand = App.NavigationVM.NavigateToAddSiteCommand;
        DeleteSiteCommand = new AsyncRelayCommand(
            async () => await DeleteSite(),
            () => SelectedSite != null
        );        LoadSites();
    }
    

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

    public Site SelectedSite
    {
        get => _selectedSite;
        set
        {
            _selectedSite = value;
            OnPropertyChanged();
            ((AsyncRelayCommand)DeleteSiteCommand).RaiseCanExecuteChanged();
        }
    }

    public ICommand NavigateToListServiceCommand { get; set; }
    public ICommand NavigateToListEmployeeCommand { get; set; }
    public ICommand NavigateToMenuCommand { get; set; }
    public ICommand NavigateToAddSiteCommand { get; set; }
    public ICommand DeleteSiteCommand { get; set; }

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

    private async Task DeleteSite()
    {
        if(SelectedSite == null) return;
        var result = MessageBox.Show($"Voulez-vous vraiment supprimer '{SelectedSite.Ville}?", 
            "Confirmation", 
            MessageBoxButton.YesNo, 
            MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            bool isDeleted = await _siteService.DeleteSiteAsync(SelectedSite.Id);
            if (isDeleted)
            {
                MessageBox.Show("Site supprimé avec succès", "Success", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information);
                Sites.Remove(SelectedSite);
                SelectedSite = null;
            }
            else
            {
                MessageBox.Show("Erreur lors de la suppression d'un site", "Error", 
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}