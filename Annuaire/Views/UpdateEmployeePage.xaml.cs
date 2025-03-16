using System.Windows;
using System.Windows.Controls;
using Annuaire.Services;
using AnnuaireModel;

namespace Annuaire.Views;

public partial class UpdateEmployeePage : Page
{
    public UpdateEmployeePage( Employee employee, EmployeeService employeeService )
    {
        InitializeComponent();
        if (employee == null)
        {
            MessageBox.Show( "Problème lors du chargement de l'employee." );
            NavigationService.Navigate(new ListeEmployeePage());
        }
        else
        {
            DataContext = new UpdateEmployeeViewModel(employeeService, employee);
        }
    }
    private void GoBack_Click(object sender, RoutedEventArgs e)
    {
        Annuaire.NavigationService.Instance.GoBack();
    }
}