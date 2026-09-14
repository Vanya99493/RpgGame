using Runtime.Interfaces;
using UnityEngine;

namespace Patterns
{
	public class MonoBehaviourFactory<T> : MonoBehaviour where T : MonoBehaviour, IDestroyable
	{
		[SerializeField]
		private T _prefab;
		
		[SerializeField]
		private Transform _parent;
		
		public T Create()
		{
			T newObject = Instantiate(_prefab, _parent);
			newObject.DestroyEvent += OnDestroyEventHandler;
			return newObject;
		}
		
		public T Create(Vector3 position, Quaternion rotation)
		{
			T newObject = Instantiate(_prefab, position, rotation);
			newObject.transform.parent = _parent;
			newObject.DestroyEvent += OnDestroyEventHandler;
			return newObject;
		}

		private void OnDestroyEventHandler(IDestroyable destroyable)
		{
			Destroy((destroyable as MonoBehaviour).gameObject);
		}
	}
}