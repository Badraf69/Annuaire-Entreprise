using System.Windows;
using System.Windows.Controls;
using Annuaire.Services;
using AnnuaireModel;

namespace Annuaire.Views;

public partial class UpdateEmployeePage : Page
{
    public UpdateEmployeePage()
    {
        InitializeComponent();
        var employeeService = new EmployeeService();
        DataContext = new UpdateEmployeePage();
    }
    private void GoBack_Click(object sender, RoutedEventArgs e)
    {

        Annuaire.NavigationService.Instance.GoBack();

    }
}