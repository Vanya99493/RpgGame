using System;
using UnityEngine;

namespace Runtime.Models
{
	[Serializable]
	public class BodyPartReference
	{
		public Transform Reference;
		
		public string Name => Reference?.name ?? "undefined";
	}
}