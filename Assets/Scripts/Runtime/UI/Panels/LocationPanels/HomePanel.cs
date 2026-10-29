using System;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI.Panels
{
	public class HomePanel : BasePanel
	{
		[SerializeField]
		private Button _cityButton;

		public event Action CityButtonClickEvent;
		
		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();
			
			_cityButton.onClick.AddListener(OnCityButtonClick);
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_cityButton.onClick.RemoveListener(OnCityButtonClick);
		}

		private void OnCityButtonClick()
		{
			CityButtonClickEvent?.Invoke();
		}
	}
}