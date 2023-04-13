using appSGSales2.Model;
using appSGSales2.Database;

namespace appSGSales2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class FiltraCliente : ContentPage
    {
        private string tipoOperacao = "";
        private Pedido pedido;
        private GerenciadorDB database;
        public FiltraCliente(Pedido pedido)
        {
            InitializeComponent();
            BindingContext = pedido;

            this.pedido = pedido;
        }

        private void salvarPedido_Clicked(object sender, EventArgs e)
        {
            tipoOperacao = "";
            if (pedido.numpede == 0)
                tipoOperacao = "I";

            database = new GerenciadorDB();
            if (tipoOperacao == "I")
            {
                //pedido.codvend = App.Current.Properties["LOGIN"].ToString(); - Xamarin 
                pedido.codvend = Preferences.Default.Get("LOGIN","").ToString(); // Maui .net 7.0
                pedido.DTEMISS = DateTime.Now;
                database.inserePedido(pedido);
            }
            else
                database.atualizaPedido(pedido);


            //Page page = (Page)App.Current.Properties["PEDIDOMAIN"];
            Page page = Preferences.Default.Get("PEDIDOMAIN",new Page() );
            PopToPage(page);
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


        private void PedidoCancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}