using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel;

public partial class MonkeysViewModel : BaseViewModel
{
    private readonly MonkeyService _monkeyService;
    private readonly IConnectivity _connectivity;
    private readonly IGeolocation _geolocation;
    public ObservableCollection<Monkey> Monkeys { get; set; } = new();

    public MonkeysViewModel(MonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation)
    {
        _monkeyService = monkeyService;
        _connectivity = connectivity;
        _geolocation = geolocation;
        Title = "Monkey Finder";
    }
    
    [ObservableProperty] 
    private bool _isRefreshing;

    [RelayCommand]
    async Task GetMonkeysAsync()
    {
        if(IsBusy)
            return;

        try
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Internet issue", $"Check your internet and try again", "OK");
                
            }
            
            IsBusy = true;
            var monkeys = await _monkeyService.GetMonkeys();

            if (Monkeys.Count != 0)
            {
                Monkeys.Clear();
            }

            foreach (var monkey in monkeys)
            {
                Monkeys.Add(monkey);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            
            await App.Current.MainPage.DisplayAlert("Error", $"Unable to get monkeys: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }
    
    [RelayCommand]
    async Task GoToDetailsAsync(Monkey monkey)
    {
        if (monkey is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true,
            new Dictionary<string, object>
            {
                {"Monkey", monkey}
            });
    }

    [RelayCommand]
    private async Task GetClosestMonkeyAsync()
    {
        if (IsBusy || Monkeys.Count == 0)
            return;

        try
        {
            var location = await _geolocation.GetLastKnownLocationAsync();

            if (location == null)
            {
                location = await _geolocation.GetLocationAsync(
                    new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30),
                    });
            }

            if (location == null)
                return;

            var closestMonkey = Monkeys.MinBy(m => location.CalculateDistance(m.Latitude, m.Longitude, DistanceUnits.Kilometers));
            
            await Shell.Current.DisplayAlert("Closest Monkey", $"The closest monkey is {closestMonkey.Name} in {closestMonkey.Location}", "OK");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            
            await App.Current.MainPage.DisplayAlert("Error", $"Unable to get closest monkey: {ex.Message}", "OK");
        }
    }
}
