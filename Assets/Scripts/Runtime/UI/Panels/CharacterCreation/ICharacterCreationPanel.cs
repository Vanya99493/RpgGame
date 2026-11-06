using System;
using Runtime.Enums;

namespace Runtime.UI.Panels
{
	public interface ICharacterCreationPanel
	{
		public event Action<BodyPartType> ChangeBodyPartEvent;
		public event Action ToPreviousElementEvent;
		public event Action ToNextElementEvent;

		void SetIndexText(int index);
	}
}