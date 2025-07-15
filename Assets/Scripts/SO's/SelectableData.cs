using UnityEngine;

[CreateAssetMenu(fileName = "Selectable Data", menuName = "Scriptable Objects/Selectable Data")]
public class SelectableData : ScriptableObject
{
    [Header("Animation")]
    [field: SerializeField] public Vector3 normalScale { get; private set; }
    [field: SerializeField] public Vector3 selectedScale { get; private set; }
    [field: SerializeField] public float animationDuration { get; private set; }

    [Header("Background")]
    [field: SerializeField] public Color backgroundNormalColor { get; private set; }
    [field: SerializeField] public Color backgroundSelectedColor { get; private set; }

    [Header("Text")]
    [field: SerializeField] public Color textNormalColor { get; private set; }
    [field: SerializeField] public Color textSelectedColor { get; private set; }
}

