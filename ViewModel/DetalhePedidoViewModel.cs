using appSGSales2.Database;
using appSGSales2.Model;
using appSGSales2.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace appSGSales2.ViewModel
{
    public partial class DetalhePedidoViewModel : ObservableObject,IQueryAttributable
    {
        private GerenciadorDB database;
        
        public DetalhePedidoViewModel(GerenciadorDB gerenciadorDB)
        {
            database = gerenciadorDB;
        }

        [ObservableProperty]
        Pedido pedidos;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Pedidos = query["Pedido"] as Pedido;
        }

        [RelayCommand]
        async void EditarPedido()
        {
            var param = new Dictionary<string, object>
            {
                { "Pedido",Pedidos }
            };
            await Shell.Current.GoToAsync($"DetalhePedidoItem", param);
        }

        [RelayCommand]
        async void ExcluirPedido(Pedido pedido)
        {
            database.exclusaoPedido(pedido);
            //listaItensPedido.ItemsSource = await database.consultaItensPedido(ipedido.ID_PEDE);
            await Shell.Current.GoToAsync("//OrderView");
        }

    }
}
