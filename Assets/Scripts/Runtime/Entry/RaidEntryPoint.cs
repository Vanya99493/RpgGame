using Runtime.CameraControl;
using Runtime.Level;
using Runtime.Management.RaidsManagement;
using UnityEngine;

namespace Runtime.Entry
{
	public class RaidEntryPoint : MonoBehaviour
	{
		[SerializeField]
		private CameraController _cameraController;
		
		[SerializeField]
		private RaidManager _raidManager;
		
		[SerializeField]
		private LevelBuilder _levelBuilder;
		
		private void Awake()
		{
			_cameraController.Initialize();
			
			_cameraController.ActivateSelection();
		}

		private void OnDestroy()
		{
			_cameraController.DeactivateSelection();
		}
	}
}