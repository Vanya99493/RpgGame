namespace Runtime.CameraControl
{
	public interface ICameraNavigation
	{
		void NavigateToNode(CameraNavigationNode node, float movementDuration = -1);
	}
}