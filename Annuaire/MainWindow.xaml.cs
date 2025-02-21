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
    private readonly List<Key> _inputKeysSequence = new List<Key>();

    private readonly List<Key> _KonamiCode = new List<Key>()
    {
        Key.B, Key.A
    };
    public MainWindow()
    {
        InitializeComponent();
        //NavigationServiceSingleton.MainFrame = MainFrame;
        //MainFrame.NavigationService.Navigate(new ListeEmployeePage());
        //NavigationServiceSingleton.Navigate(new LoginView());
        NavigationService.Instance.Initialize(MainFrame);
        NavigationService.Instance.GoToFirstPage(new MainPage());
        this.KeyDown += KonamiCode;
    }

    private void load(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new LoginView());
    } 
    private void KonamiCode(object sender, KeyEventArgs e)
    {
        _inputKeysSequence.Add(e.Key);
        if (_inputKeysSequence.Count > _KonamiCode.Count)
        {
            _inputKeysSequence.RemoveAt(0);
        }

        if (_inputKeysSequence.Count == _KonamiCode.Count)
        {
            bool iskonamiCode = true;
            for (int i = 0; i < _KonamiCode.Count; i++)
            {
                if (_inputKeysSequence[i] != _KonamiCode[i])
                {
                    iskonamiCode = false;
                    break;
                }
            }

            if (iskonamiCode)
            {
                NavigationService.Navigate(new ListeEmployeePage());
                //MessageBox.Show("Konami Code OK");
                _inputKeysSequence.Clear();
            }
        }
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