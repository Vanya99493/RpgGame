using System;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Models
{
	[Serializable]
	public class BodyPart
	{
		public GenderType GenderType;
		public BodyCoverType BodyCoverType;
		public Transform Reference;
		
		public string Name => Reference?.name ?? "undefined";

		public BodyPart(
			GenderType genderType, 
			BodyCoverType bodyCoverType, 
			Transform reference)
		{
			GenderType = genderType;
			BodyCoverType = bodyCoverType;
			Reference = reference;
		}
	}
}