using System.Collections.Generic;
using Runtime.Enums;
using UnityEngine;

namespace Configs.Character
{
	[CreateAssetMenu(fileName = "DefaultCharacterPartsConfig", menuName = "Scriptable Objects/Character/Default Character Parts Config")]
	public class DefaultCharacterPartsConfig : ScriptableObject
	{
		public List<BodyPartType> BodyPartTypes = new();
	}
}