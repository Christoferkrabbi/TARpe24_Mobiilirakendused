namespace TARpe24_Mobiilirakendused
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                if (count >= 10)
            {
                BotImage.IsVisible = false; // Peidab pildi
                CounterLabel.Text = "Pilt kadus ära! Vajuta Reset.";
            }
            if (count >= 5)
            {
                CounterBtn.BackgroundColor = Colors.Red;
                CounterBtn.TextColor = Colors.White;
            }
            CounterBtn.Text = $"Clicked {count} times";
            BotImage.Rotation += 20; //rotate image on every click, awesome

            BotImage.Opacity -= 0.1;

            var rnd = new Random();
            var rndColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            BackgroundColor = rndColor;

            if (count >= 10)
            {
                BotImage.IsVisible = false; // Peidab pildi
                CounterLabel.Text = "Pilt kadus ära! Vajuta Reset.";
            }

            SemanticScreenReader.Announce(CounterBtn.Text);

           
        }

        private void ResetBtn_Clicked(object sender, EventArgs e)
        {

            count = 0;
            CounterBtn.Text = "Click again";
            BotImage.IsVisible = true;
            BotImage.Rotation = 0;
            CounterLabel.Text = "pilt on tagasi";
            BackgroundColor = Colors.White;
            //ResetBtn.ClearValue(BackgroundColorProperty);
            CounterBtn.ClearValue(BackgroundColorProperty);
            BotImage.Opacity = 1;

            if (BotImage.HorizontalOptions == LayoutOptions.Start)
            {
                BotImage.HorizontalOptions = LayoutOptions.End;
            }
            else
            {
                BotImage.HorizontalOptions = LayoutOptions.Start;
            }




        }
    }
}
