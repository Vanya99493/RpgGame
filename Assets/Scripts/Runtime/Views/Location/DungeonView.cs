using Runtime.Hub;

namespace Runtime.Views
{
	public class DungeonView: LocationView
	{
		protected override void OnLocationSelectEventHandler()
		{
			_hubStateMachine.SetState<DungeonHubState>();
		}
	}
}