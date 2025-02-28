using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Annuaire.Services;
using AnnuaireModel;

namespace Annuaire.Views;

public class ListUserViewModel : INotifyPropertyChanged
{
    private readonly UserService _userService;
    private ObservableCollection<User> _users;
    private User _user;
    private User _selectedUser;

    public ListUserViewModel(UserService userService)
    {
        _userService = userService;
        Users = new ObservableCollection<User>();
        
        NavigateToListServiceCommand = App.NavigationVM.NavigateToListServiceCommand;
        NavigateToListEmployeeCommand = App.NavigationVM.NavigateToListEmployeeCommand;
        NavigateToMenuCommand = App.NavigationVM.NavigateToMenuCommand;
        NavigateToAddUserCommand = App.NavigationVM.NavigateToAddUserCommand;
        DeleteUserCommand = new AsyncRelayCommand(
            async () => await DeleteUser(),
            () => SelectedUser != null
        );        
        LoadUsers();
    }
    public ObservableCollection<User> Users
    {
        get => _users;
        set
        {
            _users = value;
            OnPropertyChanged();
        }
    }

    public User SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = value;
            OnPropertyChanged();
            ((AsyncRelayCommand)DeleteUserCommand).RaiseCanExecuteChanged();
        }
    } 
    public ICommand NavigateToListServiceCommand { get; set; }
    public ICommand NavigateToListEmployeeCommand { get; set; }
    public ICommand NavigateToMenuCommand { get; set; }
    public ICommand NavigateToAddUserCommand { get; set; }
    public ICommand DeleteUserCommand { get; set; }
    
    private async void LoadUsers()
    {
        var users = await _userService.GetUsersAsync();
        Users = new ObservableCollection<User>(users);
    }
    private async Task DeleteUser()
    {
        if(SelectedUser == null) return;

        var result = MessageBox.Show($"Voulez-vous vraiment supprimer '{SelectedUser.UserName}?", 
            "Confirmation", 
            MessageBoxButton.YesNo, 
            MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            
            int isDeleted = await _userService.DeleteUserAsync(SelectedUser.Id);
            if (new []{200,201,204}.Contains(isDeleted))
            {
                MessageBox.Show("Utilisateur supprimé avec succès", "Success", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information);
                Users.Remove(SelectedUser);
                SelectedUser = null;
            }
            else
            {
                MessageBox.Show($"Erreur lors de la suppression d'un utilisateur : ", "Error", 
                    MessageBoxButton.OK);
            }
        }
    }
    
    
    
    
    
    public event PropertyChangedEventHandler PropertyChanged;
    
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}