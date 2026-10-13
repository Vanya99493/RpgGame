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
		private OutlineDataAsset _hoverOutlineDataAsset;
		
		[SerializeField]
		private OutlineDataAsset _selectionOutlineDataAsset;

		private bool _isInteractionActive;
		private bool _isSelected;
		
		public event Action HoverEvent;
		public event Action UnHoverEvent;
		public event Action SelectEvent;
		public event Action ReleaseEvent;

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
			
			ApplyOutline(_hoverOutlineDataAsset);
			
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
				ApplyOutline(_selectionOutlineDataAsset);
			}
			else
			{
				RemoveOutline();
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

			ApplyOutline(_selectionOutlineDataAsset);
			
			SelectEvent?.Invoke();
		}

		public void Release()
		{
			if (!_isInteractionActive)
			{
				return;
			}
			
			_isSelected = false;
			
			RemoveOutline();
			
			ReleaseEvent?.Invoke();
		}

		private void ApplyOutline(OutlineDataAsset outlineDataAsset)
		{
			foreach (var modelGameObject in _modelGameObjects)
			{
				if (modelGameObject.TryGetComponent(out Outline outline))
				{
					outline.SetOutlineData(
						outlineDataAsset.OutlineMode, 
						outlineDataAsset.OutlineColor, 
						outlineDataAsset.OutlineWidth);
				}
				else
				{
					var newOutline = modelGameObject.AddComponent<Outline>();
					newOutline.SetOutlineData(
						outlineDataAsset.OutlineMode, 
						outlineDataAsset.OutlineColor, 
						outlineDataAsset.OutlineWidth);
				}
			}
		}

		private void RemoveOutline()
		{
			foreach (var modelGameObject in _modelGameObjects)
			{
				if (modelGameObject.TryGetComponent(out Outline outline))
				{
					Destroy(outline);
				}
			}
		}
	}
}