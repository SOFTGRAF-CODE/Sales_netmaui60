using appSGSales2.Model;
using appSGSales2.Service;
using appSGSales2.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace appSGSales2.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {

        public LoginViewModel()
        {
            Carregandologin = true;
        }

        [ObservableProperty]
        bool carregandologin;

        [ObservableProperty]
        bool carregando;

        [ObservableProperty]
        string nome;

        [ObservableProperty]
        string senha;

        [ObservableProperty]
        string mensagem;


        [RelayCommand]
        async Task Acessar()
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
                    Preferences.Default.Set("CODVEND", user.login);
                    Preferences.Default.Set("LOGIN", user.login);
                    Preferences.Default.Set("PASSWORD", user.password);
                    
                    //Application.Current.MainPage = new NavigationPage(new OrderView());
                    //await 
                    Application.Current.MainPage = new AppShell();
                    await Shell.Current.GoToAsync(nameof(OrderView));
                }
                else
                {
                    Carregandologin = true;
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
                Carregandologin = false;
            }
        }

        void inicializaIMEI()
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
            Preferences.Default.Set("IMEI", imei);
        }
    }

}
