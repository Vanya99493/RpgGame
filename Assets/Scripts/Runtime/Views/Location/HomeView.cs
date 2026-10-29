using Runtime.Hub;

namespace Runtime.Views
{
	public class HomeView : LocationView
	{
		protected override void OnLocationSelectEventHandler()
		{
			_hubStateMachine.SetState<HomeHubState>();
		}
	}
}