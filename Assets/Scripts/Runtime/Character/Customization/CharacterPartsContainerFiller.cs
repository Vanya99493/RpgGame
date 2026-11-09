using System.Collections.Generic;
using NaughtyAttributes;
using Runtime.Enums;
using Runtime.Models;
using UnityEngine;

namespace Runtime.Character
{
	public class CharacterPartsContainerFiller : MonoBehaviour
	{
		[SerializeField]
		private CharacterPartsContainer _characterPartsContainer;
		
		[Space(10)]
		[SerializeField]
		private BodyPartType _bodyPartType;
		
		[SerializeField]
		private ClothingPairType _clothingPairType;
		
		[SerializeField]
		private GenderType _genderType;

		[SerializeField]
		private BodyCoverType _bodyCoverType;
		
		[SerializeField]
		private List<Transform> _bodyPartsToAdd = new();
		
		[SerializeField]
		private List<Transform> _pairedBodyPartsToAdd = new();

		[Button]
		public void FillBodyParts()
		{
			bool isSet = false;
			
			foreach (var bodyPartsCollection in _characterPartsContainer.BodyPartsCollections)
			{
				if (bodyPartsCollection.BodyPartType == _bodyPartType)
				{
					bodyPartsCollection.BodyParts.AddRange(_clothingPairType == ClothingPairType.Single
						? GenerateBodyParts()
						: GeneratePairedBodyParts());
					
					isSet = true;
				}
			}

			if (!isSet)
			{
				_characterPartsContainer.BodyPartsCollections.Add(new BodyPartsCollection()
				{
					BodyPartType = _bodyPartType,
					BodyParts = _clothingPairType == ClothingPairType.Single
						? GenerateBodyParts()
						: GeneratePairedBodyParts(),
				});
			}
			
			_bodyPartsToAdd.Clear();
			_pairedBodyPartsToAdd.Clear();
		}

		private List<BodyPart> GenerateBodyParts()
		{
			List<BodyPart> list = new();

			int index = 0;
			foreach (var bodyPartTransform in _bodyPartsToAdd)
			{
				list.Add(new BodyPart(
					_genderType,
					_bodyCoverType,
					new List<BodyPartReference>()
					{
						new BodyPartReference() { Reference = bodyPartTransform, },
					}));
			}

			return list;
		}
		
		private List<BodyPart> GeneratePairedBodyParts()
		{
			List<BodyPart> list = new();

			int index = 0;
			foreach (var bodyPartTransform in _bodyPartsToAdd)
			{
				list.Add(new BodyPart(
					_genderType, 
					_bodyCoverType, 
					new List<BodyPartReference>()
					{
						new BodyPartReference() { Reference = bodyPartTransform, },
						new BodyPartReference() { Reference = _pairedBodyPartsToAdd[index++] },
					}));
			}

			return list;
		}
	}
}