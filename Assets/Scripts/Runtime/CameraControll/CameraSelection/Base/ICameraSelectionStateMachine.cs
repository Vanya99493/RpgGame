using Runtime.Enums;
using Runtime.Interfaces;

namespace Runtime.CameraControl
{
	public interface ICameraSelectionStateMachine
	{
		IInteractable SelectedInteractableObject { get; }
		InteractionObjectType SelectedInteractableObjectType { get; }

		void ChangeSelectionState(IInteractable selectedInteractableObject, InputActionType inputActionType);
		void ReleaseSelection();
	}
}