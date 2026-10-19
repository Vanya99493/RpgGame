using Runtime.Enums;
using Runtime.Interfaces;

namespace Runtime.Player.States.Base
{
	public abstract class BaseSelectionState
	{
		protected ICameraSelectionStateMachine _selectionStateMachine;
		
		public BaseSelectionState(ICameraSelectionStateMachine selectionStateMachine)
		{
			_selectionStateMachine = selectionStateMachine;
		}

		public virtual void Enter()
		{
			_selectionStateMachine.SelectedInteractableObject?.Select();
		}

		public virtual void Exit()
		{
			_selectionStateMachine.SelectedInteractableObject?.Release();
		}
		
		public abstract void SelectObject(IInteractable interactable, InputActionType inputActionType);
	}
}