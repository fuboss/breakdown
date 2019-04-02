using System;
using System.Collections.Generic;

namespace Game.Map
{
	[Serializable]
	public  class LevelLayout
	{
		[Serializable]
		public class Row
		{
			public int[] bricks;
		}
		public List<Row> brickRows;
	}
}