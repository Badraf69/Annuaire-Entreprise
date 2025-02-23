using System.Windows;
using System.Windows.Controls;
using AnnuaireModel;

namespace Annuaire.Views;

public partial class AddSitePage : Page
{
    private Site _site;
    public AddSitePage()
    {
        InitializeComponent();
        DataContext = new AddSiteViewModel();
    }
    private void GoBack_Click(object sender, RoutedEventArgs e)
    {

        ListSitePage listSitePage = new ListSitePage();
        Annuaire.NavigationService.Instance.GoBack();
    }
}