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
        //var siteService = new SiteService();
        DataContext = new ListSiteViewModel(new SiteService());
        
    }
    private void SiteListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DataContext is ListSiteViewModel viewModel && viewModel.SelectedSite != null)
        {
           viewModel.NavigateToUpdateSiteCommand.Execute(null);
        }
    }
    private void BtnRetour_Click(object sender, RoutedEventArgs e)
    {
        Annuaire.NavigationService.Instance.GoBack();
    }
}