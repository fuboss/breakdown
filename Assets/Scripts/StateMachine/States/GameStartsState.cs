using System;
using System.Collections;

namespace StateMachines
{
	public class GameStartsState : GameStateBase
	{
		public override string Name => "GameStarts";
		protected override IEnumerator InternalProcess()
		{
			//cleanup all the game data
			//prepare 
			throw new NotImplementedException();
		}
	}
}