using Runtime.Views;

namespace Runtime.Hub
{
	public class ShopHubState : BaseHubState
	{
		public ShopHubState(HubContext context, LocationView locationView) : base(context, locationView) { }
		
		public override void PreEnter()
		{
			
		}

		public override void Enter()
		{
			LocationView.EnterLocation();
			
			Context.HubUIManager.ShopPanel.Open();
			Context.HubUIManager.ShopPanel.CityButtonClickEvent += OnCityButtonClickEventHandler;
			
			Context.CameraController.ActivateSelection();
		}

		public override void Exit()
		{
			LocationView.ExitLocation();
			
			Context.HubUIManager.ShopPanel.Close();
			Context.HubUIManager.ShopPanel.CityButtonClickEvent -= OnCityButtonClickEventHandler;
			
			Context.CameraController.DeactivateSelection();
		}

		private void OnCityButtonClickEventHandler()
		{
			Context.HubStateMachine.SetState<CityHubState>();
		}
	}
}