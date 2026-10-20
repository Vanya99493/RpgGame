using System.Collections.Generic;

namespace Runtime.HexagonalGrid
{
	public interface IHexagonalGridController
	{
		public List<HexagonalGridElement> GetNeighbors(HexagonalGridElement element);
	}
}