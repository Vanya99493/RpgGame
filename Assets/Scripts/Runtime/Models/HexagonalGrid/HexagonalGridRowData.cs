using System;
using System.Collections.Generic;

namespace Runtime.Models
{
	[Serializable]
	public struct HexagonalGridRowData
	{
		public int RowPadding;
		public List<HexagonalGridElementData> Elements;
	}
}