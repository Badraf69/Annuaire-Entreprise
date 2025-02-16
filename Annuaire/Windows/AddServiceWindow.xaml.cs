using System.Windows;
using Annuaire.Services;
using AnnuaireModel;

namespace Annuaire;

public partial class AddServiceWindow : Window
{

    public string ServiceName { get; set; }
    public AddServiceWindow()
    {
        InitializeComponent();
    }

    private void OnAddService(object sender, RoutedEventArgs e)
    {
        ServiceName = ServiceNameTextBox.Text.Trim();
        
        if (string.IsNullOrEmpty(ServiceName))
        {
            MessageBox.Show("Veuillez entrer un nom de service !");
        }
        else
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}