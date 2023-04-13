using appSGSales2.View;

namespace appSGSales2;

public partial class AppShell : Shell
{
	public AppShell()
	{
        InitializeComponent();

        Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
        Routing.RegisterRoute(nameof(OrderView), typeof(OrderView));
        Routing.RegisterRoute(nameof(DetalhePedido), typeof(DetalhePedido));
        Routing.RegisterRoute(nameof(CadastroPedido), typeof(CadastroPedido));
        Routing.RegisterRoute(nameof(EscolhaCliente), typeof(EscolhaCliente));
        Routing.RegisterRoute(nameof(EscolhaProduto), typeof(EscolhaProduto));
    }
}
