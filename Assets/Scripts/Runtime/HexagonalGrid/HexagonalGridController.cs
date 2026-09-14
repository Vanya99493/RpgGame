using System.Collections.Generic;
using Configs.HexagonalGrid;
using Runtime.Models;
using UnityEngine;

namespace Runtime.HexagonalGrid
{
	public class HexagonalGridController : MonoBehaviour
	{
		[SerializeField]
		private HexagonalGridElementObjectPool _hexagonalGridElementObjectPool;
		
		[Header("Configs")]
		[SerializeField]
		private HexagonalGridBuildingDataConfig _hexagonalGridBuildingDataConfig;
		
		private List<HexagonalGridRowModel> _gridRows = new();

		public void Initialize(HexagonalGridData gridData)
		{
			ClearGrid();
			
			BuildGrid(gridData);
		}

		public void Deinitialize()
		{
			ClearGrid();
		}

		private void BuildGrid(HexagonalGridData gridData)
		{
			for (int i = 0; i < gridData.Rows.Count; i++)
			{
				var gridRowModel = BuildGridRow(
					gridData.Rows[i],
					gridData.GridStartPosition + new Vector3(
						0f, 
						0f, 
						-_hexagonalGridBuildingDataConfig.HexagonalGridBuildingData.VerticalBuildingStep * i));
				_gridRows.Add(gridRowModel);
			}
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
				var gridElement = _hexagonalGridElementObjectPool.GetObjectFromPool();
				Vector3 gridElementPosition = startRowPosition + new Vector3(
					CalculateGridElementHorizontalStep(
						gridRowModel.RowPadding, 
						i),
					0f,
					0f);
				gridElement.transform.position = gridElementPosition;
				gridElement.Initialize(gridRowData.Elements[i].IsAwailableToMovement);
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