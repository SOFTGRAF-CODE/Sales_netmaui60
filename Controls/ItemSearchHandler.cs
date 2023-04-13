using appSGSales2.Database;
using appSGSales2.Model;

namespace Sales_netmaui60.Controls;

public class ItemSearchHandler : SearchHandler
{
    public IList<Cliente> Items { get; set; }
    public Type SelectedItemNavigationTarget { get; set; }
    private GerenciadorDB database;

    public ItemSearchHandler()
    {
        database = new GerenciadorDB();
    }

    protected override async void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);

        if (string.IsNullOrWhiteSpace(newValue))
        {
            ItemsSource = null;
        }
        else
        {
            ItemsSource = await database.consultaClientes(newValue);
        }
    }

    protected override async void OnItemSelected(object item)
    {
        base.OnItemSelected(item);

        // Let the animation complete
        await Task.Delay(1000);

        ShellNavigationState state = (Application.Current.MainPage as Shell).CurrentState;
        // The following route works because route names are unique in this app.
        //await Shell.Current.GoToAsync($"{GetNavigationTarget()}?name={((Item)item).Summary}");
    }
    /*
    string GetNavigationTarget()
    {
         return (Shell.Current as AppShell).Routes.FirstOrDefault(route => route.Value.Equals(SelectedItemNavigationTarget)).Key;
    }*/
}