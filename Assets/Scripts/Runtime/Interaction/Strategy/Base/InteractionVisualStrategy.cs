using UnityEngine;

namespace Runtime.Interaction
{
	public abstract class InteractionVisualStrategy : MonoBehaviour
	{
		public abstract void Hover();
		public abstract void UnHover();
		public abstract void Select();
		public abstract void Release();
	}
}