using Runtime.Views;

namespace Runtime.Hub
{
	public abstract class BaseHubState
	{
		protected HubContext Context;
		protected LocationView LocationView;
		
		public ILocation Location => LocationView;

		public BaseHubState(
			HubContext context,
			LocationView locationView)
		{
			Context = context;
			LocationView = locationView;
		}
		
		public abstract void PreEnter();
		public abstract void Enter();
		public abstract void Exit();
	}
}