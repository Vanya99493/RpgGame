using Configs.Character;
using Runtime.Enums;
using Runtime.Models;
using Runtime.UI.Panels;
using Runtime.Views.Character;
using UnityEngine;

namespace Runtime.Character
{
	public class CharacterCreationController : MonoBehaviour
	{
		[SerializeField]
		private CharacterView _characterView;
		
		[Header("Configs")]
		[SerializeField]
		private DefaultCharacterPartsConfig _defaultCharacterPartsConfig;

		private ICharacterCreationPanel _characterCreationPanel;
		private BodyPartType _currentBodyPart;
		private int _currentIndex;

		private void OnDestroy()
		{
			UnsubscribeFromEvents();
		}

		public void Initialize(ICharacterCreationPanel characterCreationPanel)
		{
			InitDefaultCharacterView(GenderType.Male);
			
			_characterCreationPanel = characterCreationPanel;
			
			SubscribeOnEvents();
			
			OnBodyPartChanged(BodyPartType.HeadCovering);
		}

		private void InitDefaultCharacterView(GenderType genderType)
		{
			_characterView.Initialize(genderType);
			
			foreach (var bodyPartData in _defaultCharacterPartsConfig.BodyPartTypes)
			{
				_characterView.SetBodyPart(
					new BodyPartData()
					{
						BodyPartType = bodyPartData,
						BodyPartName = "",
					});
			}
		}

		private void SubscribeOnEvents()
		{
			if (_characterCreationPanel != null)
			{
				_characterCreationPanel.ChangeBodyPartEvent += OnBodyPartChanged;
				_characterCreationPanel.ToPreviousElementEvent += OnPreviousElementSelectedEventHandler;
				_characterCreationPanel.ToNextElementEvent += OnNextElementSelectedEventHandler;
			}
		}
		
		private void UnsubscribeFromEvents()
		{
			if (_characterCreationPanel != null)
			{
				_characterCreationPanel.ChangeBodyPartEvent -= OnBodyPartChanged;
				_characterCreationPanel.ToPreviousElementEvent -= OnPreviousElementSelectedEventHandler;
				_characterCreationPanel.ToNextElementEvent -= OnNextElementSelectedEventHandler;
			}
		}

		private void OnBodyPartChanged(BodyPartType bodyPartType)
		{
			
		}

		private void OnPreviousElementSelectedEventHandler()
		{
			
		}
		
		private void OnNextElementSelectedEventHandler()
		{
			
		}
	}
}