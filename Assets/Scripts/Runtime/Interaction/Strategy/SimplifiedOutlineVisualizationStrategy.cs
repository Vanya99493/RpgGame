using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Interaction.Strategy
{
	public class SimplifiedOutlineVisualizationStrategy : InteractionVisualStrategy
	{
		[SerializeField]
		private List<GameObject> _modelGameObjects = new();
		
		[Header("Configs")]
		[SerializeField]
		private OutlineDataAsset _hoverOutlineDataAsset;
		
		public override void Hover()
		{
			ApplyOutline(_hoverOutlineDataAsset);
		}

		public override void UnHover()
		{
			RemoveOutline();
		}

		public override void Select() { }

		public override void Release()
		{
			RemoveOutline();
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