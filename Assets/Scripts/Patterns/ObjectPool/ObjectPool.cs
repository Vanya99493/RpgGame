using System.Collections.Generic;
using System.Linq;
using Runtime.Interfaces;
using UnityEngine;

namespace Patterns
{
	public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour, IPoolable, IDestroyable
	{
		[SerializeField]
		private Factory<T> _factory;

		[Space(20)]
		[SerializeField]
		protected List<T> _freeObjects = new();

		protected List<T> _busyObjects = new();
		
		public virtual T GetObjectFromPool()
		{
			T result;

			if (_freeObjects.Count > 0)
			{
				result = _freeObjects.First();

				_freeObjects.Remove(result);
				_busyObjects.Add(result);
			}
			else
			{
				result = _factory.Create();
				
				_busyObjects.Add(result);
			}

			result.ReturnToPoolEvent += OnReturnObjectToPool;

			return result;
		}

		protected void OnReturnObjectToPool(IPoolable obj)
		{
			obj.ReturnToPoolEvent -= OnReturnObjectToPool;

			_freeObjects.Add((T)obj);
			_busyObjects.Remove((T)obj);
		}
	}
}