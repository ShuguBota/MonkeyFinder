namespace MonkeyFinder.Services;

public class MonkeyService
{
    private List<Monkey> _monkeys = new();
    
    public async Task<List<Monkey>> GetMonkeys()
    {
        if (_monkeys?.Count > 0)
        {
            return _monkeys;
        }

        await using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();
        
        _monkeys = JsonSerializer.Deserialize<List<Monkey>>(contents);
        
        return _monkeys;
    }
}
