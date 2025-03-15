using System.Collections.ObjectModel;
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
    private ObservableCollection<User> _users;
    
    
    public ICommand AddUserCommand { get; }

    public AddUserViewModel()
    {
        _userService = new UserService();
        User = new User();
        Users = new ObservableCollection<User>();
        AddUserCommand = new RelayCommand(async (_) => await AddUser());
        LoadUsers();

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
    //liste des users nécessaire pour le contrôle d'existance
    public ObservableCollection<User> Users
    {
        get => _users;
        set
        {
            _users = value;
            OnPropertyChanged();
        }
    }
    private async void LoadUsers()
    {
        var userList = await _userService.GetUsersAsync();
        if (userList != null)
        {
            Users = new ObservableCollection<User>(userList);
        }
    }
    public async Task AddUser()
    {
        string userName = _user.UserName;
        string password = _user.PasswordHash;

        //Contrôle que toutes les champs sont non vide messag le cas écheant 
        if(string.IsNullOrWhiteSpace(_user.UserName)|| string.IsNullOrWhiteSpace(_user.PasswordHash))
        {
            MessageBox.Show("Veuillez entrer des données valides");
            return;
        }
        //Contrôle si un utilisateur ayant le même nom existe déjà et renvoi d'un message si c'est le cas
        if (Users.Any(u => u.UserName == userName))
        {
            MessageBox.Show("Un utlisateur avec ce nom existe déjà");
            return;
        }
        var addedUser = await _userService.AddUser(_user);

        if (addedUser!=null)
        {
            MessageBox.Show("User ajouté avec Succès.","Success", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new ListUserPage());
        }
    }
}