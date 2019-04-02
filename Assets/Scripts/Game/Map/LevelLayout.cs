using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

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

		//editor helper
		[Button]
		private void AddRow(int brickIndex)
		{
			var row = new Row {bricks = new int[17]};
			for (var i = 0; i < row.bricks.Length; i++)
			{
				row.bricks[i] = brickIndex;
			}

			brickRows.Add(row);
		}
	}
}