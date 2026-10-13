using Runtime.Interfaces;

namespace Runtime.Player
{
	public interface ICameraSelectionController
	{
		public void SelectObject(IInteractable interactable);
	}
}