using Runtime.CameraControl;
using Runtime.Hub;
using Runtime.InputControl;
using Runtime.UI.Core;
using UnityEngine;

namespace Runtime.Entry
{
	public class HubEntryPoint : MonoBehaviour
	{
		[SerializeField]
		private HubUIManager _hubUIManager;
		
		[SerializeField]
		private CameraController _cameraController;
		
		[SerializeField]
		private InputController _inputController;
		
		[SerializeField]
		private HubController _hubController;

		private void Awake()
		{
			Initialize();
			StartGame();
		}

		private void OnDestroy()
		{
			Dispose();
		}

		private void Initialize()
		{
			_inputController.Initialize();
			_hubUIManager.Initialize();
			_cameraController.Initialize(_inputController);
			
			HubContext context = CreateContext();
			
			_hubController.Initialize(context);
		}

		private void StartGame()
		{
			_hubController.SetStateWithoutTransition<CityHubState>();
		}

		private HubContext CreateContext()
		{
			return new HubContext(
				_hubUIManager,
				_cameraController,
				_hubController);
		}

		private void Dispose()
		{
			_cameraController.Dispose();
			_inputController.Dispose();
		}
	}
}