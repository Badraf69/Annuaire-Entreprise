using System.Windows;
using System.Windows.Controls;
using AnnuaireModel;

namespace Annuaire.Views;

public partial class AddEmployeePage : Page
{
    private Employee _employee;
    public AddEmployeePage()
    {
        InitializeComponent();
        DataContext = new AddEmployeeViewModel();
    }
    private void GoBack_Click(object sender, RoutedEventArgs e)
    {

            ListeEmployeePage listEmployeePage = new ListeEmployeePage();
            NavigationServiceSingleton.Instance.GoBack();
    }
}