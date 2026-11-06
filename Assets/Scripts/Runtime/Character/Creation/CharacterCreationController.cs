using System.Collections.Generic;
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

		private List<string> _currentBodyPartsList = new();
		private bool _isAvailableWithoutAnything;
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
			_characterView.Initialize(
				genderType,
				_defaultCharacterPartsConfig.BodyPartTypes);
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
			_currentBodyPart = bodyPartType;
			(_currentBodyPartsList, _isAvailableWithoutAnything) = _characterView.GetBodyPartNamesList(_currentBodyPart);
			_currentIndex = _characterView.GetActiveBodyPartIndex(_currentBodyPart);
			
			UpdateUI();
		}

		private void OnPreviousElementSelectedEventHandler()
		{
			_currentIndex = Mathf.Max(
				_currentIndex - 1, 
				_isAvailableWithoutAnything 
					? -1
					: 0);

			if (_currentIndex < 0)
			{
				_characterView.DisableBodyPart(_currentBodyPart);
			}
			else
			{
				_characterView.SetBodyPart(new BodyPartData()
				{
					BodyPartType = _currentBodyPart,
					BodyPartName = _currentBodyPartsList[_currentIndex]
				});
			}
			
			UpdateUI();
		}
		
		private void OnNextElementSelectedEventHandler()
		{
			_currentIndex = Mathf.Min(
				_currentIndex + 1, 
				_currentBodyPartsList.Count - 1);
			
			if (_currentIndex >= 0)
			{
				_characterView.SetBodyPart(new BodyPartData()
				{
					BodyPartType = _currentBodyPart,
					BodyPartName = _currentBodyPartsList[_currentIndex]
				});
			}

			UpdateUI();
		}

		private void UpdateUI()
		{
			_characterCreationPanel.SetIndexText(_currentIndex);
			_characterCreationPanel.SetTypeText(_currentBodyPart.ToString());
		}
	}
}