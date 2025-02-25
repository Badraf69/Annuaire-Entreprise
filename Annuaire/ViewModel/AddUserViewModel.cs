using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Annuaire.Services;
using AnnuaireModel;

namespace Annuaire.Views;

public class AddUserViewModel : INotifyPropertyChanged
{
    private readonly UserService _userService;
    private User _user;
    
    public event PropertyChangedEventHandler? PropertyChanged;
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
        MessageBox.Show($"nouvelle utilisateur : {User}");
        if (User == null)
        {
            MessageBox.Show("Veuillez entrer des données valides");
        }
        var addedUser = await _userService.AddUser(User);
        if (addedUser!=null)
        {
            MessageBox.Show("User ajouté avec Succès.","Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
    
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}