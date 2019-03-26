using System;

namespace StateMachines
{
	public interface IState
	{
		string Name { get; }
		bool IsActive { get; }
		event Action<IState> OnStateStart;
		event Action<IState> OnStateFinished;
		void Activate();
		void Deactivate();
	}
}