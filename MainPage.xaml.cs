using System.ComponentModel;


namespace appSGSales2
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //Shell.Current.GoToAsync(nameof(LoginView));
        }
        private void Pedido_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new View.EscolhaProduto());
        }
    }
}
