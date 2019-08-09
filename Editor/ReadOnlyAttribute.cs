using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ReadOnlyAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer {
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}

/* TODO: fix the belows problem:
 * The code below will display a scroll bar in the editor but it will NOT be read only.
    [ReadOnly]
    [Range(-10.0f, 70.0f)]
    public float temperatureCelcius;

    The code below will NOT display a scroll bar in the editor
    [Range(-10.0f, 70.0f)]
    [ReadOnly]
    public float temperatureCelcius;
*/

#endif