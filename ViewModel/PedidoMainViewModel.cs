using appSGSales2.Database;
using appSGSales2.Model;
using appSGSales2.Service;
using appSGSales2.View;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows.Input;

namespace appSGSales2.ViewModel
{
    public class PedidoMainViewModel : INotifyPropertyChanged
    {
        private string _mensagem;
        private bool _carregando;
        private GerenciadorDB db;
        private readonly ServiceWS _servicews;

        public bool Carregando
        {
            get { return _carregando; }
            set { _carregando = value; OnPropertyChanged("Carregando"); }
        }
        public string Mensagem
        {
            get { return _mensagem; }
            set { _mensagem = value; OnPropertyChanged("Mensagem"); }
        }

        public RelayCommand AtualizarCommand { get; set; }
        public RelayCommand PedidoCommand { get; set; }
        public RelayCommand SairCommand { get; set; }

        public PedidoMainViewModel(GerenciadorDB gerenciadorDB,ServiceWS serviceWS)
        {
            db = gerenciadorDB;
            _servicews = serviceWS;
            PedidoCommand = new RelayCommand(Pedido);
            AtualizarCommand = new RelayCommand(Acessar);
            SairCommand = new RelayCommand(Sair);
        }

        private async void Pedido()
        {
            //Application.Current.MainPage.Navigation.PushAsync(new EscolhaCliente());
            await Shell.Current.GoToAsync(nameof(EscolhaCliente));
        }

        private async void Sair()
        {
           bool result = await Application.Current.MainPage.DisplayAlert("Alerta", "Deseja Sair do Aplicativo?", "Sim","Não");

            if (result)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

        private async void Acessar()
        {
            Carregando = true;

            var msn = new Mensagem();

            try
            {
                Mensagem = "Atualizando Lista de Clientes";
                //ServiceWS _servicews = new ServiceWS();
                //string pCodVend = Application.Current.Properties["CODVEND"].ToString();
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged !=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }

        }
    }
}
