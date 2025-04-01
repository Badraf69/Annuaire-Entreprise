using System.Windows.Input;
using Annuaire.Services;
using Annuaire.ViewModel;
using AnnuaireModel;

namespace Annuaire.Views;

public class UpdateServiceViewModel : BaseViewModel
{
    private Service _service;
    private readonly ServiceService _serviceService;

    public UpdateServiceViewModel(Service service, ServiceService serviceService)
    {
        _service = service;
        _serviceService = serviceService;
        UpdateServiceCommand = new RelayCommand(async (_) => await UpdateService());
    }
    public ICommand UpdateServiceCommand { get; }

    public Service Service
    {
        get=> _service;
        set
        {
            _service = value;
            OnPropertyChanged();
        }
    }

    private async Task UpdateService()
    {
        try
        {
            var updatedService = await _serviceService.UpdateService(Service);
            if (updatedService != null)
            {   
                Service = updatedService;
                NavigationService.Navigate(new ListServicePage());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}