using Sirenix.OdinInspector;
using UnityCore.ReactiveProperties;

namespace Breakdown
{
	public class GameData
	{
		private readonly GameConfig _config;

		public ReactiveProperty<int> Lives;
		public ReactiveProperty<int> Score;
		public ReactiveProperty<int> Level;

		public GameData(GameConfig config)
		{
			_config = config;

			Reset();
		}

		public void Reset()
		{
			Lives = new ReactiveProperty<int>(_config.defaultLives);
			Score = new ReactiveProperty<int>();
			Level = new ReactiveProperty<int>();
		}

		public void ChangeLives(int delta)
		{
			Lives.Value += delta;
		}

		public void ChangeScore(int delta)
		{
			Score.Value += delta;
		}

		public void SetLevel(int level)
		{
			Level.Value = level;
		}
	}
}