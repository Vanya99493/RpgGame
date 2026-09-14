using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Entry
{
	public class EntryPoint : MonoBehaviour
	{
		[SerializeField]
		private Dictionary<int, string> _dictionary = new();

		[SerializeField]
		private float _x = 5f;
	}
}