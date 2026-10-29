using Runtime.Views;

namespace Runtime.Hub
{
	public class GuildHubState : BaseHubState
	{
		public GuildHubState(HubContext context, LocationView locationView) : base(context, locationView) { }
		
		public override void PreEnter()
		{
			
		}

		public override void Enter()
		{
			LocationView.EnterLocation();
			
			Context.HubUIManager.GuildPanel.Open();
			Context.HubUIManager.GuildPanel.CityButtonClickEvent += OnCityButtonClickEventHandler;
			
			Context.CameraController.ActivateSelection();
		}

		public override void Exit()
		{
			LocationView.ExitLocation();
			
			Context.HubUIManager.GuildPanel.Close();
			Context.HubUIManager.GuildPanel.CityButtonClickEvent -= OnCityButtonClickEventHandler;
			
			Context.CameraController.DeactivateSelection();
		}

		private void OnCityButtonClickEventHandler()
		{
			Context.HubStateMachine.SetState<CityHubState>();
		}
	}
}