using UnityEngine;

namespace Runtime.CameraControl
{
	public interface ICameraController
	{
		public ICameraNavigation CameraNavigation { get; }
		
		public void ActivateSelection();
		public void DeactivateSelection();
	}
}