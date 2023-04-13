using appSGSales2.Model;
using appSGSales2.Database;
using appSGSales2.ViewModel;

namespace appSGSales2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EscolhaCliente : ContentPage
    {
        private GerenciadorDB database;
        public EscolhaCliente(GerenciadorDB gerenciadorDB,EscolhaClienteViewModel vm) 
        {
            InitializeComponent();
            database = gerenciadorDB;
            BindingContext = vm;
        }

        async void Pedido_Tapped(object sender, EventArgs e)
        {
            Label lblCliente = (sender as Label);
            TapGestureRecognizer tapGest = (TapGestureRecognizer)lblCliente.GestureRecognizers[0];
            Cliente cliente = tapGest.CommandParameter as Cliente;
            Pedido pedido = new Pedido();
            pedido.CODCLI = cliente.CLIENTE;
            var param = new Dictionary<string, object>
            {
                { "Pedido",pedido }
            };
            //Navigation.PushAsync(new CadastroPedido(pedido));
            await Shell.Current.GoToAsync($"CadastroPedido", param);
        }

        private async void sbCliente_TextChanged(object sender, TextChangedEventArgs e)
        {
            listaCliente.ItemsSource = await database.consultaClientes(e.NewTextValue.ToString());
        }
    }
}