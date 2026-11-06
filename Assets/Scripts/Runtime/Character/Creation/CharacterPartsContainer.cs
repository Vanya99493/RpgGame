using System.Collections.Generic;
using Runtime.Models;
using UnityEngine;

namespace Runtime.Character
{
	public class CharacterPartsContainer : MonoBehaviour
	{
		[SerializeField]
		private List<BodyPartsCollection> _bodyPartsCollections = new();

		public List<BodyPartsCollection> BodyPartsCollections => _bodyPartsCollections;

		/*[Button]
		public void ValidateFields()
		{
			foreach (var bodyPartsCollection in _bodyPartsCollections)
			{
				foreach (var bodyPart in bodyPartsCollection.BodyParts)
				{
					bodyPart.References.Add(new BodyPartReference()
					{
						Reference = bodyPart.Reference,
					});
				}
			}

			foreach (var pairedBodyPartsCollection in _pairedBodyPartsCollections)
			{
				List<BodyPart> bodyParts = new();
				BodyPartsCollection bodyPartsCollection = new()
				{
					BodyPartType = pairedBodyPartsCollection.BodyPartType,
				};

				foreach (var pairedBodyPart in pairedBodyPartsCollection.BodyParts)
				{
					List<BodyPartReference> bodyPartReferences = new()
					{
						new BodyPartReference() { Reference = pairedBodyPart.Reference },
						new BodyPartReference() { Reference = pairedBodyPart.PairedReference }
					};
					
					bodyParts.Add(new BodyPart(
						pairedBodyPart.GenderType,
						pairedBodyPart.BodyCoverType,
						bodyPartReferences));
				}
				
				bodyPartsCollection.BodyParts = bodyParts;
				_bodyPartsCollections.Add(bodyPartsCollection);
			}
		}*/
	}
}