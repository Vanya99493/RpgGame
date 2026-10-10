using System;
using Patterns;
using Runtime.Interaction;
using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.HexagonalGrid
{
	public class HexagonalGridElement : MonoBehaviour, IDestroyable, IPoolable
	{
		[SerializeField]
		private GameObject _model;

		[SerializeField]
		private GameObject _frameModel;
		
		[SerializeField]
		private InteractionController _interactionController;

		public event Action<HexagonalGridElement> SelectEvent; 
		public event Action<IDestroyable> DestroyEvent;
		public event Action<IPoolable> ReturnToPoolEvent;

		private void OnEnable()
		{
			SubscribeOnEvents();
		}

		private void OnDestroy()
		{
			UnsubscribeFromEvents();
		}

		public void Initialize(bool isAvailable)
		{
			_interactionController.Initialize(isAvailable);
			InitVisual(isAvailable);
		}
		
		public void Destroy()
		{
			DestroyEvent?.Invoke(this);
		}

		public void ReturnToPool()
		{
			ReturnToPoolEvent?.Invoke(this);
		}

		private void SubscribeOnEvents()
		{
			_interactionController.SelectEvent += OnSelectEventHandler;
		}

		private void UnsubscribeFromEvents()
		{
			_interactionController.SelectEvent -= OnSelectEventHandler;
		}

		private void InitVisual(bool isAvailable)
		{
			if (!isAvailable)
			{
				var renderer = _model.GetComponent<Renderer>();

				var propertyBlock = new MaterialPropertyBlock();
				renderer.GetPropertyBlock(propertyBlock);

				propertyBlock.SetColor(
					"_BaseColor",
					new Color(1f, 1f, 1f, 0.2f)
				);

				renderer.SetPropertyBlock(propertyBlock);
			}
		}

		private void OnSelectEventHandler()
		{
			SelectEvent?.Invoke(this);
		}
	}
}