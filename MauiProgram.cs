using appSGSales2.Database;
using appSGSales2.Service;
using appSGSales2.View;
using appSGSales2.ViewModel;

namespace Sales_netmaui60;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
		builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddSingleton<GerenciadorDB>();
        builder.Services.AddSingleton<ServiceWS>();

        builder.Services.AddSingleton<LoginView>();
        builder.Services.AddSingleton<LoginViewModel>();

        builder.Services.AddSingleton<OrderView>();
		builder.Services.AddSingleton<OrderViewModel>();

        builder.Services.AddTransient<PedidoAEnviar>();
        builder.Services.AddTransient<PedidoEnviado>();
        
        builder.Services.AddTransient<EscolhaCliente>();
        builder.Services.AddTransient<EscolhaClienteViewModel>();

        builder.Services.AddTransient<DetalhePedido>();
        builder.Services.AddTransient<DetalhePedidoViewModel>();

        builder.Services.AddTransient<CadastroPedido>();
        builder.Services.AddTransient<CadastroPedidoViewModel>();

		return builder.Build();
	}
}
