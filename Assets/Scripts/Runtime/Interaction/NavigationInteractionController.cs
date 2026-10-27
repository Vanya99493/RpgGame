using Runtime.CameraControl;
using UnityEngine;

namespace Runtime.Interaction
{
	public class NavigationInteractionController : InteractionController
	{
		[SerializeField]
		private CameraNavigationNode _navigationNode;
		
		public CameraNavigationNode NavigationNode => _navigationNode;
	}
}