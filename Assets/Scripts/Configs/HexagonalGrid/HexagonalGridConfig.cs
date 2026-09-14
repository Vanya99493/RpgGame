using Runtime.Models;
using UnityEngine;

namespace Configs.HexagonalGrid
{
	[CreateAssetMenu(fileName = "HexagonalGridConfig", menuName = "Scriptable Objects/Grid/Hexagonal Grid Config")]
	public class HexagonalGridConfig : ScriptableObject
	{
		public HexagonalGridData GridData;
	}
}