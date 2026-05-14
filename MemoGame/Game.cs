using System;
using System.Collections.Generic;
using System.Linq;
using TARpe24_Mobiilirakendused;

namespace TARpe24_Mobiilirakendused
{
    public class Game
    {
        public Player CurrentPlayer { get; private set; }
        public List<Card> Cards { get; private set; }
        public List<Theme> Themes { get; private set; }
        public Theme CurrentTheme { get; private set; }

        private Card _firstSelectedCard;
        private Card _secondSelectedCard;
        private bool _isProcessing;

        public event Action OnGameWon;
        public event Action<string> OnStatusUpdated;

        public Game(string playerName)
        {
            CurrentPlayer = new Player(playerName);
            Cards = new List<Card>();
            _isProcessing = false;

            InitializeThemes();
            CurrentTheme = Themes[0];
        }

        private void InitializeThemes()
        {
            Themes = new List<Theme>
            {
                new Theme("Hele", Colors.LightGray, Colors.Black, Colors.DeepSkyBlue),
                new Theme("Tume", Color.FromArgb("#121212"), Colors.White, Color.FromArgb("#1E1E1E")),
                new Theme("Värviline", Colors.Moccasin, Colors.DarkViolet, Colors.Crimson)
            };
        }

        public void SetTheme(int index)
        {
            if (index >= 0 && index < Themes.Count)
            {
                CurrentTheme = Themes[index];
                foreach (var card in Cards)
                {
                    card.UpdateThemeColor(CurrentTheme.CardColor);
                }
            }
        }

        public void StartNewGame()
        {
            CurrentPlayer.Reset();
            Cards.Clear();
            _firstSelectedCard = null;
            _secondSelectedCard = null;
            _isProcessing = false;

            List<string> symbols = new List<string> { "🍎", "🍎", "🍌", "🍌", "🍇", "🍇", "🍉", "🍉", "🍒", "🍒", "🍓", "🍓", "🍍", "🍍", "🥑", "🥑" };

            Random rand = new Random();
            symbols = symbols.OrderBy(x => rand.Next()).ToList();

            for (int i = 0; i < symbols.Count; i++)
            {
                Cards.Add(new Card(i, symbols[i], CurrentTheme.CardColor));
            }

            OnStatusUpdated?.Invoke($"Käigud: {CurrentPlayer.Moves} | Paarid: {CurrentPlayer.Score}/8");
        }

        public async System.Threading.Tasks.Task<bool> SelectCardAsync(Card card)
        {
            if (_isProcessing || card.IsFaceUp || card.IsMatched) return false;

            await card.FlipAsync();

            if (_firstSelectedCard == null)
            {
                _firstSelectedCard = card;
                return true;
            }

            _secondSelectedCard = card;
            _isProcessing = true;
            CurrentPlayer.IncrementMoves();

            if (_firstSelectedCard.Value == _secondSelectedCard.Value)
            {
                _firstSelectedCard.MarkMatched();
                _secondSelectedCard.MarkMatched();
                CurrentPlayer.AddPoint();

                _firstSelectedCard = null;
                _secondSelectedCard = null;
                _isProcessing = false;

                OnStatusUpdated?.Invoke($"Käigud: {CurrentPlayer.Moves} | Paarid: {CurrentPlayer.Score}/8");

                if (CurrentPlayer.Score == 8)
                {
                    Microsoft.Maui.Storage.Preferences.Set("BestScore", CurrentPlayer.Moves);
                    OnGameWon?.Invoke();
                }
            }
            else
            {
                OnStatusUpdated?.Invoke($"Käigud: {CurrentPlayer.Moves} | Paarid: {CurrentPlayer.Score}/8");
                await System.Threading.Tasks.Task.Delay(1000);

                await _firstSelectedCard.FlipAsync();
                await _secondSelectedCard.FlipAsync();

                _firstSelectedCard = null;
                _secondSelectedCard = null;
                _isProcessing = false;
            }

            return true;
        }
    }
}
