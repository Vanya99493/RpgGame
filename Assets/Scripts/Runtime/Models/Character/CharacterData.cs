using System;

namespace Runtime.Models
{
	[Serializable]
	public struct CharacterData
	{
		public string Guid;
		public string Name;
		public int Health;
		public int MaxHealth;
		public int Energy;
		public int MaxEnergy;
	}
}