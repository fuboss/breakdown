using StateMachines;
using UnityCore.Common;

namespace Breakdown
{
	public sealed class GameManager : OdinMonoSingleton<GameManager>
	{
		public GameData Data;
		public GameConfig Config;

		private GameController _controller;

		private void Start()
		{
			_controller = new GameController();
			_controller.StartGame(0);
		}

	}
}