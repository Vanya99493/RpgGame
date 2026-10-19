using Runtime.Enums;
using UnityEngine;

namespace Runtime.Interfaces
{
	public interface IInteractable
	{
		GameObject Root { get; }
		InteractionObjectType InteractionObjectType { get; }
		
		void Hover();
		void UnHover();
		void Select();
		void Release();
	}
}