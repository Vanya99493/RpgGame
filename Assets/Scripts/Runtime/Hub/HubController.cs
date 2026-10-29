using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Hub
{
	public class HubController : MonoBehaviour, IHubStateMachine
	{
		[SerializeField]
		private HubLocationsContainer _locationContainer;
		
		private HubContext _hubContext;
		private Dictionary<Type, BaseHubState> _states = new();
		private BaseHubState _currentState;

		public void Initialize(HubContext hubContext)
		{
			_hubContext = hubContext;
			
			_locationContainer.Initialize(this);
			
			_states = new()
			{
				{ typeof(RaidMapHubState), new RaidMapHubState(hubContext, _locationContainer.RaidMapView) },
				{ typeof(ForestHubState), new ForestHubState(hubContext, _locationContainer.ForestView) },
				{ typeof(DungeonHubState), new DungeonHubState(hubContext, _locationContainer.DungeonView) },
				{ typeof(CityHubState), new CityHubState(hubContext, _locationContainer.CityView) },
				{ typeof(GuildHubState), new GuildHubState(hubContext, _locationContainer.GuildView) },
				{ typeof(ShopHubState), new ShopHubState(hubContext, _locationContainer.ShopView) },
				{ typeof(ForgeHubState), new ForgeHubState(hubContext, _locationContainer.ForgeView) },
				{ typeof(HomeHubState), new HomeHubState(hubContext, _locationContainer.HomeView) },
			};
		}
		
		public void SetState<T>()
		{
			_currentState?.Exit();
			_currentState = _states[typeof(T)];
			_currentState.PreEnter();
			
			_hubContext.CameraController.CameraNavigation.MoveTo(
				_currentState.Location.CameraNodeTransform, 
				_currentState.Enter);
		}

		public void SetStateWithoutTransition<T>()
		{
			_currentState?.Exit();
			_currentState = _states[typeof(T)];
			_currentState.PreEnter();
			
			_hubContext.CameraController.CameraNavigation.InstantlyMoveTo(
				_currentState.Location.CameraNodeTransform, 
				_currentState.Enter);
		}
	}
}