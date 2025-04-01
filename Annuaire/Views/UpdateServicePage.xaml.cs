using System.Windows;
using System.Windows.Controls;
using Annuaire.Services;
using Annuaire.ViewModel;
using AnnuaireModel;

namespace Annuaire.Views;

public partial class UpdateServicePage : Page
{
    public UpdateServicePage(Service service, ServiceService serviceService)
    {
        InitializeComponent();
        if (service == null)
        {
            MessageBox.Show( "Problème lors du chargement du site." );
            NavigationService.Navigate(new ListServicePage());
        }
        else
        {
            DataContext = new UpdateServiceViewModel(service, serviceService);

        }
    }
    private void GoBack_Click(object sender, RoutedEventArgs e)
    {

        ListServicePage listServicePage = new ListServicePage();
        Annuaire.NavigationService.Instance.GoBack();
    }
}