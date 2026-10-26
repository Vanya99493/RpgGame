using Runtime.Player;
using UnityEngine;

namespace Runtime.Entry
{
	public class HubEntryPoint : MonoBehaviour
	{
		[SerializeField]
		private CameraController _cameraController;

		private void Awake()
		{
			Initialize();
		}

		private void Initialize()
		{
			_cameraController.Initialize();
			
			_cameraController.ActivateSelection();
		}
	}
}