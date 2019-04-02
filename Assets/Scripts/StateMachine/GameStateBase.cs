using System;
using System.Collections;
using Breakdown;
using UnityCore.Common;
using UnityCore.Extensions;
using UnityEngine;

namespace StateMachines
{
	public abstract class GameStateBase : IState
	{
		protected readonly GameManager _gameManager;
		public abstract string Name { get; }
		public bool IsActive { get; protected set; }
		public event Action<IState> OnStateStart;
		public event Action<IState> OnStateFinished;
        
		private Coroutine _process;

		protected GameStateBase(GameManager gameManager)
		{
			_gameManager = gameManager;
		}
        
		public void Activate()
		{
			if(IsActive) 
				return;

			IsActive = true;
			_process = CoroutineHelper.Instance.StartCoroutine(Process());
			OnStateStart.SafeRaise(this);
		}

		public void Deactivate()
		{
			if(!IsActive)
				return;
			if (_process != null)
			{
				CoroutineHelper.Instance.StopCoroutine(_process);
				_process = null;
			}

			IsActive = false;
			OnStateFinished.SafeRaise(this);
		}
        
		protected abstract IEnumerator InternalProcess();

		protected virtual void OnActivate()
		{
            
		}
        
		protected virtual void OnDeactivate()
		{
            
		}
       
        
		private IEnumerator Process()
		{
			yield return InternalProcess();
			Deactivate();
		}

	}
}