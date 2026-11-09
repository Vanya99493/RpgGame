using System.Collections.Generic;
using Runtime.Character;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Views.Character
{
	public class CharacterView : MonoBehaviour
	{
		[SerializeField]
		private CharacterCustomizationController _customizationController;

		private GenderType _genderType;
		
		public void Initialize(
			GenderType genderType,
			List<BodyPartType> defaultBodyPartsCollection)
		{
			_genderType = genderType;
			
			_customizationController.Initialize(
				genderType, 
				defaultBodyPartsCollection);
		}
	}
}