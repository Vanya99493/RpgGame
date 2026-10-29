using Runtime.Views;

namespace Runtime.Hub
{
	public class RaidMapHubState : BaseHubState
	{
		public RaidMapHubState(HubContext context, LocationView locationView) : base(context, locationView) { }

		public override void PreEnter()
		{
			
		}

		public override void Enter()
		{
			LocationView.EnterLocation();
			
			Context.HubUIManager.RaidMapPanel.Open();
			Context.HubUIManager.RaidMapPanel.CityButtonClickEvent += OnCityButtonClickEventHandler;
			
			Context.CameraController.ActivateSelection();
		}

		public override void Exit()
		{
			LocationView.ExitLocation();
			
			Context.HubUIManager.RaidMapPanel.Close();
			Context.HubUIManager.RaidMapPanel.CityButtonClickEvent -= OnCityButtonClickEventHandler;
			
			Context.CameraController.DeactivateSelection();
		}

		private void OnCityButtonClickEventHandler()
		{
			Context.HubStateMachine.SetState<CityHubState>();
		}
	}
}