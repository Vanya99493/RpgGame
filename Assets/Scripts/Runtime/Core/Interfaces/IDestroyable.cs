using System;
using UnityEngine;

namespace Runtime.Interfaces
{
	public interface IDestroyable
	{
		public event Action<IDestroyable> DestroyEvent;

		public void Destroy();
	}
}