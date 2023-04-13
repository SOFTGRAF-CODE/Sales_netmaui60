using appSGSales2.ViewModel;

namespace appSGSales2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {

        public LoginView(LoginViewModel vm)
        {
            InitializeComponent();
            //BindingContext = new LoginViewModel();
            BindingContext = vm;
        }
    }
}