using Runtime.Interfaces;

namespace Runtime.Player.States.Base
{
	public abstract class BaseSelectionState
	{
		public abstract void SelectObject(IInteractable interactable);
		
		public abstract void Enter();
		public abstract void Preform();
		public abstract void Exit();
	}
}