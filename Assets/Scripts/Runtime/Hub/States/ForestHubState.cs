using Runtime.Views;

namespace Runtime.Hub
{
	public class ForestHubState : BaseHubState
	{
		public ForestHubState(HubContext context, LocationView locationView) : base(context, locationView) { }

		public override void PreEnter()
		{
			
		}

		public override void Enter()
		{
			LocationView.EnterLocation();
			
			Context.HubUIManager.ForestPanel.Open();
			Context.HubUIManager.ForestPanel.RaidMapButtonClickEvent += OnRaidMapButtonClickEventHandler;
			
			Context.CameraController.ActivateSelection();
		}

		public override void Exit()
		{
			LocationView.ExitLocation();
			
			Context.HubUIManager.ForestPanel.Close();
			Context.HubUIManager.ForestPanel.RaidMapButtonClickEvent -= OnRaidMapButtonClickEventHandler;
			
			Context.CameraController.DeactivateSelection();
		}

		private void OnRaidMapButtonClickEventHandler()
		{
			Context.HubStateMachine.SetState<RaidMapHubState>();
		}
	}
}