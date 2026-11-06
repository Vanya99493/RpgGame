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

		private readonly List<BodyPartData> _activeBodyParts = new();
		private GenderType _genderType;
		
		public void Initialize(GenderType genderType)
		{
			_genderType = genderType;
		}
		
		public void SetBodyPart(BodyPartData bodyPartData)
		{
			var bodyPartsCollection = _characterPartsContainer.BodyPartsCollections
				.FirstOrDefault(x => x.BodyPartType == bodyPartData.BodyPartType);
			
			_activeBodyParts
				.Where(x => x.BodyPartType == bodyPartData.BodyPartType)
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

			var newActiveBodyPart = bodyPartData.BodyPartName == ""
				? bodyPartsCollection?.BodyParts
					.First(x => x.GenderType == _genderType)
				: bodyPartsCollection?.BodyParts
					.FirstOrDefault(x => x.Name == bodyPartData.BodyPartName
												 && x.GenderType == _genderType);

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

		public void SetBodyPart()
		{
			
		}
	}
}