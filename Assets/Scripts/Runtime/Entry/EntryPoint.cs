using Configs.HexagonalGrid;
using Runtime.Level;
using Runtime.Models.Level;
using Runtime.Player;
using UnityEngine;

namespace Runtime.Entry
{
	public class EntryPoint : MonoBehaviour
	{
		[SerializeField]
		private PlayerCameraController _playerCameraController;
		
		[SerializeField]
		private LevelBuilder _levelBuilder;
		
		[Header("Configs")]
		[SerializeField]
		private HexagonalGridConfig _hexagonalGridConfig;

		private void Awake()
		{
			_playerCameraController.ActivateSelection();
			
			BuildGrid();
		}

		private void OnDestroy()
		{
			_playerCameraController.DeactivateSelection();
		}

		private void BuildGrid()
		{
			LevelData levelData = new LevelData()
			{
				GridData = _hexagonalGridConfig.GridData,
			};
			
			_levelBuilder.BuildLevel(levelData);
		}
	}
}