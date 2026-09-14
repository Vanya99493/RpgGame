using Runtime.Models;
using UnityEngine;

namespace Configs.HexagonalGrid
{
	[CreateAssetMenu(fileName = "HexagonalGridBuildingDataConfig", menuName = "Scriptable Objects/Grid/Hexagonal Grid Building Data Config")]
	public class HexagonalGridBuildingDataConfig : ScriptableObject
	{
		public HexagonalGridBuildingData HexagonalGridBuildingData;
	}
}