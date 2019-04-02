using Breakdown.Player;
using Sirenix.OdinInspector;
using StateMachines;
using UnityCore.Common;

namespace Breakdown
{
	public sealed class GameManager : OdinMonoSingleton<GameManager>
	{
		public GameConfig Config;
		public MapController MapController;
		public PlayerController PlayerController;
		
		[ShowInInspector]
		public GameData Data { get; set; }
		
		
		private GameController _controller;

		private void Start()
		{
			Data = new GameData(Config);
			_controller = new GameController(this);
			_controller.StartGame(0);
		}
	}
}