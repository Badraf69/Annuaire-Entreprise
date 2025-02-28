using System.ComponentModel;
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
    
    public ICommand AddSiteCommand { get; }
    


    public AddSiteViewModel()
    {
        _siteService = new SiteService();
        Site = new Site();
        AddSiteCommand = new RelayCommand(async (_) => await AddSite());
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
        if (string.IsNullOrWhiteSpace(Site.Ville) || string.IsNullOrWhiteSpace(Site.Type))
        {
            MessageBox.Show("Tous les champs sont obligatoire.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            var addedSite = await _siteService.AddSite(Site);
            if (addedSite != null)
            {
                MessageBox.Show("Site ajouté avec Succès", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new ListSitePage());
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erreur lors de l'ajout du site :{ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        
    }
}