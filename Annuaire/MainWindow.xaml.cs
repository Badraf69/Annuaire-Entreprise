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
        NavigationServiceSingleton.Instance.MainFrame = MainFrame;
        MainFrame.NavigationService.Navigate(new EmployeePage());
    }

    private void BtnListeEmploye_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.NavigationService.Navigate(new EmployeePage());
    }

    // private void BtnFicheEmploye_Click(object sender, RoutedEventArgs e)
    // {
    //     MainFrame.NavigationService.Navigate(new FicheEmployee());
    // }
}