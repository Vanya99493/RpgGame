using Runtime.Views;
using UnityEngine;

namespace Runtime.Hub
{
	public class HubLocationsContainer : MonoBehaviour
	{
		[field: SerializeField]
		public RaidMapView RaidMapView { get; private set; }
		
		[field: SerializeField]
		public ForestView ForestView { get; private set; }
		
		[field: SerializeField]
		public DungeonView DungeonView { get; private set; }
		
		[field: SerializeField]
		public CityView CityView { get; private set; }
		
		[field: SerializeField]
		public GuildView GuildView { get; private set; }
		
		[field: SerializeField]
		public ShopView ShopView { get; private set; }
		
		[field: SerializeField]
		public ForgeView ForgeView { get; private set; }
		
		[field: SerializeField]
		public HomeView HomeView { get; private set; }

		public void Initialize(IHubStateMachine hubStateMachine)
		{
			RaidMapView.Initialize(hubStateMachine);
			ForestView.Initialize(hubStateMachine);
			DungeonView.Initialize(hubStateMachine);
			CityView.Initialize(hubStateMachine);
			GuildView.Initialize(hubStateMachine);
			ShopView.Initialize(hubStateMachine);
			ForgeView.Initialize(hubStateMachine);
			HomeView.Initialize(hubStateMachine);
		}
	}
}