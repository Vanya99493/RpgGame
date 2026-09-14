using Configs.HexagonalGrid;
using Runtime.HexagonalGrid;
using UnityEngine;

namespace Runtime.Entry
{
	public class EntryPoint : MonoBehaviour
	{
		[SerializeField]
		private HexagonalGridController _hexagonalGridController;
		
		[SerializeField]
		private HexagonalGridConfig _hexagonalGridConfig;

		private void Awake()
		{
			BuildGrid();
		}

		public void BuildGrid()
		{
			_hexagonalGridController.Initialize(_hexagonalGridConfig.GridData);
		}
	}
}