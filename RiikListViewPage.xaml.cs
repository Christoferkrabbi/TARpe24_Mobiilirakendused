using System.Collections.ObjectModel;

namespace TARpe24_Mobiilirakendused;

public class Riik
{
    public string Nimi { get; set; }
    public string Pealinn { get; set; }
    public int Rahvaarv { get; set; }
    public string Lipp { get; set; } // Hoiab pildi nime või seadme failiteed
}

public partial class RiikListViewPage : ContentPage
{
    // Globaalsed muutujad
    ObservableCollection<Riik> riigid;
    ListView list;
    Entry entryNimi, entryPealinn, entryRahvaarv;

    // Muutujad pildi valimise jaoks
    string valitudPildiTee = "";
    Label lblValitudPilt;

    public RiikListViewPage()
	{
        this.Title = "Euroopa riigid";

        // Algandmete laadimine
        riigid = new ObservableCollection<Riik>
            {
                new Riik { Nimi="Eesti", Pealinn="Tallinn", Rahvaarv=1362954, Lipp="eesti.png" },
                new Riik { Nimi="Poola", Pealinn="Varssavi", Rahvaarv= 3797015, Lipp="poola.png" },
                new Riik { Nimi="Saksamaa", Pealinn="Berliin", Rahvaarv=83577140, Lipp="saksa.png" }
            };
    }
}