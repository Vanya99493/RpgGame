using System;
using System.Collections.Generic;
using Runtime.HexagonalGrid;
using Runtime.Interaction;
using Runtime.Interfaces;
using Runtime.Models;
using UnityEngine;

namespace Runtime.Character
{
	public class CharacterController : MonoBehaviour, IDestroyable
	{
		[SerializeField]
		private InteractionController _interactionController;
		
		private readonly List<HexagonalGridElement> _hoveredPossibleCellsToMove = new();
		
		private HexagonalGridController _hexagonalGridController;
		private Vector2Int _gridPosition;
		
		public CharacterData CharacterData { get; private set; }
		
		public event Action<IDestroyable> DestroyEvent;

		private void OnEnable()
		{
			SubscribeOnEvents();
		}

		private void OnDestroy()
		{
			UnsubscribeFromEvents();
		}

		public void Initialize(
			CharacterInitializationData initializationData,
			HexagonalGridController hexagonalGridController)
		{
			_hexagonalGridController = hexagonalGridController;
			
			CharacterData = initializationData.CharacterData;
			_gridPosition = initializationData.SpawnPosition;
			
			_interactionController.Initialize(true);

			transform.position = _hexagonalGridController.GetGridElementPosition(_gridPosition);
		}

		public void Move(HexagonalGridElement destination)
		{
			transform.position = destination.CenterPosition;
		}

		public void Destroy()
		{
			DestroyEvent?.Invoke(this);
		}
		
		private void SubscribeOnEvents()
		{
			_interactionController.SelectEvent += OnSelectEventHandler;
			_interactionController.ReleaseEvent += OnReleaseEventHandler;
		}

		private void UnsubscribeFromEvents()
		{
			_interactionController.SelectEvent -= OnSelectEventHandler;
			_interactionController.ReleaseEvent -= OnReleaseEventHandler;
		}
		
		private void OnSelectEventHandler()
		{
			Debug.Log("OnSelectEventHandler");
			_hoveredPossibleCellsToMove.AddRange(_hexagonalGridController.GetClosestGridElements(_gridPosition, CharacterData.Energy));

			foreach (var hoveredPossibleCell in _hoveredPossibleCellsToMove)
			{
				hoveredPossibleCell.ForceHover();
			}
				
			// TODO: add ui activation
		}

		private void OnReleaseEventHandler()
		{
			Debug.Log("OnReleaseEventHandler");
			foreach (var hoveredPossibleCell in _hoveredPossibleCellsToMove)
			{
				hoveredPossibleCell.ForceUnhover();
			}
			
			_hoveredPossibleCellsToMove.Clear();
				
			// TODO: add ui deactivation
		}
	}
}