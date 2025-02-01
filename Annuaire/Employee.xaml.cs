using System.Windows.Controls;

namespace Annuaire;

public partial class Employee : Page
{
    public Employee()
    {
        InitializeComponent();
        this.DataContext = new Employee();
    }
}