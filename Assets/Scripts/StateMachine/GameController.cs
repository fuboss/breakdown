using System;
using System.Collections.Generic;
using Breakdown;
using UnityEngine;

namespace StateMachines
{
	/// <summary>
	/// Very basic game state machine
	/// </summary>
	public sealed class GameController
	{
		private readonly Dictionary<string, IState> _states = new Dictionary<string, IState>();
		private IState _activeState;
		private IState _previousState;

		public GameController(GameManager manager)
		{
			var gameStarts = new GameStartsState(manager);
			var gameRuns = new GameRunsState(manager);
			var gameOver = new GameOverState(manager);

			_states.Add(gameStarts.Name, gameStarts);
			_states.Add(gameRuns.Name, gameRuns);
			_states.Add(gameOver.Name, gameOver);

			foreach (var pair in _states)
			{
				pair.Value.OnStateFinished += StateStarted;
				pair.Value.OnStateFinished += StateFinished;
			}

			_activeState = null;
		}

		public void StartGame(int level)
		{
			if (_activeState != null)
			{
				Debug.LogError("Game is already runs");
				return;
			}

			GameManager.Instance.Data.SetLevel(level);
			StartState("GameStarts");
		}

		private void StartState(string stateName)
		{
			//deactivate the currently active state
			if (_activeState != null && _activeState.IsActive)
				_activeState.Deactivate();
			_previousState = _activeState;

			//try activate desired one
			try
			{
				_activeState = _states[stateName];
				_activeState.Activate();
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}
		}

		private void StateFinished(IState state)
		{
			//double validation
			if (_activeState != null && _activeState != state)
			{
				Debug.LogError("Finished state is not the same as currently active state");
				return;
			}

			_previousState = state;

			//activate the next state
			switch (state.Name)
			{
				case "GameStarts":
					StartState("GameRuns");
					break;
				case "GameRuns":
					StartState("GameOver");
					break;
				case "GameOver":
					StartState("GameStarts");
					break;
				default:
					Debug.LogError("smth goes wrong");
					break;
			}
		}

		private void StateStarted(IState state)
		{
			//double validation
			if (_activeState != null && _activeState != state)
			{
				Debug.LogError("Started state is not the same as currently active state");
				return;
			}

			_activeState = state;
		}

		~GameController()
		{
			if (_states == null)
				return;

			foreach (var pair in _states)
			{
				pair.Value.OnStateFinished -= StateStarted;
				pair.Value.OnStateFinished -= StateFinished;
			}
		}
	}
}