using Runtime.Enums;
using Runtime.Interaction;
using Runtime.Interfaces;

namespace Runtime.CameraControl
{
	public class LocationSelectionState : BaseSelectionState
	{
		private ICameraNavigation _cameraNavigation;
		
		public LocationSelectionState(ICameraSelectionStateMachine selectionStateMachine, ICameraNavigation cameraNavigation) : base(selectionStateMachine)
		{
			_cameraNavigation = cameraNavigation;
		}
		
		public override void SelectObject(
			IInteractable interactable, 
			InputActionType inputActionType, 
			InputActionPhase inputActionPhase)
		{
			if (inputActionType == InputActionType.PrimaryAction)
			{
				_selectionStateMachine.ChangeSelectionState(interactable, inputActionType);

				if (interactable is NavigationInteractionController navigationInteractionController)
				{
					_cameraNavigation.NavigateToNode(navigationInteractionController.NavigationNode);
				}
			}
		}
	}
}