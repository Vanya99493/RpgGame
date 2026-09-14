using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Models
{
	[Serializable]
	public struct HexagonalGridData
	{
		public Vector3 GridStartPosition;
		public List<HexagonalGridRowData> Rows;
	}
}