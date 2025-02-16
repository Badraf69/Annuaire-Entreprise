using System.Windows;
using System.Windows.Controls;
using Annuaire.Services;
using Annuaire.Views;
using AnnuaireModel;

namespace Annuaire
{

    public partial class FicheEmployee : Page
    {
        //private dynamic _selectedEmployee;
        //dynamic employee
        public FicheEmployee(Employee employee, EmployeeService employeeService)
        {
            InitializeComponent();
           // _selectedEmployee = employee;
            //DataContext = _selectedEmployee;
            DataContext = new FicheEmployeViewModel(employee, employeeService);
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {

                NavigationServiceSingleton.Instance.GoBack();

        }
        
    }
}