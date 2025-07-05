using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Settings/Description Message", fileName = "Description Message")]
public class DescriptionMessage : ScriptableObject
{
    [field: TextArea]
    [field: SerializeField] public string Message { get; private set; }
}
