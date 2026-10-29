using Runtime.Enums;
using UnityEngine;

namespace Runtime.CameraControl
{
	public class HubCameraSelectionController : CameraSelectionController
	{
		public override void Initialize(Camera mainCamera)
		{
			_selectionStates = new()
			{
				{ InteractionObjectType.Location, new LocationSelectionState(this) },
			};
			
			SetFreeSelectionState();
		}

		protected override void SetFreeSelectionState()
		{
			SetSelectionState(InteractionObjectType.Location, null);
		}
	}
}