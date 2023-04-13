using appSGSales2.ViewModel;

namespace appSGSales2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroPedido : ContentPage
    {
        public CadastroPedido(CadastroPedidoViewModel vm)
        {
            InitializeComponent();
            
            BindingContext = vm;
        }
    }
}