using System.Collections.Generic;

namespace Runtime.Bfs
{
	public interface IBfsElement
	{
		List<T> GetNeighbors<T>();
	}
}