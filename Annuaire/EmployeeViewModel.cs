// using System.Collections.ObjectModel;
// using System.ComponentModel;
// using System.Data;
// using System.Runtime.CompilerServices;
// using Annuaire.Services;
// using AnnuaireModel;
// using System.Threading.Tasks;
// using System.Windows;
// using System.Windows.Input;
// using Microsoft.EntityFrameworkCore.Storage;
//
// namespace Annuaire
// {
//     public class EmployeeViewModel : INotifyPropertyChanged
//     {
//         private readonly EmployeeService _employeeService;
//         private Employee _selectedEmployee;
//
//         public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
//         public event PropertyChangedEventHandler PropertyChanged;
//
//         public Employee SelectedEmployee
//         {
//             
//             get => _selectedEmployee;
//             set
//             {
//                 _selectedEmployee = value;
//                 OnPropertyChanged();
//                 OpenEmployeeDetailsCommand.Execute(_selectedEmployee);
//             }
//         }
//
//         public ICommand OpenEmployeeDetailsCommand { get; }
//         public ICommand SupprimerCommand {get; }
//
//         public EmployeeViewModel(EmployeeService employeeService)
//         {
//             _employeeService = new EmployeeService();
//             OpenEmployeeDetailsCommand = new RelayCommand(OpenEmployeeDetails);
//             SupprimerCommand = new RelayCommand(async (param) => await SupprimerEmploye());
//             
//         }
//
//         private async Task LoadEmployees()
//         {
//             var employees = await _employeeService.GetEmployeesAsync();
//             Employees.Clear();
//             foreach (var emp in employees)
//             {
//                 Employees.Add(emp);
//             }
//         }
//
//         // private void OpenEmployeeDetails(object parameter)
//         // {
//         //     if (parameter is Employee employee)
//         //     {
//         //         var detailsPage = new FicheEmployee(employee);
//         //         NavigationServiceSingleton.Navigate(detailsPage);
//         //     }
//         // }
//
//         public void OnPropertyChanged([CallerMemberName] string propertyName = null)
//         {
//             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//         }
//
//
//
//         private bool CanDeleteEmployee(object parameter)
//         {
//             return SelectedEmployee != null;
//         }
//
//         private async Task SupprimerEmploye()
//         {
//             Console.WriteLine($"c'est bien la fonction select");
//             if (SelectedEmployee != null)
//             {
//                 await _employeeService.DeleteEmployeeAsync(SelectedEmployee.Id);
//             }
//
//             // if (SelectedEmployee == null)
//             // {
//             //     MessageBox.Show("Veuillez séléctionner un employe.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
//             //     return;
//             // }
//             // var result = MessageBox.Show($"Voulez-vous vraiment supprimer { SelectedEmployee.FirstName} {SelectedEmployee.LastName}?",
//             //     "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
//             // if (result == MessageBoxResult.Yes)
//             // {
//             //     bool isDeleted = await _employeeService.DeleteEmployeeAsync(SelectedEmployee.Id);
//             //     if (isDeleted)
//             //     {
//             //         MessageBox.Show("Employé supprimé avec succès", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
//             //         NavigationServiceSingleton.Instance.Navigate(new EmployeePage());
//             //     }
//             //     else
//             //     {
//             //         MessageBox.Show("Echec de  la suppression.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
//             //     }
//             // }
//         }
//
//     }
// }