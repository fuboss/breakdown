using System.Collections;
using Breakdown;

namespace StateMachines
{
	public class GameRunsState : GameStateBase
	{
		public GameRunsState(GameManager gameManager) : base(gameManager)
		{
		}

		public override string Name => "GameRuns";


		protected override IEnumerator InternalProcess()
		{
			//handle all the game logic
			//build Map
			//place player
			_gameManager.PlayerController.CanMove = true;
			//place a crusher
			
			while (_gameManager.Data.Lives > 0)
			{
				yield return null;
			}
		}
	}
}