using UnityCore.ReactiveProperties;

namespace Breakdown
{
	public class GameData
	{
		private readonly GameConfig _config;
		public ReactiveProperty<int> Lives { get; private set; }
		public ReactiveProperty<int> Score { get; private set; }
		public ReactiveProperty<int> Level { get; private set; }

		public GameData(GameConfig config)
		{
			_config = config;

			Reset();
		}

		public void Reset()
		{
			Lives = new ReactiveProperty<int>(_config.defaultLives);
			Score.Value = new ReactiveProperty<int>();
			Level.Value = new ReactiveProperty<int>();
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