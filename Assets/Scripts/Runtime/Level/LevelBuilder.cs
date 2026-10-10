using Runtime.Character;
using Runtime.HexagonalGrid;
using Runtime.Models.Level;
using UnityEngine;

namespace Runtime.Level
{
	public class LevelBuilder : MonoBehaviour
	{
		[SerializeField]
		private HexagonalGridController _hexagonalGridController;
		
		[SerializeField]
		private CharactersController _charactersController;

		public void BuildLevel(LevelData levelData)
		{
			_hexagonalGridController.Initialize(levelData.GridData);
			
		}
	}
}