using System.Data;
using AnnuaireModel;

namespace Annuaire
{
    public class EmployeeViewModel
    {
        public EmployeePage CurrentEmployee { get; set; }

        public EmployeeViewModel()
        {
            //CurrentEmployee = new AnnuaireModel.Employee(1, "Gille", "Bertrand", "gille.bertrand@gmail.com", "0123654879", "0145896874", 1, 1);
        }
    }
}