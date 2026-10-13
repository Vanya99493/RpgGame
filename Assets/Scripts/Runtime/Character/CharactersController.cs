using System.Collections.Generic;
using Runtime.HexagonalGrid;
using Runtime.Models;
using UnityEngine;

namespace Runtime.Character
{
	public class CharactersController : MonoBehaviour
	{
		[SerializeField]
		private CharactersFactory _charactersFactory;
		
		private readonly List<CharacterController> _characters = new();

		private HexagonalGridController _hexagonalGridController;
		
		public void Initialize(HexagonalGridController hexagonalGridController)
		{
			_hexagonalGridController = hexagonalGridController;
		}
		
		public void CreateCharacters(List<CharacterInitializationData> initializationDataList)
		{
			foreach (CharacterInitializationData data in initializationDataList)
			{
				CreateCharacter(data);
			}
		}
		
		public void CreateCharacter(CharacterInitializationData initializationData)
		{
			var characterController = _charactersFactory.Create();
			characterController.Initialize(
				initializationData,
				_hexagonalGridController);
			_characters.Add(characterController);
		}
		
		public void DestroyAllCharacters()
		{
			foreach (CharacterController characterController in _characters)
			{
				characterController.Destroy();
			}
			_characters.Clear();
		}
	}
}