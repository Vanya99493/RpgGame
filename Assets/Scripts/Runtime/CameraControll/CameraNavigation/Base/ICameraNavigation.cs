using System;
using UnityEngine;

namespace Runtime.CameraControl
{
	public interface ICameraNavigation
	{
		void MoveTo(
			Transform destinationTransform,
			Action callback = null);
		
		void InstantlyMoveTo(
			Transform destinationTransform,
			Action callback = null);
	}
}