using System.Collections;
using TMPro;
using UnityCore.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Breakdown
{
	public sealed class UIManager : OdinMonoSingleton<UIManager>
	{
		[SerializeField]
		private TMP_Text _score;
		[SerializeField]
		private TMP_Text _lives;

		[SerializeField]
		private TMP_Text _gameOverLabel;
		[SerializeField]
		private Button _startNewGameButton;

		private void Awake()
		{
			_startNewGameButton.gameObject.SetActive(false);
			_gameOverLabel.gameObject.SetActive(false);
		}

		public void InitializeLabels()
		{
			_gameOverLabel.gameObject.SetActive(false);
			
			GameManager.Instance.Data.Lives.OnValueChanged += lives => _lives.text = lives.ToString();
			_lives.text = GameManager.Instance.Data.Lives.Value.ToString();
			GameManager.Instance.Data.Score.OnValueChanged += score => _score.text = score.ToString();
			_score.text = GameManager.Instance.Data.Score.Value.ToString();
		}

		public IEnumerator WaitStartGame()
		{
			_startNewGameButton.gameObject.SetActive(true);
			bool wait = true;
			_startNewGameButton.onClick.AddListener(()=>wait = false);
			yield return new WaitWhile(()=>wait);
			_startNewGameButton.gameObject.SetActive(false);
			_startNewGameButton.onClick.RemoveAllListeners();
		}

		public void ShowGameOver()
		{
			_gameOverLabel.gameObject.SetActive(true);
		}
	}
}