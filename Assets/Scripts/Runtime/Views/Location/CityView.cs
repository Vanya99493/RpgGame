using Runtime.Hub;

namespace Runtime.Views
{
	public class CityView : LocationView
	{
		protected override void OnLocationSelectEventHandler()
		{
			_hubStateMachine.SetState<CityHubState>();
		}
	}
}