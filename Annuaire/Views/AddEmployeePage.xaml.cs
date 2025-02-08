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
}