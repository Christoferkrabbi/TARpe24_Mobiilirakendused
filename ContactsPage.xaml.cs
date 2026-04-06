namespace TARpe24_Mobiilirakendused;

public partial class ContactsPage : ContentPage
{
	string[] tervitused = {
		"Palju õnne sünnipäevaks!",
		"Häid pühi sulle!",
		"Kõike head ja paremat!",
		"Rõõmsat päeva!",
		"Tervitused sulle pühade puhul!"
	};

	public ContactsPage()
	{
		InitializeComponent();
	}

	// HELISTA funktsioon
	private async void Helista_Clicked(object sender, EventArgs e)
	{
		string data = email_phone.Text;
		if (!string.IsNullOrWhiteSpace(data))
		{
			if (PhoneDialer.Default.IsSupported)
				PhoneDialer.Default.Open(data);
		}
	}

	private async void Saada_sms_Clicked(object? sender, EventArgs e)
	{
		string phone = email_phone.Text;
		string message = sonum_vali.Text ?? "Tere tulemast, saadan sõnumi!";

		if (!string.IsNullOrWhiteSpace(phone) && Sms.Default.IsComposeSupported)
		{
			await Sms.Default.ComposeAsync(new SmsMessage(message, phone));
		}
	}
	private async void Saada_email_Clicked(object? sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(email_phone.Text)) return;

		string target = email_phone.Text;
		string message = sonum_vali.Text ?? "Tere tulemast, saadan e-kirja!";

		EmailMessage e_mail = new EmailMessage
		{
			Subject = "Tervitus sõbralt",
			Body = message,
			BodyFormat = EmailBodyFormat.PlainText,
			To = new List<string>(new[] { target })
		};

		if (Email.Default.IsComposeSupported)
		{
			await Email.Default.ComposeAsync(e_mail);
		}
		else
		{
			await DisplayAlert("Viga", "E-kirja saatmine ei ole toetatud.", "OK");
		}
	}
	private async void SuvalineOnnitlus_Clicked(object sender, EventArgs e)
	{
		string suvalineSisu = tervitused[new Random().Next(tervitused.Length)];
		string viis = await DisplayActionSheet("Vali saatmisviis:", "Tühista", null, "SMS", "E-mail");

		if (viis == "SMS")
		{
			await Sms.Default.ComposeAsync(new SmsMessage(suvalineSisu, email_phone.Text));
		}
		else if (viis == "E-mail")
		{
			await Email.Default.ComposeAsync(new EmailMessage { Body = suvalineSisu, To = new List<string> { email_phone.Text } });
		}
	}
	private async void MuudaFoto_Tapped(object sender, EventArgs e)
	{
		if (MediaPicker.Default.IsCaptureSupported)
		{
			FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
			if (photo != null)
			{
				SobraFoto.Source = ImageSource.FromFile(photo.FullPath);
			}
		}
		else
		{
			await DisplayAlert("Viga", "Kaamera kasutamine pole toetatud.", "OK");
		}
	}
}
