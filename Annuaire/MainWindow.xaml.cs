        using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Annuaire.Services;
using Annuaire.Views;
using AnnuaireModel;

namespace Annuaire;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        NavigationServiceSingleton.MainFrame = MainFrame;
        MainFrame.NavigationService.Navigate(new ListeEmployeePage());
    }

    // private void BtnListeEmploye_Click(object sender, RoutedEventArgs e)
    // {
    //     MainFrame.NavigationService.Navigate(new FicheEmployee(_contentLoaded));
    // }

    // private void BtnFicheEmploye_Click(object sender, RoutedEventArgs e)
    // {
    //     MainFrame.NavigationService.Navigate(new FicheEmployee());
    // }
}