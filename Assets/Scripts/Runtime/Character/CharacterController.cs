using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
		
		[SerializeField]
		private CharacterMovementController _movementController;
		
		private readonly List<HexagonalGridElement> _hoveredPossibleCellsToMove = new();
		
		private HexagonalGridController _hexagonalGridController;
		private Vector2Int _gridPosition;
		
		// Debug attribute
		[field: SerializeField]
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

		public async void Move(HexagonalGridElement destination)
		{
			List<HexagonalGridElement> moveGridPoints = _hexagonalGridController.GetPath(_gridPosition, destination.GridPosition);
			await _movementController.Move(
				transform,
				CharacterData.Energy, 
				moveGridPoints, 
				OnMovementEnd);
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
			_hoveredPossibleCellsToMove.AddRange(_hexagonalGridController.GetClosestGridElements(_gridPosition, CharacterData.Energy));

			foreach (var hoveredPossibleCell in _hoveredPossibleCellsToMove)
			{
				hoveredPossibleCell.ForceHover();
			}
				
			// TODO: add ui activation
		}

		private void OnReleaseEventHandler()
		{
			foreach (var hoveredPossibleCell in _hoveredPossibleCellsToMove)
			{
				hoveredPossibleCell.ForceUnhover();
			}
			
			_hoveredPossibleCellsToMove.Clear();
				
			// TODO: add ui deactivation
		}

		private void OnMovementEnd(HexagonalGridElement movementEndElement, int remainingEnergy)
		{
			if (movementEndElement != null)
			{
				transform.position = movementEndElement.CenterPosition;
				_gridPosition = movementEndElement.GridPosition;
			}
		}
	}
}