using System;
using System.Collections.Generic;
using System.Linq;
using Configs.Items;
using Runtime.Enums;
using Runtime.Models;
using UnityEngine;

namespace Runtime.Character
{
	public class CharacterCustomizationController : MonoBehaviour, ICharacterClothingCustomization
	{
		[SerializeField]
		private CharacterPartsContainer _characterPartsContainer;

		[SerializeField]
		private ClothingItemsCollectionsContainerConfig _clothingItemsCollectionsContainerConfig;

		private readonly List<BodyPartType> _defaultBodyParts = new();
		private readonly List<BodyPartData> _activeBodyParts = new();
		private readonly List<BodyPartData> _hidedBodyParts = new();
		
		public void Initialize(
			GenderType genderType,
			List<BodyPartType> defaultBodyPartsCollection)
		{
			_defaultBodyParts.Clear();
			_defaultBodyParts.AddRange(defaultBodyPartsCollection);
			_hidedBodyParts.Clear();

			DisableActiveBodyParts();

			foreach (var bodyPartType in _defaultBodyParts)
			{
				SetBodyPart(
					genderType,
					new BodyPartData()
					{
						BodyPartType = bodyPartType,
						BodyPartName = "",
					});
			}
		}
		
		public void SetBodyPart(
			GenderType genderType,
			BodyPartData bodyPartData)
		{
			var bodyPartsCollection = _characterPartsContainer.BodyPartsCollections
				.FirstOrDefault(x => x.BodyPartType == bodyPartData.BodyPartType);
			
			DisableBodyPart(
				bodyPartData.BodyPartType, 
				bodyPartsCollection);

			var newActiveBodyPart = bodyPartData.BodyPartName == ""
				? bodyPartsCollection?.BodyParts
					.First(x => x.GenderType == genderType
										|| x.GenderType == GenderType.Unisex)
				: bodyPartsCollection?.BodyParts
					.FirstOrDefault(x => x.Name == bodyPartData.BodyPartName
												 && (x.GenderType == genderType
												 || x.GenderType == GenderType.Unisex));

			if (newActiveBodyPart != null)
			{
				newActiveBodyPart.SetActive(true);
				_activeBodyParts.Add(new BodyPartData()
				{
					BodyPartType = bodyPartData.BodyPartType,
					BodyPartName = newActiveBodyPart.Name,
				});
			}
			else
			{
				Debug.LogError($"Could not find and activate body part {bodyPartData.BodyPartType}");
			}
		}

		public void DisableBodyPart(BodyPartType bodyPartType)
		{
			var bodyPartsCollection = _characterPartsContainer.BodyPartsCollections
				.FirstOrDefault(x => x.BodyPartType == bodyPartType);
			
			DisableBodyPart(
				bodyPartType, 
				bodyPartsCollection);
		}

		public (List<string>, bool) GetBodyPartNamesList(
			GenderType genderType, 
			BodyPartType bodyPartType)
		{
			List<string> bodyPartNamesList = _characterPartsContainer.BodyPartsCollections
				.FirstOrDefault(x => x.BodyPartType == bodyPartType)?
				.BodyParts
				.Where(x => x.GenderType == genderType 
									|| x.GenderType == GenderType.Unisex)
				.ToList()
				.Select(x => x.Name)
				.ToList() 
			       ?? new();
			
			bool isAvailableWithoutAnything = !_defaultBodyParts.Contains(bodyPartType);
			
			return (bodyPartNamesList, isAvailableWithoutAnything);
		}

		public int GetActiveBodyPartIndex(
			GenderType genderType, 
			BodyPartType bodyPartType)
		{
			foreach (var bodyPartData in _activeBodyParts)
			{
				if (bodyPartData.BodyPartType == bodyPartType)
				{
					var bodyPartsCollection = _characterPartsContainer.BodyPartsCollections
						.FirstOrDefault(x => x.BodyPartType == bodyPartData.BodyPartType)?.BodyParts 
					                          ?? new();

					for (int i = 0, index = 0; i < bodyPartsCollection.Count; i++)
					{
						if (bodyPartsCollection[i].Name == bodyPartData.BodyPartName)
						{
							return index;
						}
						
						if (bodyPartsCollection[i].GenderType == genderType
						    || bodyPartsCollection[i].GenderType == GenderType.Unisex)
						{
							index++;
						}
					}
				}
			}

			return -1;
		}

		public void HideBodyPart(BodyPartType typeToHide)
		{
			foreach (var activeBodyPartData in _activeBodyParts)
			{
				if (typeToHide == activeBodyPartData.BodyPartType)
				{
					_hidedBodyParts.Add(activeBodyPartData);
					_activeBodyParts.Remove(activeBodyPartData);
						
					var bodyPartsCollection = _characterPartsContainer.BodyPartsCollections
						.FirstOrDefault(x => x.BodyPartType == typeToHide);
					
					DisableBodyPart(
						bodyPartsCollection, 
						activeBodyPartData.BodyPartName);
					
					break;
				}
			}
		}

		public void ShowBodyPart(BodyPartType typeToShow)
		{
			foreach (var hidedBodyPartData in _hidedBodyParts)
			{
				if (typeToShow == hidedBodyPartData.BodyPartType)
				{
					_activeBodyParts.Add(hidedBodyPartData);
					_hidedBodyParts.Remove(hidedBodyPartData);
					
					var bodyPartsCollection = _characterPartsContainer.BodyPartsCollections
						.FirstOrDefault(x => x.BodyPartType == typeToShow);
					
					EnableBodyPart(
						bodyPartsCollection, 
						hidedBodyPartData.BodyPartName);
					
					break;
				}
			}
		}

		private void DisableActiveBodyParts()
		{
			List<BodyPartType> activeBodyTypes = _activeBodyParts
				.Select(x => x.BodyPartType)
				.ToList();
			
			foreach (var activeBodyType in activeBodyTypes)
			{
				DisableBodyPart(activeBodyType);
			}
		}

		private void DisableBodyPart(
			BodyPartType bodyPartType,
			BodyPartsCollection bodyPartsCollection)
		{
			_activeBodyParts
				.Where(x => x.BodyPartType == bodyPartType)
				.ToList()
				.ForEach(activeBodyPartData =>
				{
					_activeBodyParts.Remove(activeBodyPartData);
					
					DisableBodyPart(
						bodyPartsCollection,
						activeBodyPartData.BodyPartName);
				});
		}

		private void DisableBodyPart(
			BodyPartsCollection bodyPartsCollection,
			string bodyPartName)
		{
			DoSmthWithBodyPart(
				bodyPartsCollection, 
				bodyPartName,
				bodyPart => bodyPart.SetActive(false));
		}

		private void EnableBodyPart(
			BodyPartsCollection bodyPartsCollection,
			string bodyPartName)
		{
			DoSmthWithBodyPart(
				bodyPartsCollection, 
				bodyPartName,
				bodyPart => bodyPart.SetActive(true));
		}

		private void DoSmthWithBodyPart(
			BodyPartsCollection bodyPartsCollection,
			string bodyPartName,
			Action<BodyPart> smth = null)
		{
			bodyPartsCollection?.BodyParts
				.Where(x => x.Name == bodyPartName)
				.ToList()
				.ForEach(bodyPart => smth?.Invoke(bodyPart));
		}
	}
}