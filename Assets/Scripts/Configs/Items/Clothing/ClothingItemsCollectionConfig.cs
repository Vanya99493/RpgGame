using System.Collections.Generic;
using Runtime.Enums;
using UnityEngine;

namespace Configs.Items
{
	[CreateAssetMenu(fileName = "ClothingItemsCollectionConfig", menuName = "Scriptable Objects/Items/Clothing/Clothing Items Collection Config")]
	public class ClothingItemsCollectionConfig : ScriptableObject
	{
		public ClothingType ClothingType;
		public List<ClothingItemConfig> Items = new();
	}
}