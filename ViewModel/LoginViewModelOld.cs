using appSGSales2.Model;
using appSGSales2.Service;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace appSGSales2.ViewModel
{
    public class LoginViewModelOld : INotifyPropertyChanged
    {
        private string _nome;
        private string _senha;
        private string _mensagem;
        private bool _carregando;
        private bool _carregandoLogin;

        public bool CarregandoLogin
        {
            get { return _carregandoLogin; }
            set { _carregandoLogin = value; OnPropertyChanged("CarregandoLogin"); }
        }

        public bool Carregando
        {
            get { return _carregando; }
            set { _carregando = value; OnPropertyChanged("Carregando"); }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; OnPropertyChanged("Nome"); }
        }
        public string Senha
        {
            get { return _senha; }
            set { _senha = value; OnPropertyChanged("Senha"); }
        }
        public string Mensagem
        {
            get { return _mensagem; }
            set { _mensagem = value; OnPropertyChanged("Mensagem"); }
        }

        public RelayCommand AcessarCommand { get; set; }

        [Obsolete]
        public LoginViewModelOld()
        {
            CarregandoLogin = true;
            AcessarCommand = new RelayCommand(Acessar);
        }

        [Obsolete]
        private async void Acessar()
        {
            Carregando = true;
            try
            {
                inicializaIMEI();

                var user = new Login();
                user.login = Nome;
                user.password = Senha;

                var msn = new Mensagem();
                msn = await ServiceWS.validaUsuario(user);
                
                if (msn != null)
                {
                    /*Application.Current.Properties["CODVEND"] = user.login;
                    Application.Current.Properties["LOGIN"] = user.login;
                    Application.Current.Properties["PASSWORD"] = user.password;*/
                    Preferences.Default.Set("CODVEND", user.login);
                    Preferences.Default.Set("LOGIN", user.login);
                    Preferences.Default.Set("PASSWORD", user.password);
                    Application.Current.MainPage = new NavigationPage(new View.PedidoMain());
                    //Application.Current.MainPage = new NavigationPage(new View.OrderView());
                    //await Shell.Current.GoToAsync(nameof(View.OrderView));
                }
                else
                {
                    CarregandoLogin = true;
                    Carregando = false;
                    Mensagem = "Problemas com usuário ou senha";
                }
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
            finally
            {
                Carregando = false;
                CarregandoLogin = false;
            }
        }

        [Obsolete]
        private async void inicializaIMEI()
        {
           /* var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Phone);
            if (status != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Phone);
                //Best practice to always check that the key exists
                if (results.ContainsKey(Permission.Phone))
                    status = results[Permission.Phone];
            }
*/
            //var imei = DependencyService.Get<IServiceIMEI>().GetImei();
            var imei = "4t3g3563whw";
            Preferences.Default.Set("IMEI",imei);
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
