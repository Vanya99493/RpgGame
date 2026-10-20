using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Models
{
	[Serializable]
	public struct HexagonalGridData
	{
		public Vector3 GridCenterPosition;
		public List<HexagonalGridRowData> Rows;
	}
}