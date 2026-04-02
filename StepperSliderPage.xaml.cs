namespace TARpe24_Mobiilirakendused;

public partial class StepperSliderPage : ContentPage
{
	Label label;
	Stepper stepper;
	Slider slider;
	AbsoluteLayout al;
	public StepperSliderPage()
	{

        InitializeComponent();

        label = new Label
		{
			Text = "...",
			BackgroundColor = Colors.LightGray,
		};
		stepper = new Stepper
		{
			Minimum = 0,
			Maximum = 360,
			Increment = 5,
			Value = 50,
			HorizontalOptions = LayoutOptions.Center
		};
		stepper.ValueChanged += Stepper_Slider_ValueChanged;
		slider = new Slider
		{
			Minimum = 0,
			Maximum = 360,
			Value = 50,
			HorizontalOptions = LayoutOptions.Center,
			MinimumTrackColor = Colors.LightGray,
			MaximumTrackColor = Colors.DarkGray,
			ThumbColor = Colors.Gray,
			WidthRequest = 300,
		};
		slider.ValueChanged += Stepper_Slider_ValueChanged;
		al = new AbsoluteLayout { Children = { label, stepper, slider } };
		List<View> controls = new List<View> { label, stepper, slider };
		for (int i = 0; i< controls.Count; i++)
		{
			double yKoht = 0.2 + i * 0.2;
			AbsoluteLayout.SetLayoutBounds(controls[i], new Rect(0.5, yKoht, 300, 60));
			AbsoluteLayout.SetLayoutFlags(controls[i], Microsoft.Maui.Layouts.AbsoluteLayoutFlags.PositionProportional);
		}
		Content = al;

	
	}

	private void Stepper_Slider_ValueChanged(object? sender, ValueChangedEventArgs e)
	{
		label.Text = $"Stepperi/Slideri väärtus: {e.NewValue:F0}";
		label.FontSize = 24 + e.NewValue / 4;
		label.BackgroundColor = Color.FromRgb((int)(e.NewValue * 2.55), (int)(255 - e.NewValue * 2.55), 128);
		label.TextColor = Color.FromRgb((int)(255 - e.NewValue * 2.55), (int)(e.NewValue * 2.55), 128);
		label.Rotation = e.NewValue;
	}
}