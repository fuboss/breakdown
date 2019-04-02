using System;
using System.Collections;
using Breakdown;
using Breakdown.Sounds;
using UnityEngine;

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
			Debug.Log("game over");
			SoundManager.Instance.PlayGameOver();
			UIManager.Instance.ShowGameOver();
			yield break;
		}
	}
}