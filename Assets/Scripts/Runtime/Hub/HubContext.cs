using Runtime.CameraControl;
using Runtime.UI.Core;

namespace Runtime.Hub
{
	public class HubContext
	{
		public HubUIManager HubUIManager { get; private set; }
		public ICameraController CameraController { get; private set; }
		public IHubStateMachine HubStateMachine { get; private set; }

		public HubContext(
			HubUIManager hubUIManager,
			ICameraController cameraController,
			IHubStateMachine hubStateMachine)
		{
			HubUIManager = hubUIManager;
			CameraController = cameraController;
			HubStateMachine = hubStateMachine;
		}
	}
}