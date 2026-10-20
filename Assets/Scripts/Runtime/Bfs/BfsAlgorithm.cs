using System.Collections.Generic;
using System.Linq;

namespace Runtime.Bfs
{
	public class BfsAlgorithm<T> where T : class, IBfsElement
	{
		// TODO: fix logic
		public List<T> GetClosestGridElements(T startPoint, int depth)
		{
			var queue = new Queue<BfsElement<T>>();
			var visited = new HashSet<T>();

			BfsElement<T> startGridElement = new BfsElement<T>(
				startPoint, 
				null, 
				0);
			
			queue.Enqueue(startGridElement);
			visited.Add(startPoint);

			while (queue.Count > 0)
			{
				var current = queue.Dequeue();
				var neighbors = current.Reference.GetNeighbors<T>();

				foreach (var neighbor in neighbors)
				{
					if (current.Depth + 1 < depth
						&& visited.Add(neighbor))
					{
						BfsElement<T> neighborElement = new(
							neighbor,
							current.Reference,
							current.Depth + 1);
						
						queue.Enqueue(neighborElement);
					}
				}
			}

			return visited.ToList();
		}
		
		public List<T> BuildRoute(BfsElement<T> destinationElement)
		{
			List<T> route = new();

			while (destinationElement.ComeFromReference != null)
			{
				route.Add(destinationElement.Reference);
			}
			
			route.Reverse();
			return route;
		}
	}
}