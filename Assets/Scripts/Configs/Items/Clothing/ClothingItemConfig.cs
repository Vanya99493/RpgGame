using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Models;
using UnityEngine;

namespace Configs.Items
{
	[CreateAssetMenu(fileName = "ClothingItemConfig", menuName = "Scriptable Objects/Items/Clothing/Clothing Item Config")]
	public class ClothingItemConfig : ItemConfig
	{
		[Header("Clothing")]
		public BodyCoverType BodyCoverType;

		[Space(10)]
		public List<ClothingPartData> References = new();
		
		[Space(20)]
		public DressingStrategy DressingStrategy;
	}
}