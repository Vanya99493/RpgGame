using Runtime.Interaction;
using Runtime.Models;
using UnityEngine;

namespace Runtime.Character
{
	public class CharacterController : MonoBehaviour
	{
		[SerializeField]
		private InteractionController _interactionController;
		
		public CharacterData CharacterData { get; private set; }

		private void OnEnable()
		{
			SubscribeOnEvents();
		}

		private void OnDestroy()
		{
			UnsubscribeFromEvents();
		}
		
		private void SubscribeOnEvents()
		{
			_interactionController.SelectEvent += OnSelectEventHandler;
		}

		private void UnsubscribeFromEvents()
		{
			_interactionController.SelectEvent -= OnSelectEventHandler;
		}
		
		private void OnSelectEventHandler()
		{
			
		}
	}
}