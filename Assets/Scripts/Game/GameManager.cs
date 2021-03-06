using Breakdown.Player;
using Game.Map;
using Sirenix.OdinInspector;
using StateMachines;
using UnityCore.Common;

namespace Breakdown
{
	//basic game service locator
	public sealed class GameManager : OdinMonoSingleton<GameManager>
	{
		public GameConfig Config;
		public Map map;
		public PlayerController PlayerController;
		
		[ShowInInspector]
		public GameData Data { get; set; } //session data

		public int level = 0;
		
		private GameController _controller;

		private void Start()
		{
			Data = new GameData(Config);
			_controller = new GameController(this);
			_controller.StartGame(level);
		}
	}
}