using System.Windows;
using System.Windows.Controls;
using Annuaire.Views;
using AnnuaireAPI.Controllers;
using Microsoft.Win32.SafeHandles;

namespace Annuaire;


public partial class LoginView : Page
{
    private readonly AppDbContext _appDbContext;
    public LoginView()
    {
        InitializeComponent();
        
    }

    private void LoginButton(object sender, RoutedEventArgs e)
    {
        string userName = UserNameTextBox.Text;
        string password = PasswordBox.Password;

        if (AuthenticateUser(userName, password))
        {
            Annuaire.NavigationService.Navigate(new MainPage());
        }
        else
        {
            ErrorMessage.Text = "Nom d'utilisateur ou mot de passe incorrect.";
            ErrorMessage.Visibility = Visibility.Visible;
        }
    }

    private bool AuthenticateUser(string userName, string password)
    {
       var user = _appDbContext.Users.SingleOrDefault(u => u.UserName == userName);
       if (user != null)
       {
           return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
       }
       return false;
    }
    
}