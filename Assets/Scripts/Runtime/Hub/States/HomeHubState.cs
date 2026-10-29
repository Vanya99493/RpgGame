using Runtime.Views;

namespace Runtime.Hub
{
	public class HomeHubState : BaseHubState
	{
		public HomeHubState(HubContext context, LocationView locationView) : base(context, locationView) { }
		
		public override void PreEnter()
		{
			
		}

		public override void Enter()
		{
			LocationView.EnterLocation();
			
			Context.HubUIManager.HomePanel.Open();
			Context.HubUIManager.HomePanel.CityButtonClickEvent += OnCityButtonClickEventHandler;
			
			Context.CameraController.ActivateSelection();
		}

		public override void Exit()
		{
			LocationView.ExitLocation();
			
			Context.HubUIManager.HomePanel.Close();
			Context.HubUIManager.HomePanel.CityButtonClickEvent -= OnCityButtonClickEventHandler;
			
			Context.CameraController.DeactivateSelection();
		}

		private void OnCityButtonClickEventHandler()
		{
			Context.HubStateMachine.SetState<CityHubState>();
		}
	}
}