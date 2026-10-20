using System.Collections.Generic;
using Runtime.Models;
using UnityEngine;

namespace Runtime.HexagonalGrid
{
	public class HexagonalGridController : MonoBehaviour, IHexagonalGridController
	{
		[SerializeField]
		private HexagonalGridBuilder _hexagonalGridBuilder;
		
		private readonly List<HexagonalGridRowModel> _gridRows = new();
		private readonly HexagonalGridBfs _hexagonalGridBfs = new();

		public void Initialize(HexagonalGridData gridData)
		{
			ClearGrid();
			
			BuildGrid(gridData);
		}

		public void Deinitialize()
		{
			ClearGrid();
		}

		public Vector3 GetGridElementPosition(Vector2Int position) => GetGridElement(position).CenterPosition;

		public HexagonalGridElement GetGridElement(Vector2Int position) => _gridRows[position.x].Elements[position.y];

		public List<HexagonalGridElement> GetClosestGridElements(Vector2Int gridPosition, int depth)
		{
			return _hexagonalGridBfs.GetClosestGridElements(GetGridElement(gridPosition), depth);
		}

		public List<HexagonalGridElement> GetNeighbors(Vector2Int gridPosition)
		{
			return GetNeighbors(GetGridElement(gridPosition));
		}

		public List<HexagonalGridElement> GetNeighbors(HexagonalGridElement element)
		{
			List<HexagonalGridElement> neighbors = new();
			Vector2Int position = element.GridPosition;

			// Top
			if (position.x > 0)
			{
				int paddingDifference = _gridRows[position.x].RowPadding - _gridRows[position.x - 1].RowPadding;
				int leftIndex = GetAdjacentRowElementIndex(position.y, paddingDifference);

				AddNeighborIfValid(neighbors, position.x - 1, leftIndex);
				AddNeighborIfValid(neighbors, position.x - 1, leftIndex + 1);
			}

			// Left
			if (position.y > 0)
			{
				neighbors.Add(GetGridElement(new Vector2Int(position.x, position.y - 1)));
			}

			// Right
			if (position.y < _gridRows[position.x].Elements.Count - 1)
			{
				neighbors.Add(GetGridElement(new Vector2Int(position.x, position.y + 1)));
			}

			// Bottom
			if (position.x < _gridRows.Count - 1)
			{
				int paddingDifference = _gridRows[position.x].RowPadding - _gridRows[position.x + 1].RowPadding;
				int leftIndex = GetAdjacentRowElementIndex(position.y, paddingDifference);

				AddNeighborIfValid(neighbors, position.x + 1, leftIndex);
				AddNeighborIfValid(neighbors, position.x + 1, leftIndex + 1);
			}

			return neighbors;
		}

		private static int GetAdjacentRowElementIndex(int currentIndex, int paddingDifference) => paddingDifference + currentIndex + Mathf.Abs(Mathf.RoundToInt(paddingDifference / 2f));

		private void AddNeighborIfValid(
			List<HexagonalGridElement> neighbors,
			int rowIndex,
			int elementIndex)
		{
			if (elementIndex >= 0 
			    && elementIndex < _gridRows[rowIndex].Elements.Count)
			{
				neighbors.Add(GetGridElement(new Vector2Int(rowIndex, elementIndex)));
			}
		}

		private void BuildGrid(HexagonalGridData gridData)
		{
			_gridRows.AddRange(_hexagonalGridBuilder.BuildGrid(gridData, this));
		}
		
		private void ClearGrid()
		{
			foreach (var gridRow in _gridRows)
			{
				gridRow.Clear();
			}
			
			_gridRows.Clear();
		}
	}
}