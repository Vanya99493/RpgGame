using Runtime.Enums;
using Runtime.Interfaces;

namespace Runtime.Player
{
	public interface ICameraSelectionStateMachine
	{
		IInteractable SelectedInteractableObject { get; }
		InteractionObjectType SelectedInteractableObjectType { get; }

		void ChangeSelectionState(IInteractable selectedInteractableObject, InputActionType inputActionType);
		void ReleaseSelection();
	}
}