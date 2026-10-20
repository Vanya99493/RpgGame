using System.Collections.Generic;
using Configs.HexagonalGrid;
using Runtime.Models;
using UnityEngine;

namespace Runtime.HexagonalGrid
{
	public class HexagonalGridBuilder : MonoBehaviour
	{
		[SerializeField]
		private HexagonalGridElementsObjectPool _hexagonalGridElementsObjectPool;
		
		[Header("Configs")]
		[SerializeField]
		private HexagonalGridBuildingDataConfig _hexagonalGridBuildingDataConfig;
		
		public List<HexagonalGridRowModel> BuildGrid(
			HexagonalGridData gridData,
			IHexagonalGridController hexagonalGridController)
		{
			List<HexagonalGridRowModel> gridRows = new();
			Vector3 gridOffset = CalculateGridOffset(gridData);
			
			for (int i = 0; i < gridData.Rows.Count; i++)
			{
				var gridRowModel = BuildGridRow(
					hexagonalGridController,
					gridData.Rows[i],
					i,
					gridOffset + new Vector3(
						0f, 
						0f, 
						-_hexagonalGridBuildingDataConfig.HexagonalGridBuildingData.VerticalBuildingStep * i));
				gridRows.Add(gridRowModel);
			}

			return gridRows;
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
			IHexagonalGridController hexagonalGridController,
			HexagonalGridRowData gridRowData,
			int rowIndex,
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
				gridElement.Initialize(
					hexagonalGridController, 
					gridRowData.Elements[i],
					new Vector2Int(rowIndex, i));
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
	}
}