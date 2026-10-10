using System;
using System.Collections.Generic;
using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.Interaction
{
	public class InteractionController : MonoBehaviour, IInteractable
	{
		[SerializeField]
		private List<GameObject> _modelGameObjects = new();

		[SerializeField]
		private OutlineDataAsset _outlineDataAsset;

		private bool _isInteractionActive;
		
		public event Action HoverEvent;
		public event Action UnHoverEvent;
		public event Action SelectEvent;

		public void Initialize(bool isInteractionActive)
		{
			_isInteractionActive = isInteractionActive;
		}
		
		public void Hover()
		{
			if (!_isInteractionActive)
			{
				return;
			}
			
			foreach (var modelGameObject in _modelGameObjects)
			{
				var outline = modelGameObject.AddComponent<Outline>();
				outline.SetOutlineData(
					_outlineDataAsset.OutlineMode, 
					_outlineDataAsset.OutlineColor, 
					_outlineDataAsset.OutlineWidth);
			}
			
			HoverEvent?.Invoke();
		}

		public void UnHover()
		{
			if (!_isInteractionActive)
			{
				return;
			}

			foreach (var modelGameObject in _modelGameObjects)
			{
				if (modelGameObject.TryGetComponent(out Outline outline))
				{
					Destroy(outline);
				}
			}
			
			UnHoverEvent?.Invoke();
		}

		public void Select()
		{
			if (!_isInteractionActive)
			{
				return;
			}

			UnHover();
			SelectEvent?.Invoke();
		}
	}
}