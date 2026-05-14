using Microsoft.Maui.Controls;
using System;

namespace TARpe24_Mobiilirakendused.MemoGame
{
    public partial class MemoGame : ContentPage
    {
        private Game _game;

        public MemoGame()
        {
            InitializeComponent();

            _game = new Game("Mängija 1");

            _game.OnStatusUpdated += UpdateStatusUI;
            _game.OnGameWon += HandleGameWon;

            ThemePicker.SelectedIndex = 0; 
            LoadBestScore();
            StartNewGameLayout();
        }

        private void StartNewGameLayout()
        {
            _game.StartNewGame();
            GameGrid.Children.Clear();

            int cardIndex = 0;
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    var card = _game.Cards[cardIndex];

                    //Tap Gesture
                    var tapGesture = new TapGestureRecognizer();
                    tapGesture.Tapped += async (s, e) => await OnCardTapped(s, e);
                    card.GestureRecognizers.Add(tapGesture);

                    //Grid
                    Grid.SetRow(card, r);
                    Grid.SetColumn(card, c);
                    GameGrid.Children.Add(card);

                    cardIndex++;
                }
            }

            _game.CurrentTheme.Apply(this);
        }

        private async System.Threading.Tasks.Task OnCardTapped(object sender, EventArgs e)
        {
            if (sender is Card clickedCard)
            {
                await _game.SelectCardAsync(clickedCard);
            }
        }

        private void UpdateStatusUI(string statusText)
        {
            StatusLabel.Text = statusText;
        }

        private async void HandleGameWon()
        {
            LoadBestScore();
            await DisplayAlert("Võit!", $"Palju õnne, {_game.CurrentPlayer.Name}! Läbisid mängu {_game.CurrentPlayer.Moves} käiguga.", "OK");
        }

        private void LoadBestScore()
        {
            if (Preferences.ContainsKey("BestScore"))
            {
                int best = Preferences.Get("BestScore", 0);
                BestScoreLabel.Text = $"Parim tulemus (vähim käike): {best}";
            }
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            if (_game == null) return;

            _game.SetTheme(ThemePicker.SelectedIndex);
            _game.CurrentTheme.Apply(this);
        }

        private void OnNewGameClicked(object sender, EventArgs e)
        {
            StartNewGameLayout();
        }
    }
}
