using Runtime.Character;
using Runtime.Enums;
using UnityEngine;

namespace Configs.Items
{
	[CreateAssetMenu(fileName = "DefaultDressingStrategy", menuName = "Scriptable Objects/Items/Clothing/Default Dressing Strategy")]
	public class DefaultDressingStrategy : DressingStrategy
	{
		public override void ApplyVisibility(ICharacterClothingCustomization clothingCustomization, BodyCoverType bodyCoverType) { }
		public override void ResetVisibility(ICharacterClothingCustomization clothingCustomization, BodyCoverType bodyCoverType) { }
	}
}