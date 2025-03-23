using System.Windows;
using System.Windows.Controls;
using Annuaire.Views;

namespace Annuaire
{
    public partial class LoginView : Page
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Password = ((PasswordBox)sender).Password;
            }
        }
        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            Annuaire.NavigationService.Instance.GoBack();
        }
    }
}