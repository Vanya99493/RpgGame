using System;
using Runtime.Enums;

namespace Runtime.Models
{
	[Serializable]
	public struct BodyPartData
	{
		public BodyPartType BodyPartType;
		public string BodyPartName;
	}
}