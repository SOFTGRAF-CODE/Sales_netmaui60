using appSGSales2.View;
using appSGSales2.ViewModel;

namespace Sales_netmaui60;

public partial class App : Application
{
	public App(LoginViewModel vm)
	{
		InitializeComponent();

        MainPage = new NavigationPage(new LoginView(vm));
        //MainPage = new AppShell();
	}
}
/*
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new View.LoginView());
    }

    protected override void OnStart()
    {
    }

    protected override void OnSleep()
    {
        // Handle when your app sleeps
    }

    protected override void OnResume()
    {
        // Handle when your app resumes
    }
}*/