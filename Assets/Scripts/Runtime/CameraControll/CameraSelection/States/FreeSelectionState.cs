using Runtime.Enums;
using Runtime.Interfaces;

namespace Runtime.CameraControl
{
	public class FreeSelectionState : BaseSelectionState
	{
		public FreeSelectionState(ICameraSelectionStateMachine selectionStateMachine) : base(selectionStateMachine) { }

		public override void SelectObject(
			IInteractable interactable, 
			InputActionType inputActionType, 
			InputActionPhase inputActionPhase)
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