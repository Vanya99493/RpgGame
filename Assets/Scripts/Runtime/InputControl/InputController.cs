using System;
using UnityEngine;
using UnityEngine.InputSystem;
using InputActionPhase = Runtime.Enums.InputActionPhase;

namespace Runtime.InputControl
{
	public class InputController : MonoBehaviour, IDisposable
	{
		[Header("Input Action References")]
		[SerializeField]
		private InputActionReference _primaryButtonInputActionReference;
		
		[SerializeField]
		private InputActionReference _secondaryButtonInputActionReference;

		public event Action<InputActionPhase> PrimaryButtonPerformedEvent;
		public event Action<InputActionPhase> SecondaryButtonPerformedEvent;

		public void Initialize()
		{
			_primaryButtonInputActionReference.action.performed += OnPrimaryButtonPerformedEventHandler;
			_primaryButtonInputActionReference.action.canceled += OnPrimaryButtonCanceledEventHandler;
			_primaryButtonInputActionReference.action.Enable();
			
			_secondaryButtonInputActionReference.action.performed += OnSecondaryButtonPerformedEventHandler;
			_secondaryButtonInputActionReference.action.canceled += OnSecondaryButtonCanceledEventHandler;
			_secondaryButtonInputActionReference.action.Enable();
		}

		public void Dispose()
		{
			_primaryButtonInputActionReference.action.performed -= OnPrimaryButtonPerformedEventHandler;
			_primaryButtonInputActionReference.action.canceled -= OnPrimaryButtonCanceledEventHandler;
			_primaryButtonInputActionReference.action.Disable();
			
			_secondaryButtonInputActionReference.action.performed -= OnSecondaryButtonPerformedEventHandler;
			_secondaryButtonInputActionReference.action.canceled -= OnSecondaryButtonCanceledEventHandler;
			_secondaryButtonInputActionReference.action.Disable();
		}

		private void OnPrimaryButtonPerformedEventHandler(InputAction.CallbackContext callback)
		{
			PrimaryButtonPerformedEvent?.Invoke(InputActionPhase.Performed);
		}

		private void OnPrimaryButtonCanceledEventHandler(InputAction.CallbackContext callback)
		{
			PrimaryButtonPerformedEvent?.Invoke(InputActionPhase.Canceled);
		}

		private void OnSecondaryButtonPerformedEventHandler(InputAction.CallbackContext callback)
		{
			SecondaryButtonPerformedEvent?.Invoke(InputActionPhase.Performed);
		}

		private void OnSecondaryButtonCanceledEventHandler(InputAction.CallbackContext callback)
		{
			SecondaryButtonPerformedEvent?.Invoke(InputActionPhase.Canceled);
		}
	}
}