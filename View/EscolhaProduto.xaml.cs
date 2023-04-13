using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using appSGSales2.Model;
using appSGSales2.Database;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace appSGSales2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EscolhaProduto : ContentPage
    {
        private GerenciadorDB database;
        private Pedido pedido;
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //pedido = (Pedido)App.Current.Properties["PEDIDO"]; // Xamarin
            pedido = Preferences.Default.Get("PEDIDO", new Pedido() );
        }

        public EscolhaProduto()
        {
            InitializeComponent();
            database = new GerenciadorDB();
        }

        private void Pedido_Tapped(object sender, EventArgs e)
        {
        }

        private async void sbProduto_TextChanged(object sender, TextChangedEventArgs e)
        {
            listaProduto.ItemsSource = await database.consultaProdutos(pedido.CODCLI, e.NewTextValue.ToString());
        }

        private void CadastroPedidoItem_Tapped(object sender, EventArgs e)
        {
            Label lblCliente = (sender as Label);
            TapGestureRecognizer tapGest = (TapGestureRecognizer)lblCliente.GestureRecognizers[0];
            IPedido ipedido = new IPedido();
            ListaPreco produto = tapGest.CommandParameter as ListaPreco;

            ipedido.CODMMCC = produto.CODMP;
            ipedido.DESCRICAO = produto.DESCRICAO;
            var param = new Dictionary<string, object>
            {
                { "IPedido",ipedido }
            };

            Shell.Current.GoToAsync($"DetalhePedidoItem", param);
            //Navigation.PushAsync(new DetalhePedidoItem(ipedido));
        }
    }
}