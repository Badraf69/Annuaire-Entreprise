using System.ComponentModel;
using Annuaire.Services;
using Annuaire.Views;

namespace Annuaire.Helpers;

public class SessionManager 
{
    private static LoginViewModel _loginViewModel;

    public static LoginViewModel LoginViewModel
    {
        get => _loginViewModel;
        set
        {
            _loginViewModel = value;
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(LoginViewModel)));            
        }
    }

    public static void InitializeLoginViewModel(UserService userService)
    {
        if (_loginViewModel == null)
        {
            _loginViewModel = new LoginViewModel();
            _loginViewModel.Initialize(userService);
        }
    }
    public static bool IsUserLoggedIn{get; set;}=false;
    public static event PropertyChangedEventHandler PropertyChanged;
}

