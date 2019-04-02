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
			yield return null;
			//handle all the game logic
			//build Map
			_gameManager.map.BuildMap(_gameManager.Data.Level.Value);
			//place player
			_gameManager.PlayerController.CanMove = true;
			//place a crusher
			_gameManager.map.Crusher.CanMove = true;
			
			while (_gameManager.Data.Lives > 0)
			{
				yield return null;
			}
		}
	}
}