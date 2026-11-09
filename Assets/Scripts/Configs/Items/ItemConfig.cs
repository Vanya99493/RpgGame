using Runtime.Enums;
using UnityEngine;

namespace Configs.Items
{
	public class ItemConfig : ScriptableObject
	{
		[Header("Base")]
		public ItemType ItemType;
		public string Name;
		public Sprite IconSprite;
	}
}