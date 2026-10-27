using Runtime.Enums;
using UnityEngine;

namespace Runtime.CameraControl
{
	public class RaidCameraSelectionController : CameraSelectionController
	{
		public override void Initialize(Camera mainCamera)
		{
			_selectionStates = new()
			{
				{ InteractionObjectType.Undefined, new FreeSelectionState(this) },
				{ InteractionObjectType.Character, new CharacterControlSelectionState(this) },
				{ InteractionObjectType.Grid, new GridSelectionState(this) },
				{ InteractionObjectType.EnvironmentObject, new EnvironmentObjectSelectionState(this) },
			};
			
			SetFreeSelectionState();
		}

		protected override void SetFreeSelectionState()
		{
			SetSelectionState(InteractionObjectType.Undefined, null);
		}
	}
}