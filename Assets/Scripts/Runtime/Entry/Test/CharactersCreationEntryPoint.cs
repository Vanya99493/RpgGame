using Runtime.Character;
using Runtime.UI.Panels;
using UnityEngine;

namespace Runtime.Entry
{
	public class CharactersCreationEntryPoint : MonoBehaviour
	{
		[SerializeField]
		private CharacterCreationController _characterCreationController;
		
		[Header("UI")]
		[SerializeField]
		private CharacterCreationPanel _characterCreationPanel;

		private void Awake()
		{
			Initialize();
		}

		private void Initialize()
		{
			_characterCreationController.Initialize(_characterCreationPanel);
		}
	}
}