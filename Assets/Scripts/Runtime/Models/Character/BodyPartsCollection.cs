using System;
using System.Collections.Generic;
using Runtime.Enums;

namespace Runtime.Models
{
	[Serializable]
	public class BodyPartsCollection
	{
		public BodyPartType BodyPartType;
		public List<BodyPart> BodyParts = new();
	}
}