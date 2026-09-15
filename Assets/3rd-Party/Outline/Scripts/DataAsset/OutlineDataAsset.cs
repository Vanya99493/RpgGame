using UnityEngine;

[CreateAssetMenu(fileName = "OutlineDataAsset", menuName = "Scriptable Objects/Outline/Outline Data Asset")]
public class OutlineDataAsset : ScriptableObject
{
	[field: SerializeField]
	public Outline.Mode OutlineMode { get; private set; }
		
	[field: SerializeField]
	public Color OutlineColor { get; private set; }
		
	[field: SerializeField, Range(0f, 10f)]
	public float OutlineWidth { get; private set; }
}