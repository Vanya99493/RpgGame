using System;
using Patterns;
using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.HexagonalGrid
{
	public class HexagonalGridElement : MonoBehaviour, IDestroyable, IPoolable
	{
		[SerializeField]
		private GameObject _model;
		
		public event Action<IDestroyable> DestroyEvent;
		public event Action<IPoolable> ReturnToPoolEvent;

		public void Initialize(bool isAvailable)
		{
			if (!isAvailable)
			{
				var renderer = _model.GetComponent<Renderer>();

				var propertyBlock = new MaterialPropertyBlock();
				renderer.GetPropertyBlock(propertyBlock);

				propertyBlock.SetColor(
					"_BaseColor",
					new Color(1f, 1f, 1f, 0.2f)
				);

				renderer.SetPropertyBlock(propertyBlock);
			}
		}
		
		public void Destroy()
		{
			DestroyEvent?.Invoke(this);
		}

		public void ReturnToPool()
		{
			ReturnToPoolEvent?.Invoke(this);
		}
	}
}