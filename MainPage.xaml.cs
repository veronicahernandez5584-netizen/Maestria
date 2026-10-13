using System.Text.RegularExpressions;

namespace HolaMundo;

public partial class MainPage : ContentPage
{
	/* este es el patron elegido, en el cual obliga al 
	Debe haber al menos una minúscula. [a-z]
	Debe haber al menos una mayuscula. [A-Z]
	Debe haber al menos 1 digito (?=.*\d)
	Debe haber al menos un character especial (?=.*[^a-zA-Z0-9])
	*/
	const string PasswordPattern =
		@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).+\z";

	static readonly Regex PasswordRegex =
		new(PasswordPattern, RegexOptions.Compiled, TimeSpan.FromMilliseconds(500));

	public MainPage()
	{
		InitializeComponent();
	}
	// Evento principal del boton que valida la contraseña
	private async void OnValidateClicked(object? sender, EventArgs e)
	{
		if (ValidarFormulario())
		{
			await DisplayAlertAsync("Validación", "La contraseña ha sido validada", "OK");
		}
	}
	/* Aquí se realiza la validación del formulario, se extrae y compara el password ingresado 
	se ocupa la clase isMatch de Regex para validar via REGEX los requisitos que nos indico en la tarea
	Al menos una letra mayúscula
	Al menos una letra minúscula
	Al menos un símbolo
	Al menos un número
	*/
	private bool ValidarFormulario()
	{
		string password = PasswordEntry.Text ?? string.Empty;
		string confirmacion = ConfirmPasswordEntry.Text ?? string.Empty;

		if (!Regex.IsMatch(password, PasswordPattern))
		{
			_ = MostrarError(
				"La contraseña no cumple la estructura requerida: " +
				"al menos una letra mayúscula, una letra minúscula, un símbolo y un número.");

			return false;
		}

		if (!string.Equals(password, confirmacion, StringComparison.Ordinal))
		{
			_ = MostrarError("Las contraseñas no coinciden.");

			return false;
		}

		return true;
	}
	// Muestra el mensaje de error
	private async Task MostrarError(string mensaje)
	{
		await DisplayAlertAsync("Validación", mensaje, "OK");

		PasswordRulesLabel.Text = mensaje;
		SemanticScreenReader.Announce(mensaje);
	}
}