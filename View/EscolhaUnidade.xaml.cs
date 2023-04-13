using appSGSales2.Model;
using appSGSales2.Database;

namespace appSGSales2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EscolhaUnidade : ContentPage
    {
        private GerenciadorDB database;
        public EscolhaUnidade()
        {
            InitializeComponent();
            database = new GerenciadorDB();
        }

        private void Unidade_Tapped(object sender, EventArgs e)
        {
            Label lblUnidade = (sender as Label);
            TapGestureRecognizer tapGest = (TapGestureRecognizer)lblUnidade.GestureRecognizers[0];
            Unidade _unidade = tapGest.CommandParameter as Unidade;
            //IPedido ipedido = (IPedido)App.Current.Properties["M_PEDIDOITEM"];
            IPedido ipedido = Preferences.Default.Get("M_PEDIDOITEM", new IPedido() );
            ipedido.CODUNID = _unidade.CODUNID;
            //App.Current.Properties["M_PEDIDOITEM"] = ipedido; Xamarin
            Preferences.Default.Set("M_PEDIDOITEM", ipedido);
            Navigation.PopAsync();
        }

        private async void sbUnidade_TextChanged(object sender, TextChangedEventArgs e)
        {
            listaUnidade.ItemsSource = await database.consultaUnidades(e.NewTextValue.ToString());
        }
    }
}