using Runtime.Views;

namespace Runtime.Hub
{
	public class DungeonHubState : BaseHubState
	{
		public DungeonHubState(HubContext context, LocationView locationView) : base(context, locationView) { }

		public override void PreEnter()
		{
			
		}

		public override void Enter()
		{
			LocationView.EnterLocation();
			
			Context.HubUIManager.DungeonPanel.Open();
			Context.HubUIManager.DungeonPanel.RaidMapButtonClickEvent += OnRaidMapButtonClickEventHandler;
			
			Context.CameraController.ActivateSelection();
		}

		public override void Exit()
		{
			LocationView.ExitLocation();
			
			Context.HubUIManager.DungeonPanel.Close();
			Context.HubUIManager.DungeonPanel.RaidMapButtonClickEvent -= OnRaidMapButtonClickEventHandler;
			
			Context.CameraController.DeactivateSelection();
		}

		private void OnRaidMapButtonClickEventHandler()
		{
			Context.HubStateMachine.SetState<RaidMapHubState>();
		}
	}
}