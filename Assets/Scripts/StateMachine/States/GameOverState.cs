using System;
using System.Collections;
using Breakdown;

namespace StateMachines
{
	public class GameOverState : GameStateBase
	{
		public override string Name => "GameOver";
		
		public GameOverState(GameManager gameManager) : base(gameManager)
		{
		}
		
		protected override IEnumerator InternalProcess()
		{
			//handle the gameOver
			throw new NotImplementedException();
		}


	}
}