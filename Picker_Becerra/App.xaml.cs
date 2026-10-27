namespace Picker_Becerra;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        // Inicia la aplicación cargando primero el Login dentro de un NavigationPage
        return new Window(new NavigationPage(new LoginPage()));
    }
}