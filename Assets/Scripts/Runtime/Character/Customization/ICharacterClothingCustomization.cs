using Runtime.Enums;

namespace Runtime.Character
{
	public interface ICharacterClothingCustomization
	{
		void HideBodyPart(BodyPartType typeToHide);
		void ShowBodyPart(BodyPartType typeToShow);
	}
}