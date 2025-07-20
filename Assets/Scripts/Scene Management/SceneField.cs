using UnityEngine;
using UnityEditor;

[System.Serializable]
public class SceneField
{
	[SerializeField] private Object sceneAsset;
	[SerializeField] private string sceneName = "";
	public string SceneName => sceneName;

	public static implicit operator string(SceneField sceneField) => sceneField.SceneName;

	public SceneField(string name) => sceneName = name;
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SceneField))]
public class SceneFieldPropertyDrawer : PropertyDrawer
{
	public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
	{
		EditorGUI.BeginProperty(_position, GUIContent.none, _property);
		SerializedProperty sceneAsset = _property.FindPropertyRelative("sceneAsset");
		SerializedProperty sceneName = _property.FindPropertyRelative("sceneName");
		_position = EditorGUI.PrefixLabel(_position, GUIUtility.GetControlID(FocusType.Passive), _label);
		if (sceneAsset != null)
		{
			sceneAsset.objectReferenceValue = EditorGUI.ObjectField(_position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false);

			if (sceneAsset.objectReferenceValue != null)
				sceneName.stringValue = (sceneAsset.objectReferenceValue as SceneAsset).name;
		}

		EditorGUI.EndProperty();
	}
}
#endif