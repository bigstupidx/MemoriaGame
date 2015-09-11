//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(TweenAlpha_2))]
public class TweenAlpha_2Editor : UITweenerEditor
{
    public override void OnInspectorGUI ()
    {
        GUILayout.Space (6f);
        NGUIEditorTools.SetLabelWidth (120f);

        TweenAlpha_2 tw = target as TweenAlpha_2;
        GUI.changed = false;

        float from = EditorGUILayout.Slider ("From", tw.from, 0f, 1f);
        float to = EditorGUILayout.Slider ("To", tw.to, 0f, 1f);

        if (GUI.changed) {
            NGUIEditorTools.RegisterUndo ("Tween Change", tw);
            tw.from = from;
            tw.to = to;
            NGUITools.SetDirty (tw);
        }

        DrawCommonProperties ();
    }
}
