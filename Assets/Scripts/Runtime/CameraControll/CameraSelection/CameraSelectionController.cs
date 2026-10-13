using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.Player
{
	public class CameraSelectionController : MonoBehaviour, ICameraSelectionController
	{
		private IInteractable _interactable;
		
		public void SelectObject(IInteractable interactable)
		{
			_interactable?.Release();
			
			if (_interactable != interactable)
			{
				_interactable = interactable;
				_interactable.Select();
			}
			else
			{
				_interactable = null;
			}
		}

		public void ReleaseSelection()
		{
			_interactable?.Release();
			_interactable = null;
		}
	}
}