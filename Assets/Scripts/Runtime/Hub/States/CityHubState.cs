using Runtime.Views;

namespace Runtime.Hub
{
	public class CityHubState : BaseHubState
	{
		public CityHubState(HubContext context, LocationView locationView) : base(context, locationView) { }

		public override void PreEnter()
		{
			
		}

		public override void Enter()
		{
			LocationView.EnterLocation();
			
			Context.HubUIManager.CityPanel.Open();
			Context.HubUIManager.CityPanel.RaidButtonClickEvent += OnRaidButtonClickEventHandler;
			
			Context.CameraController.ActivateSelection();
		}

		public override void Exit()
		{
			LocationView.ExitLocation();
			
			Context.HubUIManager.CityPanel.Close();
			Context.HubUIManager.CityPanel.RaidButtonClickEvent -= OnRaidButtonClickEventHandler;
			
			Context.CameraController.DeactivateSelection();
		}

		private void OnRaidButtonClickEventHandler()
		{
			Context.HubStateMachine.SetState<RaidMapHubState>();
		}
	}
}