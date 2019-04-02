using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityCore.Common;
using UnityEngine;

namespace Breakdown.Sounds
{
	//TODO: use POOLS
	public class SoundManager : MonoSingleton<SoundManager>
	{
		public AudioClip liveLoose;
		public AudioClip playerHit;
		public AudioClip gameOver;
		public AudioClip[] brickBreak;

		[SerializeField]
		private AudioSource _audioSourceTemplate;
		private List<AudioSource> _sources;
		
		
		public void PlayBrickSound(int brickTier)
		{
			PlaySound(brickBreak[brickTier]);
		}

		public void PlayLiveLoose()
		{
			PlaySound(liveLoose);
		}
		
		public void PlayPlayerHit()
		{
			PlaySound(playerHit);
		}
		public void PlayGameOver()
		{
			PlaySound(gameOver);
		}

		private void PlaySound(AudioClip clip)
		{
			if (clip == null)
			{
				Debug.LogError("clip is null!", this);
				return;
			}

			var source = Instantiate(_audioSourceTemplate, transform);
			source.gameObject.name = clip.name;
			source.enabled = true;
			source.clip = clip;
			source.Play();
			_sources.Add(source);
		}
		
		private IEnumerator Start()
		{
			_sources = new List<AudioSource>(10);
			var killInterval = new WaitForSeconds(0.2f);
			while (true)
			{
				if(!enabled)
					yield break;
				yield return killInterval;

				//todo: optimize this
				var buffer = _sources.Where(s=>!s.isPlaying).ToArray();
				foreach (var toDestroy in buffer)
				{
					Destroy(toDestroy.gameObject);
				}

				_sources = _sources.Where(s => s.isPlaying).ToList();
			}
		}
	}
}