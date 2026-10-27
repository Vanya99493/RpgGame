using System;
using System.Collections.Generic;
using Runtime.Enums;

namespace Runtime.Models
{
	[Serializable]
	public struct LocationsCollectionData
	{
		public LocationType LocationType;
		public List<LocationData> Locations;
	}
}