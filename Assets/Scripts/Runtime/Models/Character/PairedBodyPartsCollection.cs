using System;
using System.Collections.Generic;
using Runtime.Enums;

namespace Runtime.Models
{
	[Serializable]
	public class PairedBodyPartsCollection
	{
		public BodyPartType BodyPartType;
		public List<PairedBodyPart> BodyParts = new();
	}
}