using System;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI.Panels
{
	public class DungeonPanel : BasePanel
	{
		[SerializeField]
		private Button _raidMapButton;

		public event Action RaidMapButtonClickEvent;
		
		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();
			
			_raidMapButton.onClick.AddListener(OnRaidMapButtonClick);
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_raidMapButton.onClick.RemoveListener(OnRaidMapButtonClick);
		}

		private void OnRaidMapButtonClick()
		{
			RaidMapButtonClickEvent?.Invoke();
		}
	}
}