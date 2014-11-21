using UnityEngine;
using UnityEditor;

/// <summary>
/// Inspector class used to edit UISpriteAnimationLimit.
/// </summary>

[CanEditMultipleObjects]
[CustomEditor(typeof(UISpriteAnimationLimit))]
public class UISpriteAnimationLimitInspector : Editor
{
		/// <summary>
		/// Draw the inspector widget.
		/// </summary>

		public override void OnInspectorGUI ()
		{
				GUILayout.Space(3f);
				NGUIEditorTools.SetLabelWidth(80f);
				serializedObject.Update();

				NGUIEditorTools.DrawProperty("Framerate", serializedObject, "mFPS");
				NGUIEditorTools.DrawProperty("Name Prefix", serializedObject, "mPrefix");
				NGUIEditorTools.DrawProperty("Min Frame", serializedObject, "mMinSprite");
				NGUIEditorTools.DrawProperty("Max Frame", serializedObject, "mMaxSprite");
				NGUIEditorTools.DrawProperty("Reverse", serializedObject, "mReverse");
				NGUIEditorTools.DrawProperty("Loop", serializedObject, "mLoop");
				NGUIEditorTools.DrawProperty("Pixel Snap", serializedObject, "mSnap");

				serializedObject.ApplyModifiedProperties();

				NGUIEditorTools.DrawEvents("On Finished", (UISpriteAnimationLimit)target, ((UISpriteAnimationLimit)target).onFinished);

		}
}
