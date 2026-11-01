using System;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Models
{
	[Serializable]
	public class PairedBodyPart
	{
		public GenderType GenderType;
		public BodyCoverType BodyCoverType;
		public Transform Reference;
		public Transform PairedReference;
		
		public string Name => Reference?.name ?? "undefined";
		public string PairedName => PairedReference?.name ?? "undefined";

		public PairedBodyPart(
			GenderType genderType, 
			BodyCoverType bodyCoverType, 
			Transform reference, 
			Transform pairedReference)
		{
			GenderType = genderType;
			BodyCoverType = bodyCoverType;
			Reference = reference;
			PairedReference = pairedReference;
		}
	}
}