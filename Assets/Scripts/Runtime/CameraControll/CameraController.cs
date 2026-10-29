using System;
using Runtime.InputControl;
using Runtime.Interfaces;
using Runtime.Models;
using UnityEngine;
using InputActionPhase = Runtime.Enums.InputActionPhase;
using InputActionType = Runtime.Enums.InputActionType;

namespace Runtime.CameraControl
{
	public class CameraController : MonoBehaviour, ICameraController, IDisposable
	{
		[SerializeField]
		private CameraSelectionController _cameraSelectionController;
		
		[SerializeField]
		private CameraNavigationController _navigationController;
		
		[Header("Camera")]
		[SerializeField]
		private Camera _playerCamera;
		
		[Space(20)]
		[SerializeField]
		private RaycastSettings _raycastSettings;
		
		private InputController _inputController;
		private bool _isSelectionActive;
		private IInteractable _hoveredInteractable;
		
		public ICameraNavigation CameraNavigation => _navigationController;

		private void Update()
		{
			if (!_isSelectionActive)
			{
				return;
			}
			
			CastSelectionRay();
		}

		public void Initialize(InputController inputController)
		{
			_cameraSelectionController.Initialize(_playerCamera);
			_navigationController.Initialize(_playerCamera);
			
			_inputController = inputController;
			
			_inputController.PrimaryButtonPerformedEvent += OnPrimaryButtonPerformedEventHandler;
			_inputController.SecondaryButtonPerformedEvent += OnSecondaryButtonPerformedEventHandler;
		}
		
		public void Dispose()
		{
			_inputController.PrimaryButtonPerformedEvent -= OnPrimaryButtonPerformedEventHandler;
			_inputController.SecondaryButtonPerformedEvent -= OnSecondaryButtonPerformedEventHandler;
		}

		public void ActivateSelection()
		{
			_isSelectionActive = true;
		}

		public void DeactivateSelection()
		{
			_isSelectionActive = false;
			UnHoverHoveredInteractableObject();
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

		private void OnPrimaryButtonPerformedEventHandler(InputActionPhase inputActionPhase)
		{
			CastSelectionRay(InputActionType.PrimaryAction, inputActionPhase);
		}

		private void OnSecondaryButtonPerformedEventHandler(InputActionPhase inputActionPhase)
		{
			CastSelectionRay(InputActionType.SecondaryAction, inputActionPhase);
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