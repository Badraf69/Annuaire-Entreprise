using System.Windows;
using System.Windows.Controls;

namespace Annuaire
{

    public partial class FicheEmployee : Page
    {
        private dynamic _selectedEmployee;
        public FicheEmployee(dynamic employee)
        {
            InitializeComponent();
            _selectedEmployee = employee;
            DataContext = _selectedEmployee;
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationServiceSingleton.Instance.MainFrame.CanGoBack)
            {
                NavigationServiceSingleton.Instance.MainFrame.GoBack();
            }
        }
    }
}