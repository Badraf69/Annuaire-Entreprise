using System.Windows;
using System.Windows.Controls;
using AnnuaireAPI.Controllers;
using AnnuaireModel;

namespace Annuaire.Views;

public partial class AddUser : Page
{
    private readonly AppDbContext _context;

    public AddUser()
    {
        InitializeComponent();
        DataContext = new AddUserViewModel();
        
    }

    private void AddUserButton_Click(object sender, RoutedEventArgs e)
    {
        string userName = UserNameTextBox.Text;
        string password = PasswordBox.Password;
        
        
        if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
        {
            StatusMessage.Text = "Veuillez remplir tous les champs.";
            StatusMessage.Visibility = Visibility.Visible;
            return;
        }

        
        // var existingUser = _context.Users.SingleOrDefault(u => u.UserName == userName);
        // if (existingUser != null)
        // {
        //     StatusMessage.Text = "Cet utilisateur existe déjà.";
        //     StatusMessage.Visibility = Visibility.Visible;
        //     return;
        // }

        
        var newUser = new User { UserName = userName };
        newUser.SetPassword(password); 
        
        _context.Users.Add(newUser);
        _context.SaveChanges();

        MessageBox.Show("Utilisateur ajouté avec succès !");
        NavigationService.Navigate(new MainPage()); 
       
    }
}