using Runtime.Enums;
using Runtime.Interfaces;

namespace Runtime.CameraControl
{
	public class LocationSelectionState : BaseSelectionState
	{
		public LocationSelectionState(ICameraSelectionStateMachine selectionStateMachine) : base(selectionStateMachine) { }
		
		public override void SelectObject(
			IInteractable interactable, 
			InputActionType inputActionType, 
			InputActionPhase inputActionPhase)
		{
			if (inputActionType == InputActionType.PrimaryAction)
			{
				_selectionStateMachine.ChangeSelectionState(interactable, inputActionType);
			}
		}
	}
}