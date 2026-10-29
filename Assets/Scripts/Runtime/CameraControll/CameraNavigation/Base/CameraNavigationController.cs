using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.CameraControl
{
	public class CameraNavigationController : MonoBehaviour, ICameraNavigation
	{
		[SerializeField]
		private float _movementDuration = 1f;

		private Camera _mainCamera;
		private Tween _cameraMoveTween;

		public void Initialize(Camera mainCamera)
		{
			_mainCamera = mainCamera;
		}
		
		public void MoveTo(
			Transform destinationTransform, 
			Action callback = null)
		{
			MoveTo(
				destinationTransform, 
				_movementDuration, 
				callback);
		}

		public void InstantlyMoveTo(
			Transform destinationTransform,
			Action callback = null)
		{
			MoveTo(
				destinationTransform, 
				0f, 
				callback);
		}
		
		private void MoveTo(
			Transform destinationTransform, 
			float movementDuration, 
			Action callback = null)
		{
			_cameraMoveTween?.Kill();
			_cameraMoveTween = DOTween.Sequence()
				.Join(_mainCamera.transform.DOMove(destinationTransform.position, movementDuration))
				.Join(_mainCamera.transform.DORotate(destinationTransform.eulerAngles, movementDuration))
				.SetEase(Ease.Flash)
				.OnComplete(() =>
				{
					callback?.Invoke();
				});
		}
	}
}