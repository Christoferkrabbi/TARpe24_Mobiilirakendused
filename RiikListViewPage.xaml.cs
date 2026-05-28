using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TARpe24_Mobiilirakendused;

public class Riik
{
    public string Nimi { get; set; }
    public string Pealinn { get; set; }
    public int Rahvaarv { get; set; }
    public string Lipp { get; set; }
}

public partial class RiikListViewPage : ContentPage
{
    public ObservableCollection<Riik> riigid { get; set; }
    string valitudPildiTee = "";

    public RiikListViewPage()
    {
        InitializeComponent();

        riigid = new ObservableCollection<Riik>
        {
            new Riik { Nimi="Eesti", Pealinn="Tallinn", Rahvaarv=1362954, Lipp="estonia.png" },
            new Riik { Nimi="Poola", Pealinn="Varssavi", Rahvaarv= 3797015, Lipp="poland.jpg" },
            new Riik { Nimi="Saksamaa", Pealinn="Berliin", Rahvaarv=83577140, Lipp="germany.svg" }
        };
        list.ItemsSource = riigid;
    }

    private async void BtnValiPilt_Clicked(object sender, EventArgs e)
    {
        try
        {
            var photo = await MediaPicker.Default.PickPhotoAsync();
            if (photo != null)
            {
                valitudPildiTee = photo.FullPath;
                lblValitudPilt.Text = $"Valitud: {photo.FileName}";
                lblValitudPilt.TextColor = Colors.Green;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Viga", "Pildi valimine ebaõnnestus: " + ex.Message, "OK");
        }
    }

    private void Lisa_Clicked(object sender, EventArgs e)
    {
        string uusNimi = entryNimi.Text;

        if (string.IsNullOrWhiteSpace(uusNimi))
        {
            DisplayAlert("Viga", "Täida kõik väljad", "OK");
            return;
        }

        bool riikOnOlemas = riigid.Any(r => r.Nimi.Equals(uusNimi, StringComparison.OrdinalIgnoreCase));

        int.TryParse(entryRahvaarv.Text, out int rahvaarv);
        string pildiNimi = string.IsNullOrWhiteSpace(valitudPildiTee) ? "default.png" : valitudPildiTee;

        if (riikOnOlemas)
        {
            DisplayAlert("Viga", "See riik on juba nimekirjas!", "OK");
        }
        else
        {
            riigid.Add(new Riik
            {
                Nimi = entryNimi.Text,
                Pealinn = entryPealinn.Text,
                Rahvaarv = rahvaarv,
                Lipp = pildiNimi
            });

            entryNimi.Text = entryPealinn.Text = entryRahvaarv.Text = "";
            valitudPildiTee = "";
            lblValitudPilt.Text = "Pilti pole valitud";
            lblValitudPilt.TextColor = Colors.Gray;
        }
    }

    private async void Kustuta_Clicked(object sender, EventArgs e)
    {
        if (list.SelectedItem is Riik valitudRiik)
        {
            bool kinnitus = await DisplayAlert("Kinnitus", $"Kustuta {valitudRiik.Nimi}?", "Jah", "Ei");
            if (kinnitus) riigid.Remove(valitudRiik);
        }
    }

    private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is Riik riik)
        {
            entryNimi.Text = riik.Nimi;
            entryPealinn.Text = riik.Pealinn;
            entryRahvaarv.Text = riik.Rahvaarv.ToString();

            lblValitudPilt.Text = $"Praegune pilt: {riik.Lipp}";

            btnUuenda.IsVisible = true;
            btnKustuta.IsVisible = true;


            await DisplayAlert(
	            "Riigi info",
	            $"Riigi nimi: {riik.Nimi}\nRiigi pealinn: {riik.Pealinn}\nRiigi rahvaarv: {riik.Rahvaarv}\n\nSaad nüüd muuta andmeid sisestuskastides.",
	            "Selge"
);
		}
    }

    private async void Uuenda_Clicked(object sender, EventArgs e)
    {
        if (list.SelectedItem is Riik valitudRiik)
        {
            int index = riigid.IndexOf(valitudRiik);

            if (index != -1)
            {
                int.TryParse(entryRahvaarv.Text, out int rahvaarv);

                var uuendatudRiik = new Riik
                {
                    Nimi = entryNimi.Text,
                    Pealinn = entryPealinn.Text,
                    Rahvaarv = rahvaarv,
                    Lipp = string.IsNullOrWhiteSpace(valitudPildiTee) ? valitudRiik.Lipp : valitudPildiTee
                };

                riigid[index] = uuendatudRiik;

                await DisplayAlert("Edu", "Andmed on uuendatud!", "OK");

                PuhastaValjad();
            }
        }
    }

    private void PuhastaValjad()
    {
        entryNimi.Text = entryPealinn.Text = entryRahvaarv.Text = "";
        valitudPildiTee = "";
        lblValitudPilt.Text = "Pilti pole valitud";
        list.SelectedItem = null;
        btnUuenda.IsVisible = false;
        btnKustuta.IsVisible = false;
    }
}