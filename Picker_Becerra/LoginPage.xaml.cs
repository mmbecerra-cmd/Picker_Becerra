namespace Picker_Becerra;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object? sender, EventArgs e)
    {
        string usuario = TxtUsuario.Text?.Trim() ?? "";
        string password = TxtPassword.Text?.Trim() ?? "";

        if (usuario == "admin" && password == "1234")
        {
            await Navigation.PushAsync(new MainPage());
        }
        else
        {
            await DisplayAlertAsync("Error", "Usuario o contraseña incorrectos.", "OK");
        }
    }
}