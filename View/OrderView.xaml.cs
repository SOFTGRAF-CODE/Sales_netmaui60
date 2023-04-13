using appSGSales2.Database;
using appSGSales2.Model;
using appSGSales2.ViewModel;

namespace appSGSales2.View;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class OrderView : ContentPage
{
    public OrderView(OrderViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;

    }
    
    async void Detalhe_Tapped(object sender, EventArgs e)
    {
        Label lblDetalhePedido = (sender as Label);
        TapGestureRecognizer tapGest = (TapGestureRecognizer)lblDetalhePedido.GestureRecognizers[0];
        Pedido pedido = tapGest.CommandParameter as Pedido;

        var param = new Dictionary<string, object>
        {
            { "Pedido",pedido }
        };

        await Shell.Current.GoToAsync($"DetalhePedido",param);
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new EnvioPedidos());
    }

}