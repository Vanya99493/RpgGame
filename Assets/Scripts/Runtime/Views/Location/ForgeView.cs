using Runtime.Hub;

namespace Runtime.Views
{
	public class ForgeView : LocationView
	{
		protected override void OnLocationSelectEventHandler()
		{
			_hubStateMachine.SetState<ForgeHubState>();
		}
	}
}