using System;
using Runtime.Enums;

namespace Runtime.Models
{
	[Serializable]
	public struct HexagonalGridElementData
	{
		public bool IsBlocked;
		public SurfaceType SurfaceType;
	}
}