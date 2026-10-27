using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.CameraControl
{
	public abstract class CameraSelectionController : MonoBehaviour, ICameraSelectionController, ICameraSelectionStateMachine
	{
		protected Dictionary<InteractionObjectType, BaseSelectionState> _selectionStates;
		protected BaseSelectionState _currentSelectionState;
		
		protected IInteractable _selectedInteractable;

		public IInteractable SelectedInteractableObject => _selectedInteractable;
		public InteractionObjectType SelectedInteractableObjectType => _selectedInteractable.InteractionObjectType;
		
		public abstract void Initialize(Camera mainCamera);
		
		public virtual void SelectObject(
			IInteractable interactable, 
			InputActionType inputActionType, 
			InputActionPhase inputActionPhase)
		{
			_selectedInteractable?.Release();
			
			if (_selectedInteractable != interactable)
			{
				_currentSelectionState.SelectObject(
					interactable, 
					inputActionType, 
					inputActionPhase);
			}
			else
			{
				ReleaseSelection();
			}
		}
		
		public virtual void ChangeSelectionState(IInteractable selectedInteractableObject, InputActionType inputActionType)
		{
			SetSelectionState(selectedInteractableObject.InteractionObjectType, selectedInteractableObject);
		}
		
		public virtual void ReleaseSelection()
		{
			SetFreeSelectionState();
		}
		
		protected virtual void SetSelectionState(InteractionObjectType interactionObjectType, IInteractable interactable)
		{
			_currentSelectionState?.Exit();
			_currentSelectionState = _selectionStates[interactionObjectType];
			
			_selectedInteractable = interactable;
			
			_currentSelectionState.Enter();
		}

		protected abstract void SetFreeSelectionState();
	}
}