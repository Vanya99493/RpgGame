using Runtime.Views;

namespace Runtime.Hub
{
	public class ForgeHubState : BaseHubState
	{
		public ForgeHubState(HubContext context, LocationView locationView) : base(context, locationView) { }
		
		public override void PreEnter()
		{
			
		}

		public override void Enter()
		{
			LocationView.EnterLocation();
			
			Context.HubUIManager.ForgePanel.Open();
			Context.HubUIManager.ForgePanel.CityButtonClickEvent += OnCityButtonClickEventHandler;
			
			Context.CameraController.ActivateSelection();
		}

		public override void Exit()
		{
			LocationView.ExitLocation();
			
			Context.HubUIManager.ForgePanel.Close();
			Context.HubUIManager.ForgePanel.CityButtonClickEvent -= OnCityButtonClickEventHandler;
			
			Context.CameraController.DeactivateSelection();
		}

		private void OnCityButtonClickEventHandler()
		{
			Context.HubStateMachine.SetState<CityHubState>();
		}
	}
}