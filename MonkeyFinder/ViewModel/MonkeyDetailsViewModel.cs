namespace MonkeyFinder.ViewModel;

[QueryProperty("Monkey", "Monkey")]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
    private readonly IMap _map;
    
    public MonkeyDetailsViewModel(IMap map)
    {
        _map = map;
    }
    
    [ObservableProperty]
    Monkey monkey;

    // Can be hooked up to a button in the UI to go back
    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task OpenMapAsync()
    {
        try
        {
            await _map.OpenAsync(Monkey.Latitude, Monkey.Longitude,
                new MapLaunchOptions
                {
                    Name = Monkey.Name,
                    NavigationMode = NavigationMode.None
                });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable to open maps {ex.Message}", "OK");
        }
    }
}
