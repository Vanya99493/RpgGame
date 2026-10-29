using System;
using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.Interaction
{
	public class InteractionController : MonoBehaviour, IInteractable
	{
		[SerializeField]
		private GameObject _root;
		
		[SerializeField]
		private InteractionObjectType _interactionObjectType;

		[Space(10)]
		[SerializeField]
		private List<Collider> _interactionColliders = new();

		[Space(20)]
		[SerializeField]
		private InteractionVisualStrategy _interactionVisualizationStrategy;

		private bool _isInteractionActive;
		private bool _isSelected;

		public GameObject Root => _root;
		public InteractionObjectType InteractionObjectType => _interactionObjectType;
		
		public event Action HoverEvent;
		public event Action UnHoverEvent;
		public event Action SelectEvent;
		public event Action ReleaseEvent;

		public void Initialize(bool isInteractionActive)
		{
			_isInteractionActive = isInteractionActive;

			if (_isSelected && !_isInteractionActive)
			{
				_isSelected = false;
				_interactionVisualizationStrategy.Release();
			}
			
			foreach (var interactionCollider in _interactionColliders)
			{
				interactionCollider.enabled = isInteractionActive;
			}
		}

		public void Hover()
		{
			if (!_isInteractionActive)
			{
				return;
			}
			
			_interactionVisualizationStrategy.Hover();
			
			HoverEvent?.Invoke();
		}

		public void UnHover()
		{
			if (!_isInteractionActive)
			{
				return;
			}

			if (_isSelected)
			{
				_interactionVisualizationStrategy.Select();
			}
			else
			{
				_interactionVisualizationStrategy.UnHover();
			}
			
			UnHoverEvent?.Invoke();
		}

		public void Select()
		{
			if (!_isInteractionActive)
			{
				return;
			}
			
			_isSelected = true;

			_interactionVisualizationStrategy.Select();
			
			SelectEvent?.Invoke();
		}

		public void Release()
		{
			if (!_isInteractionActive)
			{
				return;
			}
			
			_isSelected = false;
			
			_interactionVisualizationStrategy.Release();
			
			ReleaseEvent?.Invoke();
		}
	}
}