using System.Windows;
using System.Windows.Controls;
using AnnuaireAPI.Controllers;
using AnnuaireModel;
using Microsoft.EntityFrameworkCore;

namespace Annuaire.Views;

public partial class AddUser : Page
{
    private User _user;
    private readonly AppDbContext _context;

    public AddUser()
    {
        InitializeComponent();
        DataContext = new AddUserViewModel();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("Data Source=d:/DbTest.db")
            .Options;

        _context = new AppDbContext(options);

    }

    // private void AddUserButton_Click(object sender, RoutedEventArgs e)
    // {
    //    
    //     
    //      string userName = UserNameTextBox.Text;
    //      string password = PasswordBox.Password;
    //      Console.WriteLine($"{userName} et {password}");
    //     
    //      if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
    //      {
    //          StatusMessage.Text = "Veuillez remplir tous les champs.";
    //          StatusMessage.Visibility = Visibility.Visible;
    //          return;
    //      }
    //     
    //     
    //      // var existingUser = _context.Users.SingleOrDefault(u => u.UserName == userName);
    //      // if (existingUser != null)
    //      // {
    //      //     StatusMessage.Text = "Cet utilisateur existe déjà.";
    //      //     StatusMessage.Visibility = Visibility.Visible;
    //      //     return;
    //      // }
    //     
    //     
    //      var newUser = new User { UserName = userName };
    //      newUser.SetPassword(password); 
    //     
    //      _context.Users.Add(newUser);
    //      _context.SaveChanges();
    //     
    //      MessageBox.Show("Utilisateur ajouté avec succès !");
    //      NavigationService.Navigate(new MainPage()); 
    //     
    // }
}