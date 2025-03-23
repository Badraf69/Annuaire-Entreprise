using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Annuaire.Services;
using AnnuaireModel;

namespace Annuaire.Views;

public class AddSiteViewModel : BaseViewModel
{
    private Site _site;
    private readonly SiteService _siteService;
    private ObservableCollection<Site> _sites;
    
    public ICommand AddSiteCommand { get; }
    


    public AddSiteViewModel()
    {
        _siteService = new SiteService();
        Site = new Site();
        Sites = new ObservableCollection<Site>();
        AddSiteCommand = new RelayCommand(async (_) => await AddSite());
        LoadSites();
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

    private async void LoadSites()
    {
        var siteList = await _siteService.GetSitesAsync();
        if (siteList != null)
        {
            Sites = new ObservableCollection<Site>(siteList);
        }
    }
    public Site Site
    {
        get => _site;
        set
        {
            _site = value;
            OnPropertyChanged();
        }
    }

    public async Task AddSite()
    {
        try
        {
            // Vérification si l'objet Site est null
            if (Site == null)
            {
                MessageBox.Show("Les données du site sont invalides.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Vérification des champs obligatoires
            if (string.IsNullOrWhiteSpace(Site.Ville) || string.IsNullOrWhiteSpace(Site.Type))
            {
                MessageBox.Show("Tous les champs sont obligatoires.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            // Vérification de la duplication du site
            if (Sites.Any(s=>s.Ville == Site.Ville))
            {
                
                MessageBox.Show("Un site portant ce nom existe déjà.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Ajout du site via le service
            var addedSite = await _siteService.AddSite(Site);
            if (addedSite != null)
            {
                MessageBox.Show("Site ajouté avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                // Redirection vers la liste des sites
                NavigationService.Navigate(new ListSitePage());
            }
            else
            {
                MessageBox.Show("L'ajout du site a échoué. Veuillez réessayer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (HttpRequestException httpEx)
        {
            MessageBox.Show($"Erreur réseau : {httpEx.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Une erreur inattendue est survenue : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        // try
        // {
        //     if (string.IsNullOrWhiteSpace(Site.Ville) || string.IsNullOrWhiteSpace(Site.Type))
        //      {
        //          MessageBox.Show("Tous les champs sont obligatoire.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
        //          return;
        //      }
        //
        //      if (Sites.Any(s => s.Type.ToLower() == Site.Type.ToLower()))
        //      {
        //          MessageBox.Show("Un site portant ce nom existe déjà", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information);
        //          return;
        //      }
        //     var addedSite = await _siteService.AddSite(Site);
        //     if (addedSite != null)
        //     {
        //         MessageBox.Show("Site ajouté avec Succès", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        //         NavigationService.Navigate(new ListSitePage());
        //     }
        // }
        // catch (Exception ex)
        // {
        //     MessageBox.Show($"Erreur lors de l'ajout du site :{ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        // }
        
    }
}