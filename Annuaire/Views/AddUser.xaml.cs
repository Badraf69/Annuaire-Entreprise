using System.Windows;
using System.Windows.Controls;
using AnnuaireAPI.Controllers;
using AnnuaireModel;
using Microsoft.EntityFrameworkCore;

namespace Annuaire.Views;

public partial class AddUser : Page
{
    private User _user;
    private readonly AppDbContext _context;

    public AddUser()
    {
        InitializeComponent();
        DataContext = new AddUserViewModel();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("Data Source=d:/DbTest.db")
            .Options;

        _context = new AppDbContext(options);
    }
    private void BtnRetour_Click(object sender, RoutedEventArgs e)
    {
        Annuaire.NavigationService.Instance.GoBack();
    }
}
