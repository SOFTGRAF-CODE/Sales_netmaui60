using appSGSales2.Database;
using appSGSales2.Model;
using appSGSales2.Service;
using appSGSales2.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace appSGSales2.ViewModel;

public partial class OrderViewModel : ObservableObject
{
    private GerenciadorDB db;
    private readonly ServiceWS _servicews;

    public OrderViewModel(GerenciadorDB gerenciadorDB, ServiceWS servicews)
    {
        db = gerenciadorDB;
        _servicews = servicews;
    }

    [ObservableProperty]
    ObservableCollection<Pedido> pedidos;

    [ObservableProperty]
    bool carregando;
    [ObservableProperty]
    string mensagem;

    [RelayCommand]
    async Task Appearing()
    {
        var consultaPedido = await db.ConsultarPedido();
        Pedidos = new ObservableCollection<Pedido>(consultaPedido);
    }

    [RelayCommand]
    async Task Pedido()
    {
        await Shell.Current.GoToAsync(nameof(EscolhaCliente));
    }

    [RelayCommand]
    async void ShowOrder(Pedido pedido)
    {
        var param = new Dictionary<string, object>
            {
                { "Pedido",pedido }
            };
        await Shell.Current.GoToAsync($"DetalhePedido", param);
    }

    [RelayCommand]
    async void DeleteOrder(Pedido pedido)
    {
        await db.exclusaoPedido(pedido);
    }

    [RelayCommand]
    async Task Update()
    {
        Carregando = true;

        try
        {
            Mensagem= "Atualizando Lista de Clientes";
            string pCodVend = Preferences.Default.Get("CODVEND", "");
            List<Cliente> _listaCliente = await _servicews.listaClienteVendedor(pCodVend);
            db.insereCliente(_listaCliente);

            Mensagem = "Atualizando Lista de Precos";
            List<ListaPreco> _listaPreco = await _servicews.listaPreco(pCodVend);
            //await db.insereListaPreco(_listaPreco);
            /*
            Mensagem = "Atualizando Unidades";
            List<Unidade> _unidades = await _servicews.listaUnidades();
            db.insereUnidade(_unidades);
            */

            Carregando = false;
        }
        catch (HttpRequestException rex)
        {
            Carregando = false;
            Mensagem = "Falha na conexão." + rex.Message.ToString();
            return;
        }
        catch (Exception ex)
        {
            Carregando = false;
            Mensagem = ex.Message.ToString();
            return;
        }
    }
    
    [RelayCommand]
    async Task SairCommand()
    {
        bool result = await Application.Current.MainPage.DisplayAlert("Alerta", "Deseja Sair do Aplicativo?", "Sim", "Não");

        if (result)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }



}

