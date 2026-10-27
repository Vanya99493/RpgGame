using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Runtime.CameraControl
{
	public class CameraNavigationController : MonoBehaviour, ICameraNavigation
	{
		[SerializeField]
		private CameraNavigationNode _defaultNode;
		
		[Header("Settings")]
		[SerializeField]
		private float _movementDuration = 1f;

		private Camera _mainCamera;
		private CameraNavigationNode _currentNode;
		private Tween _cameraMoveTween;

		public void Initialize(Camera mainCamera)
		{
			_mainCamera = mainCamera;
			
			NavigateToNode(_defaultNode, 0);
		}
			
		public void NavigateToNode(CameraNavigationNode node, float movementDuration = -1)
		{
			movementDuration = movementDuration >= 0 
				? movementDuration 
				: _movementDuration;
			
			_currentNode?.ExitNode();
			MoveToNode(
				node.CameraNodeTransform, 
				movementDuration, 
				() =>
				{
					_currentNode = node;
					_currentNode.EnterNode();
				});
		}

		private void MoveToNode(Transform destinationNodeTransform, float movementDuration, Action callback = null)
		{
			_cameraMoveTween?.Kill();
			_cameraMoveTween = DOTween.Sequence()
				.Join(_mainCamera.transform.DOMove(destinationNodeTransform.position, movementDuration))
				.Join(_mainCamera.transform.DORotate(destinationNodeTransform.eulerAngles, movementDuration))
				.SetEase(Ease.Flash)
				.OnComplete(() =>
				{
					callback?.Invoke();
				});
		}
	}
}