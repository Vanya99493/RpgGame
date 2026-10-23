using UnityEngine;

namespace Configs.Animation
{
	[CreateAssetMenu(fileName = "AnimationParametersConfig", menuName = "Scriptable Objects/Animation/Animation Parameters")]
	public class AnimationParametersConfig : ScriptableObject
	{
		[field: SerializeField]
		public string MoveAnimationName { get; private set; }
	}
}