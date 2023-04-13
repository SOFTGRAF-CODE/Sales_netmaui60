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
    public partial class DetalhePedidoItem : ContentPage
    {
        private IPedido ipedido;
        private string tipoOperacao;
        private readonly GerenciadorDB database;
        public DetalhePedidoItem(IPedido ipedido, GerenciadorDB gerenciadorDB)
        {
            InitializeComponent();
            database = gerenciadorDB;

            this.ipedido = ipedido;
            if (this.ipedido != null )
              BindingContext = ipedido;
            //App.Current.Properties["M_PEDIDOITEM"] = this.ipedido;
            //App.Current.Properties["NUMBER_TAPS"] = 0;
            Preferences.Default.Set("M_PEDIDOITEM", this.ipedido);
            Preferences.Default.Set("NUMBER_TAPS", 0);
        }

        
        private void Unidade_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EscolhaUnidade());
        }
        
        
        private async void salvarItemPedido_Clicked(object sender, EventArgs e)
        {
            //int nTaps = (int)App.Current.Properties["NUMBER_TAPS"]+1; // Xamarin
            int nTaps = Preferences.Default.Get("NUMBER_TAPS",0) + 1;

            if (nTaps == 2)
                return;

            Preferences.Default.Set("NUMBER_TAPS",nTaps); 
            //App.Current.Properties["NUMBER_TAPS"] = nTaps;

            try
            {
                //Pedido pedido = (Pedido)App.Current.Properties["PEDIDO"]; // Xamarin
                Pedido pedido = Preferences.Default.Get("PEDIDO", new Pedido());
                //GerenciadorDB database = new GerenciadorDB();
                List<IPedido> itemPedido = await database.consultaItensPedido(pedido.id_numpede_app);//.LastOrDefault();

                IPedido ped = itemPedido.LastOrDefault();

                if (ipedido.ID == 0) 
                {
                    ipedido.ID_SEQ = ped != null ? ped.ID_SEQ + 1 : 1;
                    tipoOperacao = "I";
                }
                ipedido.ID_PEDE = pedido.id_numpede_app;
                //ipedido.PRECOUNIT = 2.00M;
                //ipedido.TOTAL = ipedido.QTDPED * ipedido.PRECOUNIT;
                if (tipoOperacao == "I")
                    database.insereItemPedido(ipedido);
                else
                    database.atualizaItemPedido(ipedido);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //Page page = (Page)App.Current.Properties["DETALHEPEDIDO"]; Xamarin
            //Page page = Preferences.Default.Get("DETALHEPEDIDO", new Page());
            //PopToPage(page);
            await Shell.Current.GoToAsync("DetalhePedido");
        }

        private void PedidoCancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        protected async Task PopToPage(Page destination)
        {
            if (destination == null) return;

            //First, we get the navigation stack as a list
            var pages = Navigation.NavigationStack.ToList();

            //Then we invert it because it's from first to last and we need in the inverse order
            pages.Reverse();
            
            //Then we discard the current page
            pages.RemoveAt(0);
            List<Page> toRemove = new List<Page>();

            foreach (var page in pages)
            {
                if (page == destination) break; //We found it.

                toRemove.Add(page);
            }

            foreach (var rvPage in toRemove)
            {
                Navigation.RemovePage(rvPage);
            }

            await Navigation.PopAsync();
        }
    }
}