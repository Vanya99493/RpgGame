using System.Collections.Generic;
using Configs.HexagonalGrid;
using Runtime.Models;
using UnityEngine;

namespace Runtime.HexagonalGrid
{
	public class HexagonalGridController : MonoBehaviour
	{
		[SerializeField]
		private HexagonalGridElementsObjectPool _hexagonalGridElementsObjectPool;
		
		[Header("Configs")]
		[SerializeField]
		private HexagonalGridBuildingDataConfig _hexagonalGridBuildingDataConfig;
		
		private readonly List<HexagonalGridRowModel> _gridRows = new();

		public void Initialize(HexagonalGridData gridData)
		{
			ClearGrid();
			
			BuildGrid(gridData);
		}

		public void Deinitialize()
		{
			ClearGrid();
		}

		public Vector3 GetGridElementPosition(Vector2 position) => GetGridElement(position).CenterPosition;

		// TODO: create route building with shortest path search
		public List<Vector3> BuildRoute(Vector2 from, Vector2 to)
		{
			return new();
		}

		private void BuildGrid(HexagonalGridData gridData)
		{
			Vector3 gridOffset = CalculateGridOffset(gridData);
			
			for (int i = 0; i < gridData.Rows.Count; i++)
			{
				var gridRowModel = BuildGridRow(
					gridData.Rows[i],
					gridOffset + new Vector3(
						0f, 
						0f, 
						-_hexagonalGridBuildingDataConfig.HexagonalGridBuildingData.VerticalBuildingStep * i));
				_gridRows.Add(gridRowModel);
			}
		}

		private Vector3 CalculateGridOffset(HexagonalGridData gridData)
		{
			float highest = 0f;
			float lowest = -_hexagonalGridBuildingDataConfig.HexagonalGridBuildingData.VerticalBuildingStep * (gridData.Rows.Count - 1);
			
			float furthestLeft = float.MaxValue;
			float furthestRight = float.MinValue;
			
			foreach (var row in gridData.Rows)
			{
				float rowFurthestLeft = _hexagonalGridBuildingDataConfig.HexagonalGridBuildingData.HorizontalBuildingStep / 2 * row.RowPadding;
				float rowFurthestRight = rowFurthestLeft + _hexagonalGridBuildingDataConfig.HexagonalGridBuildingData.HorizontalBuildingStep * (row.Elements.Count - 1);

				furthestLeft = Mathf.Min(
					furthestLeft, 
					rowFurthestLeft);
				
				furthestRight = Mathf.Max(
					furthestRight, 
					rowFurthestRight);
			}

			return gridData.GridCenterPosition + new Vector3(
				(furthestRight - furthestLeft) / 2 * -1,
				0f,
				Mathf.Abs(lowest / 2));
		}

		private HexagonalGridRowModel BuildGridRow(
			HexagonalGridRowData gridRowData,
			Vector3 startRowPosition)
		{
			HexagonalGridRowModel gridRowModel = new HexagonalGridRowModel()
			{
				RowPadding = gridRowData.RowPadding,
			};

			for (int i = 0; i < gridRowData.Elements.Count; i++)
			{
				var gridElement = _hexagonalGridElementsObjectPool.GetObjectFromPool();
				Vector3 gridElementPosition = startRowPosition + new Vector3(
					CalculateGridElementHorizontalStep(
						gridRowModel.RowPadding, 
						i),
					0f,
					0f);
				gridElement.transform.position = gridElementPosition;
				gridElement.Initialize(gridRowData.Elements[i]);
				gridRowModel.Elements.Add(gridElement);
			}
			
			return gridRowModel;
		}

		private float CalculateGridElementHorizontalStep(
			int rowPadding,
			int elementIndex)
		{
			float padding = rowPadding * 0.5f * _hexagonalGridBuildingDataConfig.HexagonalGridBuildingData.HorizontalBuildingStep;
			float step = _hexagonalGridBuildingDataConfig.HexagonalGridBuildingData.HorizontalBuildingStep * elementIndex;

			return padding + step;
		}

		private HexagonalGridElement GetGridElement(Vector2 position) => _gridRows[(int)position.x].Elements[(int)position.y];
		
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