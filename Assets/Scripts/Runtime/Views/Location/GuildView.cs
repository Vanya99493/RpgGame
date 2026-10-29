using Runtime.Hub;

namespace Runtime.Views
{
	public class GuildView : LocationView
	{
		protected override void OnLocationSelectEventHandler()
		{
			_hubStateMachine.SetState<GuildHubState>();
		}
	}
}