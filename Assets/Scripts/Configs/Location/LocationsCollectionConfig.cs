using System.Collections.Generic;
using Runtime.Models;
using UnityEngine;

namespace Configs.Location
{
	[CreateAssetMenu(fileName = "LocationsCollectionConfig", menuName = "Scriptable Objects/Locations/Locations Collection")]
	public class LocationsCollectionConfig : ScriptableObject
	{
		public List<LocationsCollectionData> LocationsCollectionData = new();
	}
}