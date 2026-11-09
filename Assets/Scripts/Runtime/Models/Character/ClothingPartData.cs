using System;
using System.Collections.Generic;
using Runtime.Enums;

namespace Runtime.Models
{
	[Serializable]
	public struct ClothingPartData
	{
		public GenderType GenderType;
		public List<BodyPartData> BodyPartsData;
	}
}