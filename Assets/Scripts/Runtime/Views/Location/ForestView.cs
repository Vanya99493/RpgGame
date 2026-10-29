using Runtime.Hub;

namespace Runtime.Views
{
	public class ForestView : LocationView
	{
		protected override void OnLocationSelectEventHandler()
		{
			_hubStateMachine.SetState<ForestHubState>();
		}
	}
}