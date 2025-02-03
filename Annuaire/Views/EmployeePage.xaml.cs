using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using AnnuaireModel;

namespace Annuaire
{

    public partial class EmployeePage : Page
    {
        
        public EmployeePage()
        {
            InitializeComponent();
            //AnnuaireModel.Employee employee = new AnnuaireModel.Employee(1, "Bertrand","Gille", "bertrand.gille@gmail.com", "0602154879","0603020514",1,1 );
            this.DataContext = new EmployeeViewModel();
        }
    }
}