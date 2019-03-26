using UnityEngine;

namespace Breakdown
{
	[CreateAssetMenu(fileName = "GameConfig")]
	public sealed class GameConfig : ScriptableObject
	{
		public int defaultLives;
		
	}
}