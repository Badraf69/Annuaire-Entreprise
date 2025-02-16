using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Annuaire.Services;
using Annuaire.Views;

namespace Annuaire.ViewModel;

public partial class ListServicePage : Page
{
    public ListServicePage()
    {
        InitializeComponent();
        var serviceService = new ServiceService();
        DataContext = new ListServiceViewModel(serviceService);
        
    }
    private void BtnRetour_Click(object sender, RoutedEventArgs e)
    {
        NavigationServiceSingleton.Instance.GoBack();
    }


}