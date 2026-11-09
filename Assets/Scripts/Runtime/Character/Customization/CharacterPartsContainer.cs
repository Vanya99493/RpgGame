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
	}
}