using System;
using System.Collections;
using Breakdown;

namespace StateMachines
{
	public class GameStartsState : GameStateBase
	{
		public GameStartsState(GameManager gameManager) : base(gameManager)
		{
		}

		public override string Name => "GameStarts";

		protected override IEnumerator InternalProcess()
		{
			//cleanup all the game data
			_gameManager.Data.Reset();
			//prepare 
			_gameManager.PlayerController.CanMove = false;
			
			//wait for User Input 
			yield break;
		}
	}
}