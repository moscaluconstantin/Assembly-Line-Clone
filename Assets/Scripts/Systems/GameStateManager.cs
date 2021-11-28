using Systems;

namespace Scripts.Systems
{
	/// <summary>
	/// This class handles the game flow
	/// </summary>
	public static class GameStateManager
	{
		private static GameState currentState;

		public delegate void GameStateChangeDelegate(GameState gameState);

		public static event GameStateChangeDelegate onGameStateChange = delegate { };

		/// <summary>
		/// Gets or sets the Current Game State
		/// </summary>
		public static GameState CurrentState
		{
			get => currentState;

			set
			{
				if (currentState == value)
				{
					return;
				}

				currentState = value;

				onGameStateChange(currentState);
			}
		}
	}
}