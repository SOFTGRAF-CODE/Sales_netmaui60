using appSGSales2.Database;
using appSGSales2.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace appSGSales2.ViewModel
{
    public partial class CadastroPedidoViewModel : ObservableObject, IQueryAttributable
    {
        private GerenciadorDB database;

        public CadastroPedidoViewModel(GerenciadorDB gerenciadorDB)
        {
            database = gerenciadorDB;
        }

        
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Pedidos = query["Pedido"] as Pedido;
        }

        [ObservableProperty]
        Pedido pedidos;

        [RelayCommand]
        async void SalvarPedido()
        {
            var tipoOperacao = "";
            if (Pedidos.id_numpede_app == 0)
                tipoOperacao = "I";

            database = new GerenciadorDB();
            if (tipoOperacao == "I")
            {
                Pedidos.codvend = Preferences.Default.Get("LOGIN", "");
                Pedidos.DTEMISS = DateTime.Now;
                await database.inserePedido(Pedidos);
            }
            else
                await database.atualizaPedido(Pedidos);

            await Shell.Current.GoToAsync("LoginView/OrderView");
        }
    }
}
