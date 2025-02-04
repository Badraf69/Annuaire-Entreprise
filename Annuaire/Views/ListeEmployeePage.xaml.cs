using System.Windows.Controls;
using Annuaire.Services;

namespace Annuaire.Views
{

    public partial class ListeEmployeePage : Page
    {
        public ListeEmployeePage()
        {
            InitializeComponent();
            var employeeService = new EmployeeService();
            DataContext = new ListEmployeeViewModel(employeeService);
            Console.WriteLine(DataContext);
            Console.WriteLine($"test du focntionnement je suis dans ListeEmployeePage.xaml.cs");
        }

        private void EmployeesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is ListEmployeeViewModel viewModel && viewModel.SelectedEmployee != null)
            {
                viewModel.NavigateToFicheEmployeeCommand.Execute(null);
            }
        }
    }
}