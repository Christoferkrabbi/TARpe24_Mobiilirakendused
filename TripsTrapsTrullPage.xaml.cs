namespace TARpe24_Mobiilirakendused;

public partial class TripsTrapsTrullPage : ContentPage
{
	string currentPlayer = "X";
	string[,] board;
	int boardSize = 3;
	bool isBotEnabled = false;
	bool isProcessing = false;
	Random rnd = new Random();

	public TripsTrapsTrullPage()
	{
		InitializeComponent();
		InitializeGame();
	}

	void InitializeGame()
	{
		GameGrid.Children.Clear();
		GameGrid.RowDefinitions.Clear();
		GameGrid.ColumnDefinitions.Clear();
		board = new string[boardSize, boardSize];

		for (int i = 0; i < boardSize; i++)
		{
			GameGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
			GameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
		}

		for (int r = 0; r < boardSize; r++)
		{
			for (int c = 0; c < boardSize; c++)
			{
				var btn = new Button { Text = "", FontSize = 24, Margin = 2 };
				int row = r, col = c;
				btn.Clicked += (s, e) => OnSquareClicked(btn, row, col);
				GameGrid.Add(btn, col, row);
			}
		}
		StatusLabel.Text = $"Mängija {currentPlayer} kord";
	}

	async void OnSquareClicked(Button btn, int r, int c)
	{
		// 1. KONTROLL: Kui bot mõtleb või ruut on täis, siis inimene klikkida ei saa
		if (isProcessing || !string.IsNullOrEmpty(board[r, c])) return;

		// 2. INIMESE KÄIK
		bool gameEnded = await PerformMove(btn, r, c);

		// 3. BOT-I KÄIK
		if (!gameEnded && isBotEnabled && currentPlayer == "O")
		{
			isProcessing = true;
			StatusLabel.Text = "Bot mõtleb...";

			await Task.Delay(1000);

			BotMove(); // BotMove kutsub nüüd otse PerformMove-i, mitte OnSquareClicked-i

			isProcessing = false; // Vabastame ekraani
		}
	}

	// UUS MEETOD: See teeb tegeliku käigu loogika
	async Task<bool> PerformMove(Button btn, int r, int c)
	{
		if (btn == null) return false;

		MakeMove(btn, r, c);

		if (CheckWin(r, c))
		{
			await DisplayAlert("Võit!", $"Mängija {currentPlayer} võitis!", "Uus mäng");
			ResetBoard();
			return true;
		}

		if (IsBoardFull())
		{
			await DisplayAlert("Viik!", "Mäng lõppes viigiga!", "Uus mäng");
			ResetBoard();
			return true;
		}

		SwitchPlayer();
		return false;
	}

	void BotMove()
	{
		var availableMoves = new List<(int r, int c)>();
		for (int r = 0; r < boardSize; r++)
			for (int c = 0; c < boardSize; c++)
				if (string.IsNullOrEmpty(board[r, c])) availableMoves.Add((r, c));

		if (availableMoves.Count > 0)
		{
			var move = availableMoves[rnd.Next(availableMoves.Count)];
			// Leiame nupu üles
			var btn = GameGrid.Children
				.OfType<Button>()
				.FirstOrDefault(b => Grid.GetRow(b) == move.r && Grid.GetColumn(b) == move.c);

			// KASUTAME OTSE PerformMove-i, et vältida isProcessing lukku
			_ = PerformMove(btn, move.r, move.c);
		}
	}
	void MakeMove(Button btn, int r, int c)
	{
		board[r, c] = currentPlayer;
		btn.Text = currentPlayer;
		btn.TextColor = (currentPlayer == "X") ? Colors.Blue : Colors.Red;
	}

	void SwitchPlayer()
	{
		currentPlayer = (currentPlayer == "X") ? "O" : "X";
		StatusLabel.Text = $"Mängija {currentPlayer} kord";
	}

	bool CheckWin(int r, int c)
	{
		// 1. Kontrolli rida
		bool rowWin = true;
		for (int i = 0; i < boardSize; i++) if (board[r, i] != currentPlayer) rowWin = false;
		if (rowWin) return true;

		// 2. Kontrolli veergu
		bool colWin = true;
		for (int i = 0; i < boardSize; i++) if (board[i, c] != currentPlayer) colWin = false;
		if (colWin) return true;

		// 3. Kontrolli peadiagonaali (ülevalt vasakult alla paremale)
		if (r == c)
		{
			bool diagWin = true;
			for (int i = 0; i < boardSize; i++) if (board[i, i] != currentPlayer) diagWin = false;
			if (diagWin) return true;
		}

		// 4. Kontrolli kõrvaldiagonaali (ülevalt paremalt alla vasakule)
		if (r + c == boardSize - 1)
		{
			bool diagWin = true;
			for (int i = 0; i < boardSize; i++) if (board[i, boardSize - 1 - i] != currentPlayer) diagWin = false;
			if (diagWin) return true;
		}

		return false;
	}

	bool IsBoardFull()
	{
		foreach (var cell in board) if (string.IsNullOrEmpty(cell)) return false;
		return true;
	}
	// --- NUPPUDE MEETODID ---

	void OnNewGameClicked(object sender, EventArgs e) => ResetBoard();

	void OnWhoStartsClicked(object sender, EventArgs e)
	{
		currentPlayer = rnd.Next(0, 2) == 0 ? "X" : "O";
		DisplayAlert("Loosimine", $"Alustab mängija: {currentPlayer}", "OK");
		ResetBoard();
	}

	void OnBotToggleClicked(object sender, EventArgs e)
	{
		isBotEnabled = !isBotEnabled;
		((Button)sender).Text = isBotEnabled ? "Bot: SEES" : "Bot: VÄLJAS";
		ResetBoard();
	}

	async void OnRulesClicked(object sender, EventArgs e)
	{
		await DisplayAlert("Reeglid", "Saa ritta (või diagonaali) oma sümbolid enne vastast!", "Sain aru");
	}

	void ResetBoard() { currentPlayer = "X"; InitializeGame(); }
}