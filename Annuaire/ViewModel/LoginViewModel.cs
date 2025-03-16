using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Annuaire.Helpers;
using Annuaire.Services;
using AnnuaireModel;


namespace Annuaire.Views
{
    public class LoginViewModel : BaseViewModel
    {
        private  UserService _userService;
        private string _username;
        private string _password;
        private bool _isUserLoggedIn;

        public LoginViewModel()
        {
            _userService = new UserService();
            LoginCommand = new AsyncRelayCommand(Login);
            LogoutCommand = new RelayCommand(Logout);
        }
        public void Initialize(UserService userService)
        {
            _userService = userService;
        }
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            private get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public bool IsUserLoggedIn
        {
            get => _isUserLoggedIn;
            private set
            {
                _isUserLoggedIn = value;
                OnPropertyChanged(nameof(IsUserLoggedIn));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand LogoutCommand { get; }

        /**
         * Fonction pour la gestion de la connexion
         * permet de changer le statut a connecté
         */
        private async Task Login()
        {
            var users = await _userService.GetUsersAsync();
            var user = users.FirstOrDefault(u => u.UserName == Username);
            if (user != null && BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash))
            {
                IsUserLoggedIn = true;
                SessionManager.IsUserLoggedIn = true;
                NavigationService.Navigate(new MainPage());
                //MessageBox.Show("Connexion réussie !");
            }
            else
            {
                MessageBox.Show("Identifiant ou mot de passe incorrect !");
            }
        }
        
        /**
         * Fonction de déconnexion
         * 
         */
        private void Logout(object parameter)
        {
            IsUserLoggedIn = false;
            SessionManager.IsUserLoggedIn = false;
            Username = string.Empty;
            Password = string.Empty;
            NavigationService.Navigate(new MainPage());
            MessageBox.Show("Déconnexion réussie !");
        }
    }
}
