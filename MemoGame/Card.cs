using Microsoft.Maui.Controls;
using System;

namespace TARpe24_Mobiilirakendused;

public class Card : Frame
{
    public int Id { get; private set; }
    public string Value { get; private set; }
    public bool IsFaceUp { get; private set; }
    public bool IsMatched { get; private set; }

    private Label _contentLabel;
    private Color _themeCardColor;

    public Card(int id, string value, Color cardColor)
    {
        Id = id;
        Value = value;
        _themeCardColor = cardColor;
        IsFaceUp = false;
        IsMatched = false;

        Padding = 0;
        CornerRadius = 10;
        HeightRequest = 80;
        WidthRequest = 80;
        BackgroundColor = _themeCardColor;
        HorizontalOptions = LayoutOptions.Center;
        VerticalOptions = LayoutOptions.Center;

        _contentLabel = new Label
        {
            Text = "?",
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontSize = 24,
            FontAttributes = FontAttributes.Bold
        };

        Content = _contentLabel;
    }

    public void UpdateThemeColor(Color newColor)
    {
        _themeCardColor = newColor;
        if (!IsFaceUp) BackgroundColor = _themeCardColor;
    }

    public async System.Threading.Tasks.Task FlipAsync()
    {
        await this.RotateYTo(90, 150);

        IsFaceUp = !IsFaceUp;

        if (IsFaceUp)
        {
            _contentLabel.Text = Value;
            BackgroundColor = Colors.White;
            _contentLabel.TextColor = Colors.Black;
        }
        else
        {
            _contentLabel.Text = "?";
            BackgroundColor = _themeCardColor;
            _contentLabel.TextColor = Colors.White;
        }

        await this.RotateYTo(0, 150);
    }

    public void MarkMatched()
    {
        IsMatched = true;
        this.FadeTo(0.3, 250);
    }
}
