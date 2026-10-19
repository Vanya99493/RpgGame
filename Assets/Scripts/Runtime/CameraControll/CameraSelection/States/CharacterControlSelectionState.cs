using Runtime.Enums;
using Runtime.HexagonalGrid;
using Runtime.Interfaces;
using Runtime.Player.States.Base;
using UnityEngine;
using CharacterController = Runtime.Character.CharacterController;

namespace Runtime.Player.States
{
	public class CharacterControlSelectionState : BaseSelectionState
	{
		public CharacterControlSelectionState(ICameraSelectionStateMachine selectionStateMachine) : base(selectionStateMachine) { }

		public override void SelectObject(IInteractable interactable, InputActionType inputActionType)
		{
			if (interactable.InteractionObjectType == InteractionObjectType.Grid 
			    && inputActionType == InputActionType.SecondaryAction)
			{
				if (_selectionStateMachine.SelectedInteractableObject.Root.TryGetComponent(out CharacterController characterController)
				    && interactable.Root.TryGetComponent(out HexagonalGridElement hexagonalGridElement))
				{
					characterController.Move(hexagonalGridElement);
					
					_selectionStateMachine.ReleaseSelection();
				}
				else
				{
					Debug.LogError("Cannot get components from character or grid element");
				}
				
				return;
			}
			
			if (inputActionType == InputActionType.PrimaryAction)
			{
				_selectionStateMachine.ChangeSelectionState(interactable, inputActionType);
			}
			else
			{
				_selectionStateMachine.ReleaseSelection();
			}
		}
	}
}