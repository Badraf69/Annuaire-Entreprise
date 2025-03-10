using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Annuaire.Services;
using AnnuaireModel;

namespace Annuaire.Views;

public class AddUserViewModel : BaseViewModel
{
    private readonly UserService _userService;
    private User _user;
    
    public ICommand AddUserCommand { get; }

    public AddUserViewModel()
    {
        _userService = new UserService();
        User = new User();
        AddUserCommand = new RelayCommand(async (_) => await AddUser());


    }

    public User User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged();
        }
    }

    public async Task AddUser()
    {
        string userName = _user.UserName;
        string password = _user.PasswordHash;
        Console.WriteLine($" {_user}");
        // //MessageBox.Show($"nouvelle utilisateur : {User}");
        if(string.IsNullOrWhiteSpace(_user.UserName)|| string.IsNullOrWhiteSpace(_user.PasswordHash))
        {
            MessageBox.Show("Veuillez entrer des données valides");
        }
        var addedUser = await _userService.AddUser(_user);
        Console.WriteLine(addedUser);
        if (addedUser!=null)
        {
            MessageBox.Show("User ajouté avec Succès.","Success", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new ListUserPage());
        }
    }
}