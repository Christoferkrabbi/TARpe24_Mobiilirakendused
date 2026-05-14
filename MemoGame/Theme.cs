using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace TARpe24_Mobiilirakendused;

public class Theme
{
    public string Name { get; private set; }
    public Color BackgroundColor { get; private set; }
    public Color TextColor { get; private set; }
    public Color CardColor { get; private set; }
    public string FontFamily { get; private set; }

    public Theme(string name, Color bg, Color text, Color card, string font = "OpenSansRegular")
    {
        Name = name;
        BackgroundColor = bg;
        TextColor = text;
        CardColor = card;
        FontFamily = font;
    }

    public void Apply(ContentPage page)
    {
        if (page == null) return;

        page.BackgroundColor = BackgroundColor;

        ApplyToVisualTree(page.Content);
    }

    private void ApplyToVisualTree(Element element)
    {
        if (element == null) return;

        if (element is Label label)
        {
            label.TextColor = TextColor;
            label.FontFamily = FontFamily;
        }
        else if (element is Button button)
        {
            button.TextColor = TextColor;
            button.FontFamily = FontFamily;
        }

        if (element is IVisualTreeElement visualTree)
        {
            foreach (var child in visualTree.GetVisualChildren())
            {
                if (child is Element childElement)
                {
                    ApplyToVisualTree(childElement);
                }
            }
        }
    }
}
