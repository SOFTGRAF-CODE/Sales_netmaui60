using appSGSales2.Database;
using appSGSales2.Model;
using appSGSales2.Service;

namespace appSGSales2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PedidoAEnviar : ContentPage
    {
        private GerenciadorDB database;
        private ServiceWS service;

        public PedidoAEnviar(GerenciadorDB gerenciadorDB, ServiceWS serviceWS)
        {
            InitializeComponent();

            service = serviceWS;
            database = gerenciadorDB;
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //Atribuir tela para propriedade para localiza-la no momento de retornar a mesma tela.
            //App.Current.Properties["PEDIDOMAIN"] = this; Xamarin 
            Preferences.Default.Set("PEDIDOMAIN",this); // Maui - ainda temos problemas com estes caras.
            //database = new GerenciadorDB();
            var consultaPedido = await database.ConsultarPedido();
            listaPedido.ItemsSource = consultaPedido;
        }

        private async void enviarPedidos_Clicked(object sender, EventArgs e)
        {
            List<Pedido> pedidos = new List<Pedido>();
            List<IPedido> itempedido = new List<IPedido>();
            foreach (Pedido ped in listaPedido.ItemsSource)
            {
                //ped.codvend = App.Current.Properties["LOGIN"].ToString();
                ped.codvend = Preferences.Default.Get("LOGIN","").ToString();
                if (ped.IsSelected)
                {
                    pedidos.Add(ped);
                    List<IPedido> iped = await database.consultaItensPedido(ped.id_numpede_app);
                    foreach (IPedido item in iped)
                    {
                        itempedido.Add(item);
                    }

                }
            }

            try
            {
                List<Pedido> pedret = await service.enviarPedido(pedidos, itempedido);
                listaPedido.ItemsSource = pedret;
            }
            catch (HttpRequestException rex)
            {
                await DisplayAlert("Aviso", "Falha no envio do pedido." + rex.Message.ToString(), "Ok");
                return;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Aviso", ex.Message.ToString(), "Ok");
                return;
            }
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
                Switch s = (sender as Switch);
                Pedido ped = (Pedido)s.BindingContext;
                /*List<IPedido> iped = database.consultaItensPedido(ped.id_numpede_app);
                if ((iped.Count() == 0) && (e.Value == true))
                {
                    await DisplayAlert("Alerta", "Pedido não possue itens, por favor adicionar", "Ok");
                    s.IsToggled = false;
                }*/
        }
    }
}