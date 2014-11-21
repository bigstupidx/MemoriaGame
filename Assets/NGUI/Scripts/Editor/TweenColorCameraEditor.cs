using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TweenColorCamera))]
public class TweenColorCameraEditor : TweenerEditor
{
		public override void OnInspectorGUI ()
		{
				GUILayout.Space(6f);
				NGUIEditorTools.SetLabelWidth(120f);

				TweenColorCamera tw = target as TweenColorCamera;
				GUI.changed = false;

				Color from = EditorGUILayout.ColorField("From", tw.from);
				Color to = EditorGUILayout.ColorField("To", tw.to);

				if (GUI.changed)
				{
						NGUIEditorTools.RegisterUndo("Tween Change", tw);
						tw.from = from;
						tw.to = to;
						NGUITools.SetDirty(tw);
				}

				DrawCommonProperties();
		}
}
