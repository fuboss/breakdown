using System;
using System.Collections;

namespace StateMachines
{
	public class GameOverState : GameStateBase
	{
		public override string Name => "GameOver";
		protected override IEnumerator InternalProcess()
		{
			//handle the gameOver
			throw new NotImplementedException();
		}
	}
}