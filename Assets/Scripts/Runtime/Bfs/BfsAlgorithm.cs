using System.Collections.Generic;
using System.Linq;
using Extensions;

namespace Runtime.Bfs
{
	public class BfsAlgorithm<T> where T : class, IBfsElement
	{
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
				neighbors.Shuffle();

				foreach (var neighbor in neighbors)
				{
					if (current.Depth + 1 <= depth
						&& visited.Add(neighbor))
					{
						BfsElement<T> neighborElement = new(
							neighbor,
							current,
							current.Depth + 1);
						
						queue.Enqueue(neighborElement);
					}
				}
			}

			return visited.ToList();
		}
		
		public List<T> FindPath(T startPoint, T destinationPoint)
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
				neighbors.Shuffle();

				foreach (var neighbor in neighbors)
				{
					if (neighbor == destinationPoint)
					{
						BfsElement<T> destinationElement = new(
							neighbor,
							current,
							current.Depth + 1);

						return BuildRoute(destinationElement);
					}
					
					if (visited.Add(neighbor))
					{
						BfsElement<T> neighborElement = new(
							neighbor,
							current,
							current.Depth + 1);
						
						queue.Enqueue(neighborElement);
					}
				}
			}

			return new();
		}
		
		private List<T> BuildRoute(BfsElement<T> destinationElement)
		{
			List<T> route = new();
			BfsElement<T> pickedElement = destinationElement;

			while (pickedElement.ComeFromElement != null)
			{
				route.Add(pickedElement.Reference);
				pickedElement = pickedElement.ComeFromElement;
			}
			
			route.Reverse();
			return route;
		}
	}
}