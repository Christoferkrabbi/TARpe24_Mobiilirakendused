using Microsoft.Maui.Media;
namespace TARpe24_Mobiilirakendused;

public partial class LumememmPage : ContentPage
{
	double speed = 1;
	public LumememmPage()

	{
		InitializeComponent();
	}

	private async void OnActionClicked(object sender, EventArgs e)
	{
		string action = ActionPicker.SelectedItem?.ToString();
		ResultLabel.Text = action;

		switch (action)
		{
			case "Peida":
				Body.IsVisible = false;
				Head.IsVisible = false;
				Bucket.IsVisible = false;
				LeftEye.IsVisible = false;
				RightEye.IsVisible = false;
				Button1.IsVisible = false;
				Button2.IsVisible = false;

				break;

			case "Näita":
				Body.IsVisible = true;
				Head.IsVisible = true;
				Bucket.IsVisible = true;
				LeftEye.IsVisible = true;
				RightEye.IsVisible = true;
				Button1.IsVisible = true;
				Button2.IsVisible = true;
				break;

			case "Muuda värvi":
				bool answer = await DisplayAlert("Kinnitus", "Muudame värvi?", "Jah", "Ei");
				if (answer)
				{
					Random rnd = new Random();
					var color = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

					Body.BackgroundColor = color;
					Head.BackgroundColor = color;
					Bucket.Color = color;
				}
				break;

			case "Sulata":
    await Task.WhenAll(
        Body.FadeTo(0, (uint)(1000 / speed)),
        Head.FadeTo(0, (uint)(1000 / speed)),
        Bucket.TranslateTo(-5, 200, (uint)(500 / speed)),
        LeftEye.FadeTo(0, (uint)(1000 / speed)),
        RightEye.FadeTo(0, (uint)(1000 / speed)),
        Button1.FadeTo(0, (uint)(1000 / speed)),
        Button2.FadeTo(0, (uint)(1000 / speed))
    );

    // Reset
    Body.Scale = 1;
    Head.Scale = 1;
    Body.Opacity = 1;
    Head.Opacity = 1;
    LeftEye.Opacity = 1;
    RightEye.Opacity = 1;
    Button1.Opacity = 1;
    Button2.Opacity = 1;
	Bucket.TranslateTo(0, 0, 100);
    break;

			case "Tantsi":
				await TextToSpeech.SpeakAsync("Jõulud tulevad!");
				await Task.WhenAll(
					Body.TranslateTo(-50, 0, (uint)(300 / speed)),
					Head.TranslateTo(-30, 0, (uint)(300 / speed)),
					Bucket.TranslateTo(-20, 0, (uint)(300 / speed)),
					LeftEye.TranslateTo(-30, 0, (uint)(300 / speed)),
					RightEye.TranslateTo(-30, 0, (uint)(300 / speed)),
					Button1.TranslateTo(-40, 0, (uint)(300 / speed)),
					Button2.TranslateTo(-50, 0, (uint)(300 / speed))
				);

				await Task.WhenAll(
					Body.TranslateTo(50, 0, (uint)(300 / speed)),
					Head.TranslateTo(30, 0, (uint)(300 / speed)),
					Bucket.TranslateTo(20, 0, (uint)(300 / speed)),
                    LeftEye.TranslateTo(30, 0, (uint)(300 / speed)),
                    RightEye.TranslateTo(30, 0, (uint)(300 / speed)),
                    Button1.TranslateTo(40, 0, (uint)(300 / speed)),
                    Button2.TranslateTo(50, 0, (uint)(300 / speed))
                );

				await Task.WhenAll(
					Body.TranslateTo(0, 0, (uint)(300 / speed)),
					Head.TranslateTo(0, 0, (uint)(300 / speed)),
					Bucket.TranslateTo(0, 0, (uint)(300 / speed)),
					LeftEye.TranslateTo(0, 0, (uint)(300 / speed)),
                    RightEye.TranslateTo(0, 0, (uint)(300 / speed)),
                    Button1.TranslateTo(0, 0, (uint)(300 / speed)),
                    Button2.TranslateTo(0, 0, (uint)(300 / speed))
                );
				break;
		}
	}

	private void OnSliderChanged(object sender, ValueChangedEventArgs e)
	{
		double value = e.NewValue;

		Body.Opacity = value;
		Head.Opacity = value;
		Bucket.Opacity = value;
	}

	private void OnStepperChanged(object sender, ValueChangedEventArgs e)
	{
		speed = e.NewValue;
	}
	private void OnNightModeClicked(object sender, EventArgs e)
	{
		this.BackgroundColor = Colors.Blue;

		Body.BackgroundColor = Colors.White;
		Head.BackgroundColor = Colors.White;
	}
}
