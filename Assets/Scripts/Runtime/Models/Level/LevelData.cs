using System;

namespace Runtime.Models.Level
{
	[Serializable]
	public struct LevelData
	{
		public HexagonalGridData GridData;
		public CharacterData[] Characters;
	}
}