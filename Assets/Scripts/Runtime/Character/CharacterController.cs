using System;
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
		
		private HexagonalGridController _hexagonalGridController;
		private Vector2 _gridPosition;
		
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
		}

		private void UnsubscribeFromEvents()
		{
			_interactionController.SelectEvent -= OnSelectEventHandler;
		}
		
		private void OnSelectEventHandler()
		{
			// TODO: add ui activation
		}
	}
}