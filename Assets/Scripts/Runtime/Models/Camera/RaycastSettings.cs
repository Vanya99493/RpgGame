using System;
using UnityEngine;

namespace Runtime.Models
{
	[Serializable]
	public struct RaycastSettings
	{
		public LayerMask SelectionLayerMask;
		public float RaycastDistance;
	}
}