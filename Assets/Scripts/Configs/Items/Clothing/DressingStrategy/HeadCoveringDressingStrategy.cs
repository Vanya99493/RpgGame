using Runtime.Character;
using Runtime.Enums;
using UnityEngine;

namespace Configs.Items
{
	[CreateAssetMenu(fileName = "HeadCoveringDressingStrategy", menuName = "Scriptable Objects/Items/Clothing/Head Covering Dressing Strategy")]
	public class HeadCoveringDressingStrategy : DressingStrategy
	{
		public override void ApplyVisibility(
			ICharacterClothingCustomization clothingCustomization, 
			BodyCoverType bodyCoverType)
		{
			if (bodyCoverType == BodyCoverType.ChangeToBase)
			{
				clothingCustomization.HideBodyPart(BodyPartType.Hair);
			}
			else if (bodyCoverType == BodyCoverType.HideOriginal)
			{
				clothingCustomization.HideBodyPart(BodyPartType.Hair);
				clothingCustomization.HideBodyPart(BodyPartType.Head);
			}
		}

		public override void ResetVisibility(
			ICharacterClothingCustomization clothingCustomization, 
			BodyCoverType bodyCoverType)
		{
			if (bodyCoverType == BodyCoverType.ChangeToBase)
			{
				clothingCustomization.ShowBodyPart(BodyPartType.Hair);
			}
			else if (bodyCoverType == BodyCoverType.HideOriginal)
			{
				clothingCustomization.ShowBodyPart(BodyPartType.Hair);
				clothingCustomization.ShowBodyPart(BodyPartType.Head);
			}
		}
	}
}