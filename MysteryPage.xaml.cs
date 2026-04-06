namespace TARpe24_Mobiilirakendused;

public partial class MysteryPage : ContentPage
{
	public MysteryPage()
	{
		InitializeComponent();
	}

	int oiged = 0;
	int valed = 0;

	private async void OnAlustaManguClicked(object sender, EventArgs e)
	{
		oiged = 0;
		valed = 0;

		// 1. Tehnoloogia (Yes/No)
		bool v1 = await DisplayAlert("Tehnika", "Kas Wi-Fi tähistab 'Wireless Fidelity'?", "Jah", "Ei");
		LisaPunkt(v1 == false); // Tegelikult ei tähista see ametlikult midagi!

		// 2. Loodus (Yes/No)
		bool v2 = await DisplayAlert("Loodus", "Kas heli liigub vees kiiremini kui õhus?", "Jah", "Ei");
		LisaPunkt(v2 == true);

		// 3. Kosmos (Prompt - kasutaja peab trükkima)
		string planeet = await DisplayPromptAsync("Kosmos", "Mis on Päikesesüsteemi suurim planeet?", "Vasta", "Loobu");
		if (planeet?.ToLower() == "jupiter") LisaPunkt(true);
		else LisaPunkt(false);

		// 4. Geograafia (Yes/No)
		bool v4 = await DisplayAlert("Maailm", "Kas Islandil on rohkem vulkaane kui inimesi?", "Jah", "Ei");
		LisaPunkt(v4 == false); // Islandil on ~130 vulkaani ja ~370 000 inimest.

		// 5. Loomad (ActionSheet - valikuga küsimus)
		string loom = await DisplayActionSheet("Kes on kiireim maismaaloom?", "Loobu", null, "Gepard", "Lõvi", "Antiloop");
		LisaPunkt(loom == "Gepard");

		await ShowSummary();
	}

	private void LisaPunkt(bool correct)
	{
		if (correct) oiged++;
		else valed++;
	}
	private async Task ShowSummary()
	{
		double protsent = (double)oiged / (oiged + valed) * 100;
		string ikoon = protsent >= 50 ? "🏆" : "🧠";

		await DisplayAlert("Mängu lõpp!",
			$"{ikoon} Sinu tulemus:\n\n" +
			$"✅ Õigeid vastuseid: {oiged}\n" +
			$"❌ Valesid vastuseid: {valed}\n" +
			$"📊 Täpsus: {protsent:F0}%",
			"Lõpeta");
	}
	private async void OnStiilClicked(object sender, EventArgs e)
	{
		// 3. Valik nimekirjast (ActionSheet)
		string tegevus = await DisplayActionSheet("Vali taustavärv:", "Tühista", null, "Hele", "Tume", "Roheline");

		switch (tegevus)
		{
			case "Hele": BackgroundColor = Colors.White; break;
			case "Tume": BackgroundColor = Colors.Grey; StyleButton.BackgroundColor = Colors.Black; break;
			case "Roheline": BackgroundColor = Colors.LightGreen; break;
		}
	}
}
