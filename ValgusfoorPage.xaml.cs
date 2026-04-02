namespace TARpe24_Mobiilirakendused;

public partial class ValgusfoorPage : ContentPage
{
    bool IsNightMode = false;
    bool onSees = false;
    IDispatcherTimer timer;
    int samm = 0; // 0=Punane, 1=Kollane, 2=Roheline
    bool liigubAlla = true;

    public ValgusfoorPage()
    {
        InitializeComponent();

        // Loome taimeri, mis käib iga 2 sekundi järel
        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(2);    
        timer.Tick += (s, e) => VahetaTuldAutomaatselt();


        // Lisame klikkimise võimaluse ringidele
        LisaTapSündmus(PunaneTuli, "Seisa!", Colors.Red);
        LisaTapSündmus(KollaneTuli, "Valmis?", Colors.Yellow);
        LisaTapSündmus(RohelineTuli, "Sõida!", Colors.Green);
    }

    private void LisaTapSündmus(Frame raam, string teade, Color tactiveColor)
    {
        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += async (s, e) =>
        {
            if (!onSees)
            {
                statusLabel.Text = "Lülita esmalt foor sisse";
                return;
            }

            await Task.WhenAll(
                raam.ScaleTo(1.2, 150),
                raam.FadeTo(0.5, 150)
            );

            statusLabel.Text = teade;

            await Task.WhenAll(
                raam.ScaleTo(1.0, 150),
                raam.FadeTo(1.0, 150)
            );
        };
        raam.GestureRecognizers.Add(tapGesture);
    }

    private void OnSisseClicked(object sender, EventArgs e)
    {
        onSees = true;
        samm = 0; //punasest
        timer.Start(); // tsükel
        statusLabel.Text = " ";
    }

    private void OnValjaClicked(object sender, EventArgs e)
    {
        onSees = false;
        timer.Stop();

        statusLabel.Text = "Lülita esmalt foor sisse";
        PunaneTuli.BackgroundColor = Colors.Gray;
        KollaneTuli.BackgroundColor = Colors.Gray;
        RohelineTuli.BackgroundColor = Colors.Gray;
    }

    private void OnDayNightModeClicked(object sender, EventArgs e)
    {
        DayNightMode();
    }

    private void VahetaTuldAutomaatselt()
    {
        //halliks
        PunaneTuli.BackgroundColor = Colors.Gray;
        KollaneTuli.BackgroundColor = Colors.Gray;
        RohelineTuli.BackgroundColor = Colors.Gray;

        if (IsNightMode) {
           if(samm==1)
            {
                KollaneTuli.BackgroundColor = Colors.Yellow;
                samm = 0;
            }
           else
            {
                samm = 1;
            }
            return;
        }
        //case
        switch (samm)
        {
            case 0: // PUNANE
                PunaneTuli.BackgroundColor = Colors.Red;
                samm = 1;
                liigubAlla = true; // Punaselt ainult kollasele
                break;

            case 1: // KOLLANE
                KollaneTuli.BackgroundColor = Colors.Yellow;
                // Kui, siis rohelisele. Kui roheliselt, siis punasele.
                samm = liigubAlla ? 2 : 0;
                break;

            case 2: // ROHELINE
                RohelineTuli.BackgroundColor = Colors.Green;
                samm = 1;
                liigubAlla = false; // Roheliselt ainult kollasele
                break;
        }
    }

    private void DayNightMode()
    {
        IsNightMode = !IsNightMode;
        if (IsNightMode)
        {
            if (onSees) { 
            timer.Interval = TimeSpan.FromSeconds(1);
            PunaneTuli.BackgroundColor = Colors.Gray;
            RohelineTuli.BackgroundColor = Colors.Gray;
            samm = 1;
            BackgroundColor = Colors.DarkBlue;
            }
            else{
                statusLabel.Text = "Lülita esmalt foor sisse";
            }
        }
        else
        {
            timer.Interval = TimeSpan.FromSeconds(2);
            samm = 0;
            BackgroundColor = Colors.White;
        }
    }
}