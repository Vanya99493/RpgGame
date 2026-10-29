using Runtime.UI.Panels;
using UnityEngine;

namespace Runtime.UI.Core
{
	public class HubUIManager : MonoBehaviour
	{
		[SerializeField]
		private RaidMapPanel _raidMapPanel;

		[SerializeField]
		private ForestPanel _forestPanel;
		
		[SerializeField]
		private DungeonPanel _dungeonPanel;
		
		[SerializeField]
		private CityPanel _cityPanel;
		
		[SerializeField]
		private GuildPanel _guildPanel;
		
		[SerializeField]
		private ShopPanel _shopPanel;
		
		[SerializeField]
		private ForgePanel _forgePanel;
		
		[SerializeField]
		private HomePanel _homePanel;

		public RaidMapPanel RaidMapPanel => _raidMapPanel;
		public ForestPanel ForestPanel => _forestPanel;
		public DungeonPanel DungeonPanel => _dungeonPanel;
		public CityPanel CityPanel => _cityPanel;
		public GuildPanel GuildPanel => _guildPanel;
		public ShopPanel ShopPanel => _shopPanel;
		public ForgePanel ForgePanel => _forgePanel;
		public HomePanel HomePanel => _homePanel;

		public void Initialize()
		{
			CloseAllPanels();
		}

		private void CloseAllPanels()
		{
			_raidMapPanel.Close();
			_forestPanel.Close();
			_dungeonPanel.Close();
			_cityPanel.Close();
			_guildPanel.Close();
			_shopPanel.Close();
			_forgePanel.Close();
			_homePanel.Close();
		}
	}
}