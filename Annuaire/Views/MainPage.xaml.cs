using System.Windows;
using System.Windows.Controls;
using Annuaire.Services;
using AnnuaireAPI.Controllers;
using AnnuaireModel;

namespace Annuaire.Views
{
    
    public partial class MainPage : Page
    {
        private readonly Service _service;
        private readonly ServiceService _serviceService;
        
        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            _service = new Service();
            _serviceService = new ServiceService();
            
        }

        private void OnAddServiceButtonClicked(object sender, RoutedEventArgs e)
        {
            var addServiceWindow = new AddServiceWindow();
            var result = addServiceWindow.ShowDialog();
            if (result == true)
            {
                string serviceName = addServiceWindow.ServiceName;
                InsertService(serviceName);
            }
        }

        private void InsertService(string serviceName)
        {
            
            _service.ServiceName = serviceName;
            var addService =  _serviceService.AddServiceAsync(_service);
        }
    }
}