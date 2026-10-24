using Configs.HexagonalGrid;
using NaughtyAttributes;
using Runtime.HexagonalGrid;
using UnityEngine;

namespace Runtime.Entry
{
	public class LevelBuildingEntryPoint : MonoBehaviour
	{
		[SerializeField]
		private HexagonalGridController _hexagonalGridController;
		
		[Header("Configs")]
		[SerializeField]
		private HexagonalGridConfig _hexagonalGridConfig;

		[Button]
		public void BuildGrid()
		{
			_hexagonalGridController.Deinitialize();
			_hexagonalGridController.Initialize(_hexagonalGridConfig.GridData);
		}
	}
}