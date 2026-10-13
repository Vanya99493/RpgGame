using System.Collections.Generic;
using UnityEngine;

namespace Configs.Character
{
	[CreateAssetMenu(fileName = "RandomCharacterConfig", menuName = "Scriptable Objects/Character/Random Character Config")]
	public class RandomCharacterConfig : ScriptableObject
	{
		[SerializeField]
		private string _defaultName = "DefaultName";
		
		[SerializeField]
		private List<string> _characterNames = new();
		
		[Header("Skills")]
		[SerializeField]
		private Vector2 _healthRange;

		[SerializeField]
		private Vector2 _energyRange;

		public string GetRandomName()
		{
			return _characterNames.Count != 0 
				? _characterNames[Random.Range(0, _characterNames.Count)]
				: _defaultName;
		}

		public int GetRandomHealthValue()
		{
			return (int)Random.Range(_healthRange.x, _healthRange.y);
		}

		public int GetRandomEnergyValue()
		{
			return (int)Random.Range(_energyRange.x, _energyRange.y);
		}
	}
}