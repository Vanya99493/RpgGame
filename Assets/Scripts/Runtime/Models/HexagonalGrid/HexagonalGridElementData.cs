using System;
using Runtime.Enums;

namespace Runtime.Models
{
	[Serializable]
	public struct HexagonalGridElementData
	{
		public bool IsAwailableToMovement;
		public bool IsBlockedByObject;
		public SurfaceType SurfaceType;
	}
}