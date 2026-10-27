using System.Text;
using System.Text.RegularExpressions;

namespace Picker_Becerra;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnEnviarInscripcionClicked(object? sender, EventArgs e)
    {
        string tipoId = PckTipoId.SelectedItem?.ToString() ?? "";
        string numeroId = TxtNumeroId.Text?.Trim() ?? "";
        string paterno = TxtPaterno.Text?.Trim() ?? "";
        string materno = TxtMaterno.Text?.Trim() ?? "";
        string nombresStr = TxtNombres.Text?.Trim() ?? "";
        string telefono = TxtTelefono.Text?.Trim() ?? "";
        string correo = TxtCorreo.Text?.Trim() ?? "";
        string confirmarCorreo = TxtConfirmarCorreo.Text?.Trim() ?? "";
        string carrera = PckCarrera.SelectedItem?.ToString() ?? "";
        string modalidad = PckModalidad.SelectedItem?.ToString() ?? "";
        string campus = PckCampus.SelectedItem?.ToString() ?? "";

        // 1. Validar campos vacíos
        if (string.IsNullOrEmpty(numeroId) || string.IsNullOrEmpty(paterno) ||
            string.IsNullOrEmpty(nombresStr) || string.IsNullOrEmpty(telefono) ||
            string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(confirmarCorreo))
        {
            await DisplayAlertAsync("Advertencia", "Por favor completa todos los campos obligatorios.", "OK");
            return;
        }

        // 2. Validar Solo Números
        if (!Regex.IsMatch(numeroId, @"^[0-9]+$"))
        {
            await DisplayAlertAsync("Error de Validación", "El número de identificación solo debe contener números.", "OK");
            return;
        }

        if (!Regex.IsMatch(telefono, @"^[0-9]+$"))
        {
            await DisplayAlertAsync("Error de Validación", "El teléfono solo debe contener números.", "OK");
            return;
        }

        // 3. Validar Solo Letras
        string patronLetras = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$";

        if (!Regex.IsMatch(paterno, patronLetras) || (!string.IsNullOrEmpty(materno) && !Regex.IsMatch(materno, patronLetras)))
        {
            await DisplayAlertAsync("Error de Validación", "Los apellidos solo deben contener letras.", "OK");
            return;
        }

        if (!Regex.IsMatch(nombresStr, patronLetras))
        {
            await DisplayAlertAsync("Error de Validación", "Los nombres solo deben contener letras.", "OK");
            return;
        }

        // 4. Validar Coincidencia de Correo
        if (!correo.Equals(confirmarCorreo, StringComparison.OrdinalIgnoreCase))
        {
            await DisplayAlertAsync("Error de Validación", "Los correos electrónicos ingresados no coinciden.", "OK");
            return;
        }

        // 5. Guardar en TXT
        try
        {
            string contenidoRegistro = $"=== REGISTRO DE MATRÍCULA ===\n" +
                                     $"Fecha: {DateTime.Now}\n" +
                                     $"Tipo ID: {tipoId}\n" +
                                     $"Número ID: {numeroId}\n" +
                                     $"Apellidos: {paterno} {materno}\n" +
                                     $"Nombres: {nombresStr}\n" +
                                     $"Teléfono: {telefono}\n" +
                                     $"Correo: {correo}\n" +
                                     $"Carrera: {carrera}\n" +
                                     $"Modalidad: {modalidad}\n" +
                                     $"Campus: {campus}\n" +
                                     $"-----------------------------\n\n";

            string rutaArchivo = Path.Combine(FileSystem.AppDataDirectory, "datos_matricula.txt");
            File.AppendAllText(rutaArchivo, contenidoRegistro, Encoding.UTF8);

            await DisplayAlertAsync("Éxito", $"Inscripción guardada correctamente en el archivo TXT.\nRuta: {rutaArchivo}", "OK");

            LimpiarCampos();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", $"No se pudo crear el archivo TXT: {ex.Message}", "OK");
        }
    }

    private void LimpiarCampos()
    {
        TxtNumeroId.Text = string.Empty;
        TxtPaterno.Text = string.Empty;
        TxtMaterno.Text = string.Empty;
        TxtNombres.Text = string.Empty;
        TxtTelefono.Text = string.Empty;
        TxtCorreo.Text = string.Empty;
        TxtConfirmarCorreo.Text = string.Empty;
        PckTipoId.SelectedIndex = -1;
        PckCarrera.SelectedIndex = -1;
        PckModalidad.SelectedIndex = -1;
        PckCampus.SelectedIndex = -1;
    }
}