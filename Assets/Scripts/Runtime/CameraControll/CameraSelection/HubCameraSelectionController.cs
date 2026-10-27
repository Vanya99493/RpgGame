using Runtime.Enums;
using UnityEngine;

namespace Runtime.CameraControl
{
	public class HubCameraSelectionController : CameraSelectionController
	{
		[SerializeField]
		private CameraNavigationController _navigationController;
		
		public override void Initialize(Camera mainCamera)
		{
			_selectionStates = new()
			{
				{ InteractionObjectType.Location, new LocationSelectionState(this, _navigationController) },
			};
			
			_navigationController.Initialize(mainCamera);
			
			SetFreeSelectionState();
		}

		protected override void SetFreeSelectionState()
		{
			SetSelectionState(InteractionObjectType.Location, null);
		}
	}
}