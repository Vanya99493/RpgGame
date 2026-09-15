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

		public event Action HoverEvent;
		public event Action UnHoverEvent;
		public event Action SelectEvent;

		public void Hover()
		{
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
			UnHover();
			SelectEvent?.Invoke();
		}
	}
}