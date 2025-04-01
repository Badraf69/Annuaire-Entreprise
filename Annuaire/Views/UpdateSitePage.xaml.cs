using System.Windows;
using System.Windows.Controls;
using Annuaire.Services;
using AnnuaireModel;

namespace Annuaire.Views;

public partial class UpdateSitePage : Page
{
    public UpdateSitePage(Site site, SiteService siteService)
    {
        InitializeComponent();
        if (site == null)
        {
            MessageBox.Show( "Problème lors du chargement du site." );
            NavigationService.Navigate(new ListSitePage());
        }
        else
        {
            DataContext = new UpdateSiteViewModel(site, siteService);

        }
    }
    private void GoBack_Click(object sender, RoutedEventArgs e)
    {

        ListSitePage listSitePage = new ListSitePage();
        Annuaire.NavigationService.Instance.GoBack();
    }
}