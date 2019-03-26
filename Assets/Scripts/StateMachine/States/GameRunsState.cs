using System;
using System.Collections;

namespace StateMachines
{
	public class GameRunsState : GameStateBase
	{
		public override string Name => "GameRuns";
		protected override IEnumerator InternalProcess()
		{
			//handle all the game logic
			//build Map
			//place player
			//Initialize dependencies
			throw new NotImplementedException();
		}
	}
}