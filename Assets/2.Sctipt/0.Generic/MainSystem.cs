public partial class MainSystem : GenericSingleton<MainSystem> //Managers Field
{
    public ScenesManager ScenesManager { get; private set; }
    public CoroutineManager CoroutineManager { get; private set; }
    public SpreadsheetManager SpreadsheetManager { get; private set; }
    public DataManager DataManager { get; private set; }
}

public partial class MainSystem : GenericSingleton<MainSystem> //Initialize Function Field
{
    private void Allocate()
    {
        ScenesManager = gameObject.AddComponent<ScenesManager>();
        CoroutineManager = gameObject.AddComponent<CoroutineManager>();
        SpreadsheetManager = gameObject.AddComponent<SpreadsheetManager>();
        DataManager = gameObject.AddComponent<DataManager>();
    }
}
public partial class MainSystem : GenericSingleton<MainSystem> //Property Function Field
{
    public void Initialize()
    {
        Allocate();
    }
}