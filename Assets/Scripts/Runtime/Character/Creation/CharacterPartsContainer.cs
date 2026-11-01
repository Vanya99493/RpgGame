using System.Collections.Generic;
using Runtime.Models;
using UnityEngine;

namespace Runtime.Character
{
	public class CharacterPartsContainer : MonoBehaviour
	{
		[SerializeField]
		private List<BodyPartsCollection> _bodyPartsCollections = new();
		
		[SerializeField]
		private List<PairedBodyPartsCollection> _pairedBodyPartsCollections = new();

		public List<BodyPartsCollection> BodyPartsCollections => _bodyPartsCollections;
		public List<PairedBodyPartsCollection> PairedBodyPartsCollections => _pairedBodyPartsCollections;
	}
}