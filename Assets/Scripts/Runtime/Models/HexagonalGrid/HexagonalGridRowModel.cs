using System.Collections.Generic;
using Runtime.HexagonalGrid;

namespace Runtime.Models
{
	public class HexagonalGridRowModel
	{
		public int RowPadding;
		public List<HexagonalGridElement> Elements = new();

		public void Clear()
		{
			foreach (var element in Elements)
			{
				element.Destroy();
			}
			
			Elements.Clear();
		}
	}
}