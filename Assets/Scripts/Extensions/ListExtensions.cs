using UnityEngine;
using System.Collections.Generic;

namespace Extensions
{
	public static class ListExtensions
	{
		public static Queue<T> ToQueue<T>(this List<T> list)
		{
			Queue<T> queue = new();

			foreach (var listElement in list)
			{
				queue.Enqueue(listElement);
			}

			return queue;
		}
		
		public static void Shuffle<T>(this List<T> list)
		{
			for (int i = list.Count - 1; i > 0; i--)
			{
				int randomIndex = Random.Range(0, i + 1);

				(list[i], list[randomIndex]) = (list[randomIndex], list[i]);
			}
		}
	}
}