using appSGSales2.Model;
using appSGSales2.Database;

namespace appSGSales2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PedidoMain : FlyoutPage
    {
        private GerenciadorDB database = null;

        // Necessario para atualizar tela, sem setar a NavigationPage , para MainPage
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Atribuir tela para propriedade para localiza-la no momento de retornar a mesma tela.
            //App.Current.Properties["PEDIDOMAIN"] = this;
            Preferences.Set("PEDIDOMAIN", this.ToString());
            database = new GerenciadorDB();
            var consultaPedido = database.ConsultarPedido();
            listaPedido.ItemsSource = consultaPedido;
        }

        public PedidoMain()
        {
            InitializeComponent();
            BindingContext = new ViewModel.PedidoMainViewModel();
        }

        private void IPedido_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EscolhaCliente());
        }
        
        private void Detalhe_Tapped(object sender, EventArgs e)
        {
            Label lblDetalhePedido = (sender as Label);
            TapGestureRecognizer tapGest = (TapGestureRecognizer)lblDetalhePedido.GestureRecognizers[0];
            Pedido pedido = tapGest.CommandParameter as Pedido;
            //Application.Current.Properties["PEDIDO"] = pedido;
            Preferences.Default.Set("PEDIDO", pedido);
            Navigation.PushAsync(new DetalhePedido(pedido));
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EnvioPedidos());
        }

    }
}