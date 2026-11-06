using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Enums;

namespace Runtime.Models
{
	[Serializable]
	public class BodyPart
	{
		public GenderType GenderType;
		public BodyCoverType BodyCoverType;
		
		public List<BodyPartReference> References = new();
		
		public string Name => References.First()?.Name ?? "undefined";
		
		public BodyPart(
			GenderType genderType, 
			BodyCoverType bodyCoverType, 
			List<BodyPartReference> references)
		{
			GenderType = genderType;
			BodyCoverType = bodyCoverType;
			References = references;
		}

		public void SetActive(bool isActive)
		{
			foreach (var reference in References)
			{
				reference.Reference.gameObject.SetActive(isActive);
			}
		}
	}
}