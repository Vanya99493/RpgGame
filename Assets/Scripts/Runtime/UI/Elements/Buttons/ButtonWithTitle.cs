using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI.Elements
{
	public class ButtonWithTitle : MonoBehaviour
	{
		[SerializeField]
		private Button _button;
		
		[SerializeField]
		private TMP_Text _title;
		
		public Button Button => _button;
		public TMP_Text Title => _title;
	}
}