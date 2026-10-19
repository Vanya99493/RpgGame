using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Interfaces;
using Runtime.Player.States;
using Runtime.Player.States.Base;
using UnityEngine;

namespace Runtime.Player
{
	public class CameraSelectionController : MonoBehaviour, ICameraSelectionController, ICameraSelectionStateMachine
	{
		[SerializeField]
		private string _currentSelectionStateNameDebug;
		
		private Dictionary<InteractionObjectType, BaseSelectionState> _selectionStates;
		private BaseSelectionState _currentSelectionState;
		
		private IInteractable _selectedInteractable;

		public IInteractable SelectedInteractableObject => _selectedInteractable;
		public InteractionObjectType SelectedInteractableObjectType => _selectedInteractable.InteractionObjectType;

		public void Initialize()
		{
			_selectionStates = new()
			{
				{ InteractionObjectType.Undefined, new FreeSelectionState(this) },
				{ InteractionObjectType.Character, new CharacterControlSelectionState(this) },
				{ InteractionObjectType.Grid, new GridSelectionState(this) },
				{ InteractionObjectType.EnvironmentObject, new EnvironmentObjectSelectionState(this) },
			};
			
			SetFreeSelectionState();
		}
		
		public void SelectObject(IInteractable interactable, InputActionType inputActionType)
		{
			_selectedInteractable?.Release();
			
			if (_selectedInteractable != interactable)
			{
				_currentSelectionState.SelectObject(interactable, inputActionType);
			}
			else
			{
				SetFreeSelectionState();
			}
		}

		public void ReleaseSelection()
		{
			SetFreeSelectionState();
		}
		
		public void ChangeSelectionState(IInteractable selectedInteractableObject, InputActionType inputActionType)
		{
			SetSelectionState(selectedInteractableObject.InteractionObjectType, selectedInteractableObject);
		}

		private void SetFreeSelectionState()
		{
			SetSelectionState(InteractionObjectType.Undefined, null);
		}

		private void SetSelectionState(InteractionObjectType interactionObjectType, IInteractable interactable)
		{
			_currentSelectionState?.Exit();
			_currentSelectionState = _selectionStates[interactionObjectType];
			_currentSelectionStateNameDebug = _currentSelectionState.GetType().Name;
			
			_selectedInteractable = interactable;
			
			_currentSelectionState.Enter();
		}
	}
}