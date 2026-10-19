using Runtime.Interfaces;
using Runtime.Models;
using UnityEngine;
using UnityEngine.InputSystem;
using InputActionType = Runtime.Enums.InputActionType;

namespace Runtime.Player
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
			_cameraSelectionController.Initialize();
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
			_secondaryButtonInputActionReference.action.performed += OnSecondaryButtonPerformedEventHandler;
			
			_primaryButtonInputActionReference.action.Enable();
			_secondaryButtonInputActionReference.action.Enable();
		}

		private void UnsubscribeEvents()
		{
			_primaryButtonInputActionReference.action.performed -= OnPrimaryButtonPerformedEventHandler;
			_secondaryButtonInputActionReference.action.performed -= OnSecondaryButtonPerformedEventHandler;
			
			_primaryButtonInputActionReference.action.Disable();
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
			Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out RaycastHit hit, _raycastSettings.RaycastDistance, _raycastSettings.SelectionLayerMask))
			{
				if (hit.collider.TryGetComponent(out IInteractable interactable))
				{
					_cameraSelectionController.SelectObject(interactable, InputActionType.PrimaryAction);
				}
			}
			else
			{
				_cameraSelectionController.ReleaseSelection();
			}
		}

		private void OnSecondaryButtonPerformedEventHandler(InputAction.CallbackContext callback)
		{
			Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out RaycastHit hit, _raycastSettings.RaycastDistance, _raycastSettings.SelectionLayerMask))
			{
				if (hit.collider.TryGetComponent(out IInteractable interactable))
				{
					_cameraSelectionController.SelectObject(interactable, InputActionType.SecondaryAction);
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