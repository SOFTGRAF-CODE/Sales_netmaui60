using appSGSales2.Model;
using appSGSales2.Service;

namespace appSGSales2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PedidoEnviado : ContentPage
    {
        private ServiceWS service;

        public PedidoEnviado(ServiceWS serviceWS)
        {
            InitializeComponent();
            service = serviceWS;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //service = new ServiceWS();
            Pedido ped = new Pedido();
            //ped.codvend = App.Current.Properties["CODVEND"].ToString();
            ped.codvend = Preferences.Default.Get("CODVEND","").ToString();
            ped.DTEMISS = DateTime.Today;
            ped.CONTATO = "";
            var consultaPedido = await service.listaAPIPedido(ped);

            listaPedidoEnviado.ItemsSource = consultaPedido;
        }

    }
}