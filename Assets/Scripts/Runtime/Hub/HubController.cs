using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Hub
{
	public class HubController : MonoBehaviour, IHubStateMachine
	{
		private Dictionary<Type, BaseHubState> _states = new();
		private BaseHubState _currentState;
		
		public void SetState<T>()
		{
			_currentState?.Exit();
			_currentState = _states[typeof(T)];
			_currentState.PreEnter();
			TransitionToState(() =>
			{
				_currentState.Enter();
			});
		}

		private void TransitionToState(Action callback = null)
		{
			callback?.Invoke();
		}
	}
}