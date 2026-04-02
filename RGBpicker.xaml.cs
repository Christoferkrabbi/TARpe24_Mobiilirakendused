using Microsoft.Maui.Layouts;

namespace TARpe24_Mobiilirakendused;

public partial class RGBpicker : ContentPage
{
    BoxView redBox, greenBox, blueBox, resultBox;
    Slider redSlider, greenSlider, blueSlider;
    Label labelTitle;
    Button btnRandomColor;
    Stepper stpCorner;

    public RGBpicker()
    {
        InitializeComponent();

        AbsoluteLayout al = new AbsoluteLayout();

        labelTitle = new Label
        {
            Text = "RGB mudel",
            FontSize = 24,
            FontAttributes = FontAttributes.Bold,
            HorizontalTextAlignment = TextAlignment.Center
        };
        AbsoluteLayout.SetLayoutBounds(labelTitle, new Rect(0.5, 0.02, 200, 40));
        AbsoluteLayout.SetLayoutFlags(labelTitle, AbsoluteLayoutFlags.PositionProportional);

        //kastid
        double boxSize = 70;
        redBox = new BoxView { CornerRadius = 15 };
        greenBox = new BoxView { CornerRadius = 15 };
        blueBox = new BoxView { CornerRadius = 15 };

        AbsoluteLayout.SetLayoutBounds(redBox, new Rect(0.15, 0.1, boxSize, boxSize));
        AbsoluteLayout.SetLayoutFlags(redBox, AbsoluteLayoutFlags.PositionProportional);
        AbsoluteLayout.SetLayoutBounds(greenBox, new Rect(0.5, 0.1, boxSize, boxSize));
        AbsoluteLayout.SetLayoutFlags(greenBox, AbsoluteLayoutFlags.PositionProportional);
        AbsoluteLayout.SetLayoutBounds(blueBox, new Rect(0.85, 0.1, boxSize, boxSize));
        AbsoluteLayout.SetLayoutFlags(blueBox, AbsoluteLayoutFlags.PositionProportional);


        redSlider = CreateColorSlider(128);
        greenSlider = CreateColorSlider(128);
        blueSlider = CreateColorSlider(128);

        AbsoluteLayout.SetLayoutBounds(redSlider, new Rect(0.5, 0.25, 300, 40));
        AbsoluteLayout.SetLayoutFlags(redSlider, AbsoluteLayoutFlags.PositionProportional);
        AbsoluteLayout.SetLayoutBounds(greenSlider, new Rect(0.5, 0.33, 300, 40));
        AbsoluteLayout.SetLayoutFlags(greenSlider, AbsoluteLayoutFlags.PositionProportional);
        AbsoluteLayout.SetLayoutBounds(blueSlider, new Rect(0.5, 0.41, 300, 40));
        AbsoluteLayout.SetLayoutFlags(blueSlider, AbsoluteLayoutFlags.PositionProportional);


        stpCorner = new Stepper { Minimum = 0, Maximum = 100, Increment = 5, Value = 20 };
        stpCorner.ValueChanged += (s, e) => resultBox.CornerRadius = (float)e.NewValue;

        btnRandomColor = new Button { Text = "Random", HeightRequest = 40 };
        btnRandomColor.Clicked += OnRandomClicked;

        AbsoluteLayout.SetLayoutBounds(stpCorner, new Rect(0.2, 0.52, 100, 50));
        AbsoluteLayout.SetLayoutFlags(stpCorner, AbsoluteLayoutFlags.PositionProportional);
        AbsoluteLayout.SetLayoutBounds(btnRandomColor, new Rect(0.8, 0.52, 100, 40));
        AbsoluteLayout.SetLayoutFlags(btnRandomColor, AbsoluteLayoutFlags.PositionProportional);

        //result box
        resultBox = new BoxView { Color = Colors.Gray, CornerRadius = 20 };
        AbsoluteLayout.SetLayoutBounds(resultBox, new Rect(0.5, 0.9, 320, 200));
        AbsoluteLayout.SetLayoutFlags(resultBox, AbsoluteLayoutFlags.PositionProportional);

        // fix for overlapping pieces
        al.Children.Add(labelTitle);
        al.Children.Add(redBox);
        al.Children.Add(greenBox);
        al.Children.Add(blueBox);
        al.Children.Add(redSlider);
        al.Children.Add(greenSlider);
        al.Children.Add(blueSlider);
        al.Children.Add(stpCorner);
        al.Children.Add(btnRandomColor);
        al.Children.Add(resultBox);

        Content = al;

        //algv‰‰rtus
        UpdateAllColors(null, null);
    }

    private Slider CreateColorSlider(double startValue)
    {
        var s = new Slider { Minimum = 0, Maximum = 255, Value = startValue };
        s.ValueChanged += UpdateAllColors;
        return s;
    }

    private void UpdateAllColors(object sender, ValueChangedEventArgs e)
    {
        int r = Convert.ToInt32(redSlider.Value);
        int g = Convert.ToInt32(greenSlider.Value);
        int b = Convert.ToInt32(blueSlider.Value);

        redBox.Color = Color.FromRgb(r, 0, 0);
        greenBox.Color = Color.FromRgb(0, g, 0);
        blueBox.Color = Color.FromRgb(0, 0, b);

        Color finalColor = Color.FromRgb(r, g, b);
        resultBox.Color = finalColor;
        labelTitle.TextColor = finalColor;
    }

    private async void OnRandomClicked(object sender, EventArgs e)
    {
        Random rnd = new Random();

        redSlider.Value = rnd.Next(256);
        greenSlider.Value = rnd.Next(256);
        blueSlider.Value = rnd.Next(256);

        //animatsioon
        await resultBox.ScaleTo(1.05, 100);
        await resultBox.ScaleTo(1.0, 100);
    }
}