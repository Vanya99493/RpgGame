using Runtime.Hub;

namespace Runtime.Views
{
	public class RaidMapView : LocationView
	{
		protected override void OnLocationSelectEventHandler()
		{
			_hubStateMachine.SetState<RaidMapHubState>();
		}
	}
}