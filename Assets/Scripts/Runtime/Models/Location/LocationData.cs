using System;
using Configs.HexagonalGrid;
using UnityEngine;

namespace Runtime.Models
{
	[Serializable]
	public struct LocationData
	{
		public GameObject LocationPrefab;
		public HexagonalGridConfig HexagonalGridConfig;
	}
}