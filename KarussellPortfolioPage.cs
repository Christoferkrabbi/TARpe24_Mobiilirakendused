using System.Collections.ObjectModel;

namespace TARpe24_Mobiilirakendused;

public class KarussellPortfolioPage : ContentPage
{
    public class CarouselItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string HelloWorldCode { get; set; }
        public string History { get; set; }
    }

    private CarouselView carouselView;
    private ObservableCollection<CarouselItem> items;
    private int position = 0;

    public KarussellPortfolioPage()
    {
        Title = "Programmeerimiskeelte portfoolio";

        // Initsialiseerime ObservableCollectioni keelte andmetega
        items = new ObservableCollection<CarouselItem>
        {
            new CarouselItem
            {
                Title = "C#",
                Description = "Võimas objektorienteeritud keel",
                ImageUrl = "csharp_logo.png",
                HelloWorldCode = "Console.WriteLine(\"Hello World!\");",
                History = "Loodi Microsofti poolt Anders Hejlsbergi juhtimisel " +
                "spetsiaalselt uue .NET platvormi jaoks. Algselt oli projekti salajane nimi COOL " +
                "(C-like Object Oriented Language). Keel disainiti konkureerima Javaga, pakkudes C++ " +
                "võimsust, kuid palju lihtsama süsteemiga."
            },
            new CarouselItem
            {
                Title = "Python",
                Description = "Suurepärane andmetöötluseks ja automatiseerimiseks",
                ImageUrl = "python_logo.png",
                HelloWorldCode = "print(\"Hello World\")",
                History = "Hollandi programmeerija Guido van Rossum alustas Pythoni " +
                "loomist 1989. aasta jõulupuhkuse ajal oma hobi- ja isikliku projektina. Ta tahtis " +
                "luua keele, mis oleks loetav nagu tavaline inglise keel. Nime sai keel hoopis teleri " +
                "komöödiasarja \"Monty Python's Flying Circus\" järgi, mitte mao järgi."
            },
            new CarouselItem
            {
                Title = "JavaScript",
                Description = "Veebiarenduse põhikeel",
                ImageUrl = "javascript_logo.png",
                HelloWorldCode = "console.log(\"Hello World\");",
                History = "Brendan Eich lõi selle keele Netscape brauseri jaoks kõigest 10 päevaga. " +
                "Alguses oli keele nimi Mocha, siis LiveScript ja lõpuks JavaScript. Viimane valiti puhtalt" +
                "turunduslikul eesmärgil, et lõigata kasu tollel ajal ülipopulaarse Java keele kuulsusest, " +
                "kuigi neil kahel keelel pole omavahel sisulist seost."
            },
            new CarouselItem
            {
                Title = "Java",
                Description = "Kirjuta kord, käivita igal pool",
                ImageUrl = "java_logo.png",
                HelloWorldCode = "System.out.println(\"Hello World\");",
                History = "James Gosling ja tema meeskond Sun Microsystems lõid selle keele algselt " +
                "hoopis interaktiivsete telerite ja digibokside jaoks, kuid see oli oma ajast ees ja turg " +
                "polnud valmis. Projekti esialgne nimi oli Oak (tammepuu järgi autori akna taga). Hiljem " +
                "nimetati see ümber Java kohvi järgi, mida programmeerijad suurtes kogustes joovad."
            },
            new CarouselItem
            {
                Title = "C++",
                Description = "Suur jõudlusega süsteemikeel",
                ImageUrl = "cplusplus_logo.png",
                HelloWorldCode = "std::cout << \"Hello World\";",
                History = "Bjarne Stroustrup hakkas seda keelt arendama AT&T Bell Labsis, kuna talle" +
                "meeldis C-keele kiirus, kuid ta tahtis lisada võimaluse koondada koodi \"objektideks\". " +
                "Enne ametlikku nime saamist kutsuti seda keelt lihtsalt \"C with Classes\"."
            }
        };

        // Karusselli loomine
        carouselView = new CarouselView
        {
            ItemsSource = items,
            HeightRequest = 400,
            PeekAreaInsets = new Thickness(40, 0, 40, 0),

            ItemTemplate = new DataTemplate(() =>
            {
                var frame = new Frame
                {
                    CornerRadius = 15,
                    HasShadow = true,
                    Padding = 0,
                    Margin = new Thickness(5),
                    BackgroundColor = Colors.Black
                };

                var grid = new Grid();

                // Keele logo pilt
                var image = new Image { Aspect = Aspect.AspectFit, Margin = new Thickness(20) };
                image.SetBinding(Image.SourceProperty, "ImageUrl");

                var gradient = new BoxView
                {
                    Background = new LinearGradientBrush
                    {
                        StartPoint = new Point(0, 1),
                        EndPoint = new Point(0, 0),
                        GradientStops = new GradientStopCollection
                        {
                            new GradientStop(Colors.Black.WithAlpha(0.8f), 0),
                            new GradientStop(Colors.Transparent, 0.6f)
                        }
                    }
                };

                // Tekstide konteiner
                var labelStack = new VerticalStackLayout
                {
                    VerticalOptions = LayoutOptions.End,
                    Margin = new Thickness(15),
                    Spacing = 5
                };

                var titleLabel = new Label
                {
                    TextColor = Colors.White,
                    FontSize = 24,
                    FontAttributes = FontAttributes.Bold
                };
                titleLabel.SetBinding(Label.TextProperty, "Title");

                var descLabel = new Label
                {
                    TextColor = Colors.LightGray,
                    FontSize = 14,
                    LineBreakMode = LineBreakMode.WordWrap
                };
                descLabel.SetBinding(Label.TextProperty, "Description");

                labelStack.Children.Add(titleLabel);
                labelStack.Children.Add(descLabel);

                grid.Children.Add(image);
                grid.Children.Add(gradient);
                grid.Children.Add(labelStack);

                frame.Content = grid;

                // Vajutamise sündmus (DisplayAlert kuvamine)
                var tapGesture = new TapGestureRecognizer();
                tapGesture.Tapped += async (s, e) =>
                {
                    if (frame.BindingContext is CarouselItem selectedItem)
                    {
                        await DisplayAlert(
                            selectedItem.Title,
                            $"Koodinäide:\n{selectedItem.HelloWorldCode}\n\nAjalugu:\n{selectedItem.History}",
                            "Sule"
                        );
                    }
                };
                frame.GestureRecognizers.Add(tapGesture);

                return frame;
            })
        };

        var indicatorView = new IndicatorView
        {
            IndicatorColor = Colors.LightGray,  
            SelectedIndicatorColor = Colors.DarkSlateBlue,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 10)
        };
        carouselView.IndicatorView = indicatorView;

        Device.StartTimer(TimeSpan.FromSeconds(2), () =>
        {
            if (items == null || items.Count == 0) return false;

            position = (position + 1) % items.Count;
            carouselView.Position = position;
            return true;
        });

        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Padding = 20,
                Spacing = 10,
                Children =
                {
                    carouselView,
                    indicatorView
                }
            }
        };
    }
}
