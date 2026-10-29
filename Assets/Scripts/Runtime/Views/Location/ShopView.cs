using Runtime.Hub;

namespace Runtime.Views
{
	public class ShopView : LocationView
	{
		protected override void OnLocationSelectEventHandler()
		{
			_hubStateMachine.SetState<ShopHubState>();
		}
	}
}