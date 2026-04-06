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
            if (count >= 5)// mudab värvi kui klikid 5 korda
            {
                CounterBtn.BackgroundColor = Colors.Red;
                CounterBtn.TextColor = Colors.White;
            }
            CounterBtn.Text = $"Clicked {count} times";
            BotImage.Rotation += 20; //rotate image on every click, awesome

            BotImage.Opacity -= 0.1;//muudab pildi läibipaistvamaks iga klikiga

            var rnd = new Random(); //random värv taustal
            var rndColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            BackgroundColor = rndColor;

            if (count >= 10) // Peidab pildi
            {
                BotImage.IsVisible = false;
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
            CounterLabel.Text = "pilt on tagasi";//pilt
            BackgroundColor = Colors.White;
            CounterBtn.ClearValue(BackgroundColorProperty);//taastab nupu värvi
            BotImage.Opacity = 1; //taastab pildi läbipaistvuse

            if (BotImage.HorizontalOptions == LayoutOptions.Start)//liigutab pildi vasemale või paremale iga resetiga
            {
                BotImage.HorizontalOptions = LayoutOptions.End;
            }
            else
            {
                BotImage.HorizontalOptions = LayoutOptions.Start;
            }




        }
        private async void OnGoToValgusfoorClicked(object sender, EventArgs e)
        {
            // See rida avab uue ValgusfoorPage lehe
            await Navigation.PushAsync(new ValgusfoorPage());
        }

		private async void OnGoToLumememmClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new LumememmPage());
		}

		private async void OnGoToDateTimePageClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new DateTimePage());
		}
		private async void OnGoToStepperSliderPageClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new StepperSliderPage());
		}
		private async void OnGoToRGBClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new RGBpicker());
		}
		private async void OnGoToMysteryPageClicked(object sender, EventArgs e)
		{
			// See rida avab uue ValgusfoorPage lehe
			await Navigation.PushAsync(new MysteryPage());
		}
		private async void OnGoToTripsTrapsTrullPageClicked(object sender, EventArgs e)
		{
			// See rida avab uue ValgusfoorPage lehe
			await Navigation.PushAsync(new TripsTrapsTrullPage());
		}
	}
}
