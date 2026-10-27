namespace Runtime.Hub
{
	public abstract class BaseHubState
	{
		public abstract void PreEnter();
		public abstract void Enter();
		public abstract void Exit();
	}
}