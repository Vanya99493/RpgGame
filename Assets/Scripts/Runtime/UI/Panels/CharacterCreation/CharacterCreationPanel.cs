using System;
using System.Collections.Generic;
using Runtime.Enums;
using Runtime.UI.Elements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI.Panels
{
	public class CharacterCreationPanel : MonoBehaviour, ICharacterCreationPanel
	{
		[SerializeField]
		private ButtonWithTitle _buttonWithTitlePrefab;
		
		[SerializeField]
		private Transform _groupButtonsContainer;

		[Header("Gender")]
		[SerializeField]
		private Button _maleGenderButton;

		[SerializeField]
		private Button _femaleGenderButton;
		
		[Header("Navigation")]
		[SerializeField]
		private Button _leftButton;
		
		[SerializeField]
		private Button _rightButton;

		[SerializeField]
		private TMP_Text _currentIndexText;
		
		[SerializeField]
		private TMP_Text _currentTypeText;

		private List<ButtonWithTitle> _groupButtons = new();
		
		public event Action<GenderType> ChangeGenderEvent;
		public event Action<BodyPartType> ChangeBodyPartEvent;
		public event Action ToPreviousElementEvent;
		public event Action ToNextElementEvent;
		
		private void Awake()
		{
			FillGroupButtonsContainer();
		}

		private void OnDestroy()
		{
			ClearGroupButtonsContainer();
		}

		private void OnEnable()
		{
			_maleGenderButton.onClick.AddListener(OnMaleGenderButtonClicked);
			_femaleGenderButton.onClick.AddListener(OnFemaleGenderButtonClicked);
			
			_leftButton.onClick.AddListener(OnLeftButtonClicked);
			_rightButton.onClick.AddListener(OnRightButtonClicked);
		}

		private void OnDisable()
		{
			_maleGenderButton.onClick.RemoveListener(OnMaleGenderButtonClicked);
			_femaleGenderButton.onClick.RemoveListener(OnFemaleGenderButtonClicked);
			
			_leftButton.onClick.RemoveListener(OnLeftButtonClicked);
			_rightButton.onClick.RemoveListener(OnRightButtonClicked);
		}

		public void SetIndexText(int index)
		{
			_currentIndexText.text = index.ToString();
		}

		public void SetTypeText(string type)
		{
			_currentTypeText.text = type;
		}

		private void FillGroupButtonsContainer()
		{
			foreach (BodyPartType bodyPart in Enum.GetValues(typeof(BodyPartType)))
			{
				var buttonWithTitle = Instantiate(_buttonWithTitlePrefab, _groupButtonsContainer);
				buttonWithTitle.Title.text = bodyPart.ToString();
				buttonWithTitle.Button.onClick.AddListener(() => ChangeBodyPartEvent?.Invoke(bodyPart));
				_groupButtons.Add(buttonWithTitle);
			}
		}

		private void ClearGroupButtonsContainer()
		{
			foreach (var buttonWithTitle in _groupButtons)
			{
				Destroy(buttonWithTitle.gameObject);
			}
			
			_groupButtons.Clear();
		}

		private void OnMaleGenderButtonClicked()
		{
			ChangeGenderEvent?.Invoke(GenderType.Male);
		}

		private void OnFemaleGenderButtonClicked()
		{
			ChangeGenderEvent?.Invoke(GenderType.Female);
		}

		private void OnLeftButtonClicked()
		{
			ToPreviousElementEvent?.Invoke();
		}

		private void OnRightButtonClicked()
		{
			ToNextElementEvent?.Invoke();
		}
	}
}