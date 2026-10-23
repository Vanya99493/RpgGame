using System.Collections.Generic;
using Configs.HexagonalGrid;
using Extensions;
using Runtime.Character;
using Runtime.Enums;
using Runtime.Level;
using Runtime.Models;
using Runtime.Models.Level;
using Runtime.Player;
using UnityEngine;

namespace Runtime.Entry
{
	public class EntryPoint : MonoBehaviour
	{
		[SerializeField]
		private CameraController _cameraController;
		
		[SerializeField]
		private LevelBuilder _levelBuilder;

		[Header("Helpers")]
		[SerializeField]
		private RandomCharacterGenerator _randomCharacterGenerator;
		
		[Header("Configs")]
		[SerializeField]
		private HexagonalGridConfig _hexagonalGridConfig;

		private void Awake()
		{
			Initialize();
		}

		private void OnDestroy()
		{
			_cameraController.DeactivateSelection();
		}

		private void Initialize()
		{
			_cameraController.Initialize();
			
			_cameraController.ActivateSelection();
			
			BuildGrid();
		}

		private void BuildGrid()
		{
			LevelData levelData = new LevelData()
			{
				GridData = _hexagonalGridConfig.GridData,
				Characters = GenerateCharacters(1, 0).ToArray(),
			};
			
			_levelBuilder.BuildLevel(levelData);
		}

		private List<CharacterInitializationData> GenerateCharacters(int allyTeamCount, int enemyTeamCount)
		{
			List<CharacterInitializationData> charactersInitializationData = new();
			
			for (int i = 0; i < allyTeamCount; i++)
			{
				CharacterInitializationData initializationData = new()
				{
					CharacterData = _randomCharacterGenerator.GenerateRandomCharacter(TeamType.Ally),
					SpawnPosition = GenerateSpawnPosition()
				};
				charactersInitializationData.Add(initializationData);
			}

			for (int i = 0; i < enemyTeamCount; i++)
			{
				CharacterInitializationData initializationData = new()
				{
					CharacterData = _randomCharacterGenerator.GenerateRandomCharacter(TeamType.Enemy),
					SpawnPosition = GenerateSpawnPosition()
				};
				charactersInitializationData.Add(initializationData);
			}
			
			return charactersInitializationData;
		}

		private Vector2Int GenerateSpawnPosition()
		{
			int rowIndex;
			int columnIndex;
			
			do
			{
				rowIndex = Random.Range(0, _hexagonalGridConfig.GridData.Rows.Count);
				columnIndex = Random.Range(0, _hexagonalGridConfig.GridData.Rows[rowIndex].Elements.Count);
			} while (_hexagonalGridConfig.GridData.Rows[rowIndex].Elements[columnIndex].IsBlocked);
			
			return new Vector2Int(rowIndex, columnIndex);
		}
	}
}