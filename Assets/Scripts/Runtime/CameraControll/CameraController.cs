using Runtime.Interfaces;
using Runtime.Models;
using UnityEngine;
using UnityEngine.InputSystem;
using InputActionPhase = Runtime.Enums.InputActionPhase;
using InputActionType = Runtime.Enums.InputActionType;

namespace Runtime.CameraControl
{
	public class CameraController : MonoBehaviour, ICameraController
	{
		[SerializeField]
		private CameraSelectionController _cameraSelectionController;
		
		[Header("Camera")]
		[SerializeField]
		private Camera _playerCamera;
		
		[Space(20)]
		[SerializeField]
		private RaycastSettings _raycastSettings;
		
		[Header("Input Action References")]
		[SerializeField]
		private InputActionReference _primaryButtonInputActionReference;
		
		[SerializeField]
		private InputActionReference _secondaryButtonInputActionReference;

		private bool _isSelectionActive;
		private IInteractable _hoveredInteractable;

		private void OnEnable()
		{
			SubscribeEvents();
		}

		private void OnDisable()
		{
			UnsubscribeEvents();
		}

		private void Update()
		{
			if (!_isSelectionActive)
			{
				return;
			}
			
			CastSelectionRay();
		}

		public void Initialize()
		{
			_cameraSelectionController.Initialize(_playerCamera);
		}

		public void ActivateSelection()
		{
			_isSelectionActive = true;
			SubscribeEvents();
		}

		public void DeactivateSelection()
		{
			_isSelectionActive = false;
			UnsubscribeEvents();
			UnHoverHoveredInteractableObject();
		}

		private void SubscribeEvents()
		{
			_primaryButtonInputActionReference.action.performed += OnPrimaryButtonPerformedEventHandler;
			_primaryButtonInputActionReference.action.canceled += OnPrimaryButtonCanceledEventHandler;
			_primaryButtonInputActionReference.action.Enable();
			
			_secondaryButtonInputActionReference.action.performed += OnSecondaryButtonPerformedEventHandler;
			_secondaryButtonInputActionReference.action.canceled += OnSecondaryButtonCanceledEventHandler;
			_secondaryButtonInputActionReference.action.Enable();
		}

		private void UnsubscribeEvents()
		{
			_primaryButtonInputActionReference.action.performed -= OnPrimaryButtonPerformedEventHandler;
			_primaryButtonInputActionReference.action.canceled -= OnPrimaryButtonCanceledEventHandler;
			_primaryButtonInputActionReference.action.Disable();
			
			_secondaryButtonInputActionReference.action.performed -= OnSecondaryButtonPerformedEventHandler;
			_secondaryButtonInputActionReference.action.canceled -= OnSecondaryButtonCanceledEventHandler;
			_secondaryButtonInputActionReference.action.Disable();
		}

		private void CastSelectionRay()
		{
			Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out RaycastHit hit, _raycastSettings.RaycastDistance, _raycastSettings.SelectionLayerMask))
			{
				if (hit.collider.TryGetComponent(out IInteractable interactable))
				{
					if (_hoveredInteractable == null)
					{
						_hoveredInteractable = interactable;
						_hoveredInteractable.Hover();
					}
					else
					{
						if (_hoveredInteractable != interactable)
						{
							_hoveredInteractable.UnHover();
							_hoveredInteractable = interactable;
							_hoveredInteractable.Hover();
						}
					}
				}
				else
				{
					UnHoverHoveredInteractableObject();
				}
			}
			else
			{
				UnHoverHoveredInteractableObject();
			}
		}

		private void UnHoverHoveredInteractableObject()
		{
			_hoveredInteractable?.UnHover();
			_hoveredInteractable = null;
		}

		private void OnPrimaryButtonPerformedEventHandler(InputAction.CallbackContext callback)
		{
			CastSelectionRay(InputActionType.PrimaryAction, InputActionPhase.Performed);
		}

		private void OnPrimaryButtonCanceledEventHandler(InputAction.CallbackContext callback)
		{
			CastSelectionRay(InputActionType.PrimaryAction, InputActionPhase.Canceled);
		}

		private void OnSecondaryButtonPerformedEventHandler(InputAction.CallbackContext callback)
		{
			CastSelectionRay(InputActionType.SecondaryAction, InputActionPhase.Performed);
		}

		private void OnSecondaryButtonCanceledEventHandler(InputAction.CallbackContext callback)
		{
			CastSelectionRay(InputActionType.SecondaryAction, InputActionPhase.Canceled);
		}

		private void CastSelectionRay(InputActionType inputActionType, InputActionPhase inputActionPhase)
		{
			Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out RaycastHit hit, _raycastSettings.RaycastDistance, _raycastSettings.SelectionLayerMask))
			{
				if (hit.collider.TryGetComponent(out IInteractable interactable))
				{
					_cameraSelectionController.SelectObject(
						interactable, 
						inputActionType, 
						inputActionPhase);
				}
			}
			else
			{
				_cameraSelectionController.ReleaseSelection();
			}
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			if (_playerCamera == null)
			{
				return;
			}

			Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);

			Gizmos.color = Color.green;
			Gizmos.DrawRay(ray.origin, ray.direction * _raycastSettings.RaycastDistance);

			if (Physics.Raycast(ray, out RaycastHit hit, _raycastSettings.RaycastDistance, _raycastSettings.SelectionLayerMask))
			{
				Gizmos.color = Color.red;
				Gizmos.DrawSphere(hit.point, 0.15f);
			}
		}
#endif
	}
}