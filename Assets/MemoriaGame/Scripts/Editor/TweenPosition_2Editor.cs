//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(TweenPosition_2))]
public class TweenPosition_2Editor : UITweenerEditor
{
    public override void OnInspectorGUI ()
    {
        GUILayout.Space (6f);
        NGUIEditorTools.SetLabelWidth (120f);

        TweenPosition_2 tw = target as TweenPosition_2;
        GUI.changed = false;

        Vector3 from = EditorGUILayout.Vector3Field ("From", tw.from);
        Vector3 to = EditorGUILayout.Vector3Field ("To", tw.to);

        if (GUI.changed) {
            NGUIEditorTools.RegisterUndo ("Tween Change", tw);
            tw.from = from;
            tw.to = to;
            NGUITools.SetDirty (tw);
        }

        DrawCommonProperties ();
    }
}
