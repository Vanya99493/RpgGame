using System.Collections.Generic;
using System.Linq;
using Configs.Character;
using Runtime.Character;
using Runtime.Enums;
using Runtime.Models;
using UnityEngine;

namespace Runtime.Views.Character
{
	public class CharacterView : MonoBehaviour
	{
		[SerializeField]
		private CharacterPartsContainer _characterPartsContainer;

		[Header("Configs")]
		[SerializeField]
		private BodyPartsDefinitionsConfig _bodyPartsDefinitionsConfig;

		private readonly List<BodyPartType> _defaultBodyParts = new();
		private readonly List<BodyPartData> _activeBodyParts = new();
		
		private GenderType _genderType;
		
		public void Initialize(
			GenderType genderType,
			List<BodyPartType> defaultBodyPartsCollection)
		{
			_genderType = genderType;
			
			_defaultBodyParts.Clear();
			_defaultBodyParts.AddRange(defaultBodyPartsCollection);

			DisableActiveBodyParts();

			foreach (var bodyPartType in _defaultBodyParts)
			{
				SetBodyPart(new BodyPartData()
				{
					BodyPartType = bodyPartType,
					BodyPartName = "",
				});
			}
		}
		
		public void SetBodyPart(BodyPartData bodyPartData)
		{
			var bodyPartsCollection = _characterPartsContainer.BodyPartsCollections
				.FirstOrDefault(x => x.BodyPartType == bodyPartData.BodyPartType);
			
			DisableBodyPart(
				bodyPartData.BodyPartType, 
				bodyPartsCollection);

			var newActiveBodyPart = bodyPartData.BodyPartName == ""
				? bodyPartsCollection?.BodyParts
					.First(x => x.GenderType == _genderType
										|| x.GenderType == GenderType.Unisex)
				: bodyPartsCollection?.BodyParts
					.FirstOrDefault(x => x.Name == bodyPartData.BodyPartName
												 && (x.GenderType == _genderType
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

		public (List<string>, bool) GetBodyPartNamesList(BodyPartType bodyPartType)
		{
			List<string> bodyPartNamesList = _characterPartsContainer.BodyPartsCollections
				.FirstOrDefault(x => x.BodyPartType == bodyPartType)?
				.BodyParts
				.Where(x => x.GenderType == _genderType 
									|| x.GenderType == GenderType.Unisex)
				.ToList()
				.Select(x => x.Name)
				.ToList() 
			       ?? new();
			
			bool isAvailableWithoutAnything = !_defaultBodyParts.Contains(bodyPartType);
			
			return (bodyPartNamesList, isAvailableWithoutAnything);
		}

		public int GetActiveBodyPartIndex(BodyPartType bodyPartType)
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
						
						if (bodyPartsCollection[i].GenderType == _genderType
						    || bodyPartsCollection[i].GenderType == GenderType.Unisex)
						{
							index++;
						}
					}
				}
			}

			return -1;
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
					bodyPartsCollection?.BodyParts
						.Where(x => x.Name == activeBodyPartData.BodyPartName)
						.ToList()
						.ForEach(bodyPart =>
						{
							bodyPart.SetActive(false);
						});
				});
		}
	}
}