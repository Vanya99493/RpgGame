using Runtime.Enums;
using Runtime.Interfaces;
using Runtime.Player.States.Base;

namespace Runtime.Player.States
{
	public class EnvironmentObjectSelectionState : BaseSelectionState
	{
		public EnvironmentObjectSelectionState(ICameraSelectionStateMachine selectionStateMachine) : base(selectionStateMachine) { }

		public override void SelectObject(IInteractable interactable, InputActionType inputActionType)
		{
			if (inputActionType == InputActionType.PrimaryAction)
			{
				_selectionStateMachine.ChangeSelectionState(interactable, inputActionType);
			}
			else
			{
				_selectionStateMachine.ReleaseSelection();
			}
		}
	}
}