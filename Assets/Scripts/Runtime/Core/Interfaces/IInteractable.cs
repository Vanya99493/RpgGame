using System;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Interfaces
{
	public interface IInteractable
	{
		GameObject Root { get; }
		InteractionObjectType InteractionObjectType { get; }
		
		public event Action HoverEvent;
		public event Action UnHoverEvent;
		public event Action SelectEvent;
		public event Action ReleaseEvent;
		
		void Hover();
		void UnHover();
		void Select();
		void Release();
	}
}