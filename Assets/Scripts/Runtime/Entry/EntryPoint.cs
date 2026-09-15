using Configs.HexagonalGrid;
using Runtime.HexagonalGrid;
using Runtime.Player;
using UnityEngine;

namespace Runtime.Entry
{
	public class EntryPoint : MonoBehaviour
	{
		[SerializeField]
		private HexagonalGridController _hexagonalGridController;
		
		[SerializeField]
		private PlayerCameraController _playerCameraController;
		
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

		public void BuildGrid()
		{
			_hexagonalGridController.Initialize(_hexagonalGridConfig.GridData);
		}
	}
}