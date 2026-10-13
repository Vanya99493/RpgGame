using Configs.Character;
using Runtime.Enums;
using Runtime.Models;
using UnityEngine;
using Utils;

namespace Runtime.Character
{
	public class RandomCharacterGenerator : MonoBehaviour
	{
		[Header("Configs")]
		[SerializeField]
		private RandomCharacterConfig _randomCharacterConfig;

		public CharacterData GenerateRandomCharacter(TeamType team)
		{
			int health = _randomCharacterConfig.GetRandomHealthValue();
			int energy = _randomCharacterConfig.GetRandomEnergyValue();
			
			return new CharacterData()
			{
				Guid = GuidGenerator.GenerateGuid(),
				Name = _randomCharacterConfig.GetRandomName(),
				Team = team,
				Health = health,
				MaxHealth = health,
				Energy = energy,
				MaxEnergy = energy,
			};
		}
	}
}