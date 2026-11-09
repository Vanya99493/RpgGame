using Runtime.Character;
using Runtime.Enums;
using UnityEngine;

namespace Configs.Items
{
	public abstract class DressingStrategy : ScriptableObject
	{
		public abstract void ApplyVisibility(
			ICharacterClothingCustomization clothingCustomization,
			BodyCoverType bodyCoverType); 
		
		public abstract void ResetVisibility(
			ICharacterClothingCustomization clothingCustomization,
			BodyCoverType bodyCoverType); 
	}
}