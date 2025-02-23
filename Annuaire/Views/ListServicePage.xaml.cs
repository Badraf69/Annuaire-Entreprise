using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Annuaire.Services;
using Annuaire.Views;
using AnnuaireModel;

namespace Annuaire.ViewModel;

public partial class ListServicePage : Page
{
    public ListServicePage()
    {
        InitializeComponent();
        var serviceService = new ServiceService();
        var service = new Service();
        DataContext = new ListServiceViewModel(serviceService, service);
        
    }
    private void BtnRetour_Click(object sender, RoutedEventArgs e)
    {
        Annuaire.NavigationService.Instance.GoBack();
    }


}