using appSGSales2.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace appSGSales2.ViewModel;

public partial class EscolhaClienteViewModel : ObservableObject
{

    public EscolhaClienteViewModel()
    {

    }

    [RelayCommand]
    async Task Tap(Cliente cliente)
    {
        Pedido pedido = new Pedido
        {
            CODCLI = cliente.CLIENTE
        };
        var param = new Dictionary<string, object>
            {
                { "Pedido",pedido }
            };
        //Navigation.PushAsync(new CadastroPedido(pedido));
        await Shell.Current.GoToAsync($"CadastroPedido", param);
    }

}

