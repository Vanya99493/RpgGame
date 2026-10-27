using System.Collections.Generic;
using Runtime.Interaction;
using UnityEngine;

namespace Runtime.CameraControl
{
	public class CameraNavigationNode : MonoBehaviour
	{
		[SerializeField]
		private Transform _cameraNodeTransform;
		
		[Space(10)]
		[SerializeField]
		private List<InteractionController> _stateInteractions = new();
		
		public Transform CameraNodeTransform => _cameraNodeTransform;

		private void Awake()
		{
			ExitNode();
		}

		public void EnterNode()
		{
			SetInteractionActiveState(true);
		}

		public void ExitNode()
		{
			SetInteractionActiveState(false);
		}

		private void SetInteractionActiveState(bool isActive)
		{
			foreach (var interaction in _stateInteractions)
			{
				interaction.Initialize(isActive);
			}
		}
	}
}