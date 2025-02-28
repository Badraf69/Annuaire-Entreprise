using System.Windows;
using System.Windows.Controls;
using Annuaire.Services;
using AnnuaireModel;

namespace Annuaire.Views;

public partial class ListUserPage : Page
{
    public ListUserPage()
    {
        InitializeComponent();
        var userService = new UserService();
        var user = new User();
        DataContext = new ListUserViewModel(userService);
    }
    private void BtnRetour_Click(object sender, RoutedEventArgs e)
    {
        Annuaire.NavigationService.Instance.GoBack();
    }
}