using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Configs.Animation;
using DG.Tweening;
using Extensions;
using Runtime.HexagonalGrid;
using UnityEngine;

namespace Runtime.Character
{
	public class CharacterMovementController : MonoBehaviour
	{
		[SerializeField]
		private Animator _animator;

		[Header("Settings")]
		[SerializeField]
		private float _oneUnityMovementTime = 0.5f;
		
		[Header("Configs")]
		[SerializeField]
		private AnimationParametersConfig _animationParametersConfig;

		private Tween _movementTween;
		
		public async Task Move(
			Transform character,
			int energy, 
			List<HexagonalGridElement> moveGridPoints, 
			Action<HexagonalGridElement, int> onCompleteCallback = null)
		{
			_animator.SetBool(_animationParametersConfig.MoveAnimationName, true);
			
			Queue<HexagonalGridElement> moveGridPointsQueue = moveGridPoints.ToQueue();
			HexagonalGridElement lastVisitedPoint = null;

			while (energy > 0
			       && moveGridPointsQueue.Count > 0)
			{
				lastVisitedPoint = moveGridPointsQueue.Dequeue();
				await MoveToPoint(character, lastVisitedPoint);
				energy--;
			}
			
			_animator.SetBool(_animationParametersConfig.MoveAnimationName, false);
			onCompleteCallback?.Invoke(lastVisitedPoint, energy);
		}

		private async Task MoveToPoint(
			Transform character,
			HexagonalGridElement moveTo)
		{
			Vector3 direction = moveTo.CenterPosition - character.position;
			direction.y = 0f;

			if (direction.sqrMagnitude > 0.001f)
			{
				transform.rotation = Quaternion.LookRotation(direction);
			}
			
			_movementTween?.Kill();
			_movementTween = character
				.DOMove(moveTo.CenterPosition, _oneUnityMovementTime)
				.SetEase(Ease.Linear);
			
			await _movementTween.AsyncWaitForCompletion();
		}
	}
}