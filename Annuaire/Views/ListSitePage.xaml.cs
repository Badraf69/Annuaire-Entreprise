using System.Windows;
using System.Windows.Controls;
using Annuaire.Services;
using Annuaire.ViewModel;

namespace Annuaire.Views;

public partial class ListSitePage : Page
{
    public ListSitePage()
    {
        InitializeComponent();
        var siteService = new SiteService();
        DataContext = new ListSiteViewModel(siteService);
        
    }
    private void BtnRetour_Click(object sender, RoutedEventArgs e)
    {
        Annuaire.NavigationService.Instance.GoBack();
    }
}