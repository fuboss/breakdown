using System;
using UnityEngine;

namespace UnityCore.Common.ObjectPool
{
	[Serializable]
	public class PoolSettings
	{
		[Tooltip("Использовать пулинг или нет")]
		public bool Pooling;

		[Tooltip("Кол-во объектов на старте")]
		public int PreloadCount;

		public PoolSettings()
		{
			PreloadCount = 0;
			Pooling = true;
		}
	}
}