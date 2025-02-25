using System.Windows;
using System.Windows.Controls;
using Annuaire.Services;
using Annuaire.Views;
using AnnuaireModel;

namespace Annuaire
{

    public partial class FicheEmployee : Page
    {
        public FicheEmployee(Employee employee, EmployeeService employeeService)
        {
            InitializeComponent();
            DataContext = new FicheEmployeViewModel(employee, employeeService);
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {

                Annuaire.NavigationService.Instance.GoBack();

        }
        
    }
}