using System.Collections.Generic;
using UnityEngine;

namespace Configs.Items
{
	[CreateAssetMenu(fileName = "ClothingItemsCollectionsContainerConfig", menuName = "Scriptable Objects/Items/Clothing/Clothing Items Collections Container Config")]
	public class ClothingItemsCollectionsContainerConfig : ScriptableObject
	{
		public List<ClothingItemsCollectionConfig> ClothingItemsCollections = new();
	}
}