using System;
using System.Collections.Generic;
using Runtime.Hub;
using Runtime.Interaction;
using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.Views
{
	public abstract class LocationView : MonoBehaviour, ILocation, IDisposable
	{
		[SerializeField]
		protected InteractionController _interactionController;
		
		[SerializeField]
		protected Transform _cameraNodeTransform;
		
		[Space(10)]
		[SerializeField]
		protected List<LocationView> _relatedLocationViews = new();
		
		protected IHubStateMachine _hubStateMachine;
		
		public Transform CameraNodeTransform => _cameraNodeTransform;
		public IInteractable Interactable => _interactionController;
		
		public virtual void Initialize(IHubStateMachine hubStateMachine)
		{
			_hubStateMachine = hubStateMachine;
			
			_interactionController.SelectEvent += OnLocationSelectEventHandler;

			DisableLocationInteraction();
		}

		public virtual void Dispose()
		{
			_interactionController.SelectEvent -= OnLocationSelectEventHandler;
		}
		
		public void EnterLocation()
		{
			foreach (var relatedLocation in _relatedLocationViews)
			{
				relatedLocation.EnableLocationInteraction();
			}
		}

		public void ExitLocation()
		{
			foreach (var relatedLocation in _relatedLocationViews)
			{
				relatedLocation.DisableLocationInteraction();
			}
		}

		protected virtual void EnableLocationInteraction()
		{
			_interactionController.Initialize(true);
		}

		protected virtual void DisableLocationInteraction()
		{
			_interactionController.Initialize(false);
		}

		protected abstract void OnLocationSelectEventHandler();
	}
}