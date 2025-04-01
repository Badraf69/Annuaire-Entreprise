using System.Windows.Input;
using Annuaire.Services;
using AnnuaireModel;

namespace Annuaire.Views;

public class UpdateSiteViewModel : BaseViewModel
{
    
    private  Site _site;
    private readonly SiteService _siteService;


    public UpdateSiteViewModel(Site site, SiteService siteService)
    {
        
        Site = site;
        _siteService = siteService;
        UpdateSiteCommand = new RelayCommand(async (_) => await UpdateSite());
    }
    public ICommand UpdateSiteCommand { get; }

    public Site Site
    {
        get => _site;
        set
        {
            _site = value;
            OnPropertyChanged();
        }
    }
    private async Task UpdateSite()
    {
        try
        {
            var updatedSite = await _siteService.UpdateSite(Site);
            if (updatedSite != null)
            {
                Site = updatedSite;
                NavigationService.Navigate(new ListSitePage());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}