using Runtime.Bfs;

namespace Runtime.HexagonalGrid
{
	public class HexagonalGridBfsElement : BfsElement<HexagonalGridElement>
	{
		public HexagonalGridBfsElement(HexagonalGridElement reference, BfsElement<HexagonalGridElement> comeFromReference, int depth) : base(reference, comeFromReference, depth) { }
	}
}