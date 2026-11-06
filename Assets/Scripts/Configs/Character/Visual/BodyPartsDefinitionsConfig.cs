using System.Collections.Generic;
using Runtime.Enums;
using UnityEngine;

namespace Configs.Character
{
	[CreateAssetMenu(fileName = "BodyPartsDefinitionsConfig",menuName = "Scriptable Objects/Character/Character Body Parts Definitions")]
	public class BodyPartsDefinitionsConfig : ScriptableObject
	{
		public List<BodyPartType> SingleClothingTypes = new();
		public List<BodyPartType> PairedClothingTypes = new();
	}
}