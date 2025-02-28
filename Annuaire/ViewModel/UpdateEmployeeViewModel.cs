using System.ComponentModel;
using System.Runtime.CompilerServices;
using Annuaire.Services;
using AnnuaireModel;

namespace Annuaire.Views;

public class UpdateEmployeeViewModel : BaseViewModel
{
    private readonly EmployeeService _employeeService;
    private Employee _employee;

}