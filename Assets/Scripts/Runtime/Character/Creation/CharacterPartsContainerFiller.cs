using System.Collections.Generic;
using System.Linq;
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
					if (_clothingPairType == ClothingPairType.Single)
					{
						bodyPartsCollection.BodyParts.AddRange(GenerateBodyParts());
					}
					
					isSet = true;
				}
			}

			if (!isSet)
			{
				_characterPartsContainer.BodyPartsCollections.Add(new BodyPartsCollection()
				{
					BodyPartType = _bodyPartType,
					BodyParts = GenerateBodyParts(),
				});
			}
			
			_bodyPartsToAdd.Clear();
			_pairedBodyPartsToAdd.Clear();
		}

		[Button]
		public void FillPairedBodyParts()
		{
			bool isSet = false;
			
			foreach (var pairedBodyPartsCollection in _characterPartsContainer.PairedBodyPartsCollections)
			{
				if (pairedBodyPartsCollection.BodyPartType == _bodyPartType)
				{
					if (_clothingPairType == ClothingPairType.Single)
					{
						pairedBodyPartsCollection.BodyParts.AddRange(GeneratePairedBodyParts());
					}
					
					isSet = true;
				}
			}

			if (!isSet)
			{
				_characterPartsContainer.PairedBodyPartsCollections.Add(new PairedBodyPartsCollection()
				{
					BodyPartType = _bodyPartType,
					BodyParts = GeneratePairedBodyParts(),
				});
			}
			
			_bodyPartsToAdd.Clear();
			_pairedBodyPartsToAdd.Clear();
		}

		[Button]
		public void RemoveTopHead()
		{
			BodyPartsCollection topHeadCollection = _characterPartsContainer.BodyPartsCollections.First(x => x.BodyPartType == BodyPartType.TopHead);
			BodyPartsCollection headCoveringCollection = _characterPartsContainer.BodyPartsCollections.First(x => x.BodyPartType == BodyPartType.HeadCovering);

			foreach (var topHead in topHeadCollection.BodyParts)
			{
				headCoveringCollection.BodyParts.Add(new BodyPart(topHead.GenderType, topHead.BodyCoverType, topHead.Reference));
			}
		}

		private List<BodyPart> GenerateBodyParts()
		{
			List<BodyPart> list = new();

			int index = 0;
			foreach (var bodyPartTransform in _bodyPartsToAdd)
			{
				list.Add(new BodyPart(_genderType, _bodyCoverType, bodyPartTransform));
			}

			return list;
		}
		
		private List<PairedBodyPart> GeneratePairedBodyParts()
		{
			List<PairedBodyPart> list = new();

			int index = 0;
			foreach (var bodyPartTransform in _bodyPartsToAdd)
			{
				list.Add(new PairedBodyPart(_genderType, _bodyCoverType, bodyPartTransform, _pairedBodyPartsToAdd[index++]));
			}

			return list;
		}
	}
}