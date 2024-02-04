namespace MonkeyFinder.ViewModel;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool _isBusy;
    
    [ObservableProperty]
    private string _title;
    
    public BaseViewModel()
    {
    }
    
    public bool IsNotBusy => !IsBusy;
    
    // private bool _isBusy;
    // private string _title;
    //
    // public bool IsBusy
    // {
    //     get => _isBusy;
    //     set
    //     {
    //         if (_isBusy == value)
    //             return;
    //         
    //         _isBusy = value;
    //         OnPropertyChanged();
    //         OnPropertyChanged(nameof(IsNotBusy));
    //     }
    // }
    //
    // public bool IsNotBusy => !IsBusy;
    //
    // public string Title
    // {
    //     get => _title;
    //     set
    //     {
    //         if (_title == value)
    //             return;
    //         
    //         _title = value;
    //         OnPropertyChanged();
    //     }
    // }
    //
    // public event PropertyChangedEventHandler PropertyChanged;
    //
    // // CallerMemberName is used to get the name of the property that is calling the method at compile time
    // protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    // {
    //     PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    // }
    //
    // protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    // {
    //     if (EqualityComparer<T>.Default.Equals(field, value)) return false;
    //     field = value;
    //     OnPropertyChanged(propertyName);
    //     return true;
    // }
}
