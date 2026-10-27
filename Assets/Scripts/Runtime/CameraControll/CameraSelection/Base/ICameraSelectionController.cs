using Runtime.Enums;
using Runtime.Interfaces;

namespace Runtime.CameraControl
{
	public interface ICameraSelectionController
	{
		public void SelectObject(IInteractable interactable, InputActionType inputActionType, InputActionPhase inputActionPhase);
	}
}