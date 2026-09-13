using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Runtime.Entry
{
	public class EntryPoint : MonoBehaviour
	{
		[SerializeField]
		private Dictionary<int, string> _dictionary = new();
		
		[SerializeField]
		private SerializableDictionary<int, string> _serializableDictionary = new();

		[SerializeField]
		private float _x = 5f;
	}
}