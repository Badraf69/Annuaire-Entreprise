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

    }
}