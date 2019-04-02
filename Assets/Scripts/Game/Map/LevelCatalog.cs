using UnityEngine;

namespace Game.Map
{
	[CreateAssetMenu(menuName = "Map/Catalog")]
	public sealed class LevelCatalog : ScriptableObject
	{
		public LevelLayout[] levels;
	}
}