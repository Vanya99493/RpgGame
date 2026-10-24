using System;
using System.Collections.Generic;
using Patterns;
using Runtime.Bfs;
using Runtime.Interaction;
using Runtime.Interfaces;
using Runtime.Models;
using UnityEngine;

namespace Runtime.HexagonalGrid
{
	public class HexagonalGridElement : MonoBehaviour, IDestroyable, IPoolable, IBfsElement
	{
		[SerializeField]
		private GameObject _model;

		[SerializeField]
		private GameObject _frameModel;
		
		[SerializeField]
		private Transform _centerTransform;
		
		[SerializeField]
		private InteractionController _interactionController;

		private IHexagonalGridController _hexagonalGridController;
		private HexagonalGridElementModel _gridElementModel;

		public HexagonalGridElementData GridElementData => _gridElementModel.ElementData;
		public Vector2Int GridPosition => _gridElementModel.GridPosition;
		public Vector3 CenterPosition => _centerTransform.position;

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

		public void Initialize(
			IHexagonalGridController hexagonalGridController, 
			HexagonalGridElementData gridElementData,
			Vector2Int gridElementPosition)
		{
			_hexagonalGridController = hexagonalGridController;
			_gridElementModel = new()
			{
				ElementData = gridElementData,
				GridPosition = gridElementPosition,
			};
			
			_interactionController.Initialize(!GridElementData.IsBlocked);
			InitVisual(!GridElementData.IsBlocked);
		}
		
		public List<T> GetNeighbors<T>()
		{
			return _hexagonalGridController.GetNeighbors(this) as List<T>;
		}

		public void ForceHover()
		{
			_interactionController.Select();
		}

		public void ForceUnhover()
		{
			_interactionController.Release();
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
				_frameModel.SetActive(false);
			}
		}

		private void OnSelectEventHandler()
		{
			SelectEvent?.Invoke(this);
		}
	}
}