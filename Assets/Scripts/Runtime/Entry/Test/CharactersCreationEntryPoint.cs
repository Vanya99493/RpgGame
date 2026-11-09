using Runtime.Character;
using Runtime.UI.Panels;
using UnityEngine;

namespace Runtime.Entry
{
	public class CharactersCreationEntryPoint : MonoBehaviour
	{
		[SerializeField]
		private CharacterCustomizationController _characterCustomizationController;
		
		[Header("UI")]
		[SerializeField]
		private CharacterCreationPanel _characterCreationPanel;

		private void Awake()
		{
			Initialize();
		}

		private void Initialize()
		{
			_characterCustomizationController.Initialize(_characterCreationPanel);
		}
	}
}