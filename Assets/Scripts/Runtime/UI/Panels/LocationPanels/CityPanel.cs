using System;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI.Panels
{
	public class CityPanel : BasePanel
	{
		[SerializeField]
		private Button _raidButton;

		public event Action RaidButtonClickEvent;
		
		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();
			
			_raidButton.onClick.AddListener(OnRaidButtonClick);
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_raidButton.onClick.RemoveListener(OnRaidButtonClick);
		}

		private void OnRaidButtonClick()
		{
			RaidButtonClickEvent?.Invoke();
		}
	}
}