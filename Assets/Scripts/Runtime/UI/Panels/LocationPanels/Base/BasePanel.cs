using UnityEngine;

namespace Runtime.UI.Panels
{
	public class BasePanel : MonoBehaviour
	{
		public bool IsOpen { get; private set; }
		
		private void OnEnable()
		{
			SubscribeOnEvents();
		}

		private void OnDisable()
		{
			UnsubscribeFromEvents();
		}

		public virtual void Open()
		{
			gameObject.SetActive(true);
			IsOpen = true;
		}

		public virtual void Close()
		{
			gameObject.SetActive(false);
			IsOpen = false;
		}

		protected virtual void SubscribeOnEvents() { }

		protected virtual void UnsubscribeFromEvents() { }
	}
}