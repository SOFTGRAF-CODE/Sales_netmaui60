using appSGSales2.Model;
using appSGSales2.Database;
using appSGSales2.ViewModel;

namespace appSGSales2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalhePedido : ContentPage
    {
        private GerenciadorDB database;
        
       /* protected override void OnAppearing()
        {
            base.OnAppearing();
            // Necessario para atualizar tela
            //Pedido pedido = JsonConvert.DeserializeObject<Pedido>(Preferences.Default.Get("PEDIDO",""));
            //Pedido pedido = (Pedido)App.Current.Properties["PEDIDO"];
            //App.Current.Properties["DETALHEPEDIDO"] = this;
            //Pedido pedido = Preferences.Default.Get("PEDIDO", new Pedido());
            //Preferences.Default.Set("DETALHEPEDIDO", this);
            //listaItensPedido.ItemsSource = database.consultaItensPedido(pedido.id_numpede_app);
        }*/

        public DetalhePedido(DetalhePedidoViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }

        async void NovoItem_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new EscolhaProduto() );
            await Shell.Current.GoToAsync("EscolhaProduto");
        }
        
        async void ExcluirItem_Tapped(object sender, EventArgs e)
        {
            Label lblDetalheExcluirItem = (sender as Label);
            TapGestureRecognizer tapGest = (TapGestureRecognizer)lblDetalheExcluirItem.GestureRecognizers[0];
            IPedido ipedido = tapGest.CommandParameter as IPedido;
            await database.exclusaoItemPedido(ipedido);
            listaItensPedido.ItemsSource = await database.consultaItensPedido(ipedido.ID_PEDE);
        }
        
        private void EditarItem_Tapped(object sender, EventArgs e)
        {
            Label lblDetalheExcluirItem = (sender as Label);
            TapGestureRecognizer tapGest = (TapGestureRecognizer)lblDetalheExcluirItem.GestureRecognizers[0];
            IPedido ipedido = tapGest.CommandParameter as IPedido;
            var param = new Dictionary<string, object>
            {
                { "ipedido",ipedido }
            };

            Shell.Current.GoToAsync($"DetalhePedidoItem", param);
            //Navigation.PushAsync(new DetalhePedidoItem(ipedido));
        }
    }
}